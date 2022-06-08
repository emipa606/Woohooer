using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal static class WoohooManager
{
    public static IEnumerable<Toil> MakePartnerWoohoo(Pawn pawn, Pawn mate, Building_Bed bed)
    {
        var tick = 400;
        var t = new Toil
        {
            socialMode = RandomSocialMode.Off,
            tickAction = NewFunction,
            initAction = NewFunction
        };
        t.AddEndCondition(() =>
            mate.IsNotWoohooing() && tick-- > 0 ? JobCondition.Ongoing : JobCondition.Succeeded);
        t.AddFinishAction(delegate { Log.Message("Got Partner to Start WooHoo-ing Allegedly."); });
        yield return t;

        void NewFunction()
        {
            if (!mate.IsNotWoohooing())
            {
                return;
            }

            var newJob = new Job(Constants.JobWooHooRecieve, pawn, bed)
            {
                playerForced = true
            };
            mate.jobs.StartJob(newJob, JobCondition.InterruptForced);
        }
    }

    public static IEnumerable<Toil> AnimateLovin(Pawn pawn, Pawn mate, Building_Bed bed, Action finishAction = null,
        int len = 250)
    {
        if (bed == null)
        {
            yield break;
        }

        Toil toil;
        var t = toil = ToilerHelper.GotoThing(pawn, bed);
        yield return toil;
        t.AddFinishAction(delegate { Log.Message("Got To Bed for woohooing"); });
        var layDown = new Toil
        {
            initAction = delegate
            {
                pawn?.pather?.StopDead();
                if (pawn?.jobs != null)
                {
                    pawn.jobs.posture = PawnPosture.LayingInBed;
                }
            },
            tickAction = delegate { pawn?.GainComfortFromCellIfPossible(); }
        };
        layDown.AddFinishAction(delegate
        {
            pawn?.needs?.joy?.GainJoy(Rand.Value * 0.15f, Constants.Joy_Woohoo);
            mate?.needs?.joy?.GainJoy(Rand.Value * 0.15f, Constants.Joy_Woohoo);
        });
        layDown.AddPreTickAction(delegate
        {
            if (pawn.IsHashIntervalTick(100))
            {
                FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart);
            }
        });
        if (finishAction != null)
        {
            layDown.AddFinishAction(finishAction);
        }

        layDown.defaultCompleteMode = ToilCompleteMode.Delay;
        layDown.defaultDuration = len;
        yield return layDown;
    }

    public static IEnumerable<Toil> ToilsAskForWoohoo(Pawn pawn, Pawn mate, Building_Bed bed, bool askSuccess,
        HookupBedmanager hookupBedmanager)
    {
        yield return ToilerHelper.GotoThing(pawn, mate);
        yield return AskForWoohoo(pawn, mate, bed, askSuccess);
        if (askSuccess)
        {
            yield return new Toil
            {
                initAction = delegate { hookupBedmanager.claim(pawn, mate); },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
            yield break;
        }

        yield return new Toil
        {
            initAction = delegate
            {
                var newJob = new Job(JobDefOf.Insult, pawn, bed);
                mate.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, true);
            },
            socialMode = RandomSocialMode.Off,
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }

    public static Toil AskForWoohoo(Pawn pawn, Pawn mate, Building_Bed bed, bool askSuccess)
    {
        var reply = askSuccess ? FleckDefOf.Heart : FleckDefOf.SleepZ;
        var toil = new Toil
        {
            initAction = delegate
            {
                if (!pawn.IsHashIntervalTick(100))
                {
                    FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart);
                }
            },
            tickAction = delegate
            {
                if (!pawn.IsHashIntervalTick(100))
                {
                    return;
                }

                FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart);
                if (mate == null)
                {
                    return;
                }

                _ = mate.Position;
                if (true)
                {
                    FleckMaker.ThrowMetaIcon(mate.Position, pawn.Map, reply);
                }
            },
            socialMode = RandomSocialMode.Off,
            defaultCompleteMode = ToilCompleteMode.Delay,
            defaultDuration = 250
        };
        toil.AddFinishAction(delegate { Log.Message("Done Asking"); });
        return toil;
    }
}