using System;
using System.Collections.Generic;
using System.Linq;
using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class JobDriver_Woohoo : JobDriver
{
    protected override IEnumerable<Toil> MakeNewToils()
    {
        if (!(TargetA != null) || TargetA.Thing == null || !(TargetA.Thing is Pawn mate) || !(TargetB != null) ||
            TargetB.Thing == null || !(TargetB.Thing is Building_Bed building_Bed) || pawn == null ||
            !PawnHelper.IsSameRaceHumanoid(pawn, mate) || building_Bed.IsBurning())
        {
            Log.Error($"[{pawn?.Name}] can't woohoo right.");
            EndJobWith(JobCondition.Errored);
            return null;
        }

        if (pawn == mate)
        {
            throw new Exception("You cant WooHoo Alone and Together with yourself");
        }

        var hookupBedmanager = new HookupBedmanager(building_Bed);
        bool askSuccess;
        IEnumerable<Toil> first;
        if (mate.IsNotWoohooing())
        {
            pawn.records.Increment(Constants.CountAskedForWoohoo);
            mate.records.Increment(Constants.CountGotAskedToWooHoo);
            askSuccess = AskPartner(pawn, mate);
            first = WoohooManager.ToilsAskForWoohoo(pawn, mate, building_Bed, askSuccess, hookupBedmanager);
        }
        else
        {
            askSuccess = true;
            first = nothing();
        }

        if (askSuccess)
        {
            first = first.Union(WoohooManager.MakePartnerWoohoo(pawn, mate, building_Bed))
                .Union(WoohooManager.AnimateLovin(pawn, mate, building_Bed))
                .Union(MakeMyLoveToils(pawn, mate))
                .Union(WoohooManager.AnimateLovin(pawn, mate, building_Bed,
                    delegate { WoohooMod.LogMessage("We're done animating on main job."); }, 500));
        }
        else
        {
            mate.records.Increment(Constants.CountGotAskedToWooHooSaidNo);
        }

        first = first.Union(hookupBedmanager.GiveBackToil());
        PawnHelper.DelayNextWooHoo(pawn);
        return first;
    }

    private IEnumerable<Toil> nothing()
    {
        yield break;
    }

    private bool AskPartner(Pawn asker, Pawn mate)
    {
        return asker != null && mate != null &&
               (JailHelper.IsThisJailLovin(asker, mate) || !PawnHelper.IsStranger(asker, mate) || Rand.Bool);
    }

    public IEnumerable<Toil> MakeMyLoveToils(Pawn asker, Pawn mate)
    {
        if (!asker.IsPsychopath() && PawnHelper.IsStranger(asker, mate) &&
            !JailHelper.IsThisJailLovin(asker, mate))
        {
            Toils_Interpersonal.TryRecruit(TargetIndex.A);
        }

        yield return MemoryManager.addMoodletsToil(asker, mate);

        yield return BabyMaker.DoMakeBaby(asker, mate, isMakeBaby());
    }

    public override bool CanBeginNowWhileLyingDown()
    {
        return JobInBedUtility.InBedOrRestSpotNow(pawn, TargetB) &&
               JobInBedUtility.InBedOrRestSpotNow(TargetA.Thing as Pawn, TargetB);
    }

    protected virtual bool isMakeBaby()
    {
        return false;
    }

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        if (TargetA != null && TargetA.Thing is Pawn mate && TargetB != null && TargetB.Thing is Building_Bed t &&
            pawn != null && PawnHelper.IsSameRaceHumanoid(pawn, mate) && !t.IsBurning() &&
            pawn.mindState.canLovinTick < Find.TickManager.TicksGame)
        {
            return true;
        }

        WoohooMod.LogMessage(
            $"[{pawn?.Name}] can't woohoo right. Timing out their lovin for 500 ticks. They tried to some weird stuff:{GetReport()}");
        if (pawn != null)
        {
            pawn.mindState.canLovinTick = Find.TickManager.TicksGame + 500;
        }

        return false;
    }
}