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
        if (!(TargetA != null) || TargetA.Thing is not Pawn mate || !(TargetB != null) ||
            TargetB.Thing is not Building_Bed buildingBed || pawn == null ||
            !PawnHelper.IsSameRaceHumanoid(pawn, mate) || buildingBed.IsBurning())
        {
            Log.Error($"[{pawn?.Name}] can't woohoo right.");
            EndJobWith(JobCondition.Errored);
            return null;
        }

        if (pawn == mate)
        {
            throw new Exception("You cant WooHoo Alone and Together with yourself");
        }

        var hookupBedmanager = new HookupBedmanager(buildingBed);
        bool askSuccess;
        IEnumerable<Toil> first;
        if (mate.IsNotWoohooing())
        {
            pawn.records.Increment(Constants.CountAskedForWoohoo);
            mate.records.Increment(Constants.CountGotAskedToWooHoo);
            askSuccess = askPartner(pawn, mate);
            first = WoohooManager.ToilsAskForWoohoo(pawn, mate, buildingBed, askSuccess, hookupBedmanager);
        }
        else
        {
            askSuccess = true;
            first = nothing();
        }

        if (askSuccess)
        {
            first = first.Union(WoohooManager.MakePartnerWoohoo(pawn, mate, buildingBed))
                .Union(WoohooManager.AnimateLovin(pawn, mate, buildingBed))
                .Union(makeMyLoveToils(pawn, mate))
                .Union(WoohooManager.AnimateLovin(pawn, mate, buildingBed,
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

    private static IEnumerable<Toil> nothing()
    {
        yield break;
    }

    private static bool askPartner(Pawn asker, Pawn mate)
    {
        return asker != null && mate != null &&
               (JailHelper.IsThisJailLovin(asker, mate) || !PawnHelper.IsStranger(asker, mate) || Rand.Bool);
    }

    private IEnumerable<Toil> makeMyLoveToils(Pawn asker, Pawn mate)
    {
        if (!asker.IsPsychopath() && PawnHelper.IsStranger(asker, mate) &&
            !JailHelper.IsThisJailLovin(asker, mate))
        {
            Toils_Interpersonal.TryRecruit(TargetIndex.A);
        }

        yield return MemoryManager.AddMoodletsToil(asker, mate);

        yield return BabyMaker.DoMakeBaby(asker, mate, IsMakeBaby());
    }

    public override bool CanBeginNowWhileLyingDown()
    {
        return JobInBedUtility.InBedOrRestSpotNow(pawn, TargetB) &&
               JobInBedUtility.InBedOrRestSpotNow(TargetA.Thing as Pawn, TargetB);
    }

    protected virtual bool IsMakeBaby()
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