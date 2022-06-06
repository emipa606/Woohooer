using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class WorkGiver_Woohoo : WorkGiver_Scanner
{
    private Building_Bed bed;

    public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForGroup(ThingRequestGroup.Pawn);

    public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t == null || pawn == null)
        {
            return false;
        }

        if (t is not Pawn pawn2 || pawn == pawn2 || !forced && !canAutoLove(pawn, pawn2) || pawn2.Downed ||
            pawn2.Faction != pawn.Faction && (pawn2.guest == null || pawn2.Drafted) || !PawnHelper.is_human(pawn) ||
            !PawnHelper.is_human(pawn2) || !PawnHelper.IsNotWoohooing(pawn) || !PawnHelper.IsNotWoohooing(pawn2))
        {
            return false;
        }

        if (ModsConfig.IdeologyActive && !BedUtility.WillingToShareBed(pawn, pawn2))
        {
            JobFailReason.Is("IdeoligionForbids".Translate());
            return false;
        }

        LocalTargetInfo target = pawn2;
        if (!pawn.CanReserve(target, 1, -1, null, forced))
        {
            return false;
        }

        bed = BetterBedFinder.DoBetterBedFinder(pawn, pawn2);
        return bed != null;
    }

    private bool canAutoLove(Pawn pawn, Pawn pawn2)
    {
        var ticksGame = Find.TickManager.TicksGame;
        int result;
        if (WoohooSettingHelper.latest.allowAIWoohoo && pawn.mindState.canLovinTick < ticksGame &&
            pawn2.mindState.canLovinTick < ticksGame && JobUtilityIdle.isIdle(pawn2) &&
            pawn2.needs?.joy?.tolerances != null && pawn.needs?.joy?.tolerances != null)
        {
            var needs = pawn2.needs;
            if (needs != null)
            {
                var mood = needs.mood;
                if (mood != null)
                {
                    _ = mood.CurLevel;
                    var needs2 = pawn.needs;
                    if (needs2 != null)
                    {
                        var mood2 = needs2.mood;
                        if (mood2 != null)
                        {
                            _ = mood2.CurLevel;
                            if (!pawn2.needs.joy.tolerances.BoredOf(Constants.Joy_Woohoo) &&
                                !pawn.needs.joy.tolerances.BoredOf(Constants.Joy_Woohoo) &&
                                (pawn2.needs.joy.CurLevel < 0.6f || pawn2.needs.mood.CurLevel < 0.6f) &&
                                (pawn.needs.joy.CurLevel < 0.6f || pawn.needs.mood.CurLevel < 0.6f) &&
                                Rand.Value < 0.1f && RelationsUtility.PawnsKnowEachOther(pawn, pawn2))
                            {
                                result = WoohooSettingHelper.latest.familyWeight *
                                         LovePartnerRelationUtility.IncestOpinionOffsetFor(pawn2, pawn) *
                                         Rand.Value <
                                         0.5f
                                    ? 1
                                    : 0;
                                goto IL_01c1;
                            }
                        }
                    }
                }
            }
        }

        result = 0;
        IL_01c1:
        return (byte)result != 0;
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t == null || pawn == null)
        {
            return null;
        }

        var pawn2 = t as Pawn;
        if (!PawnHelper.is_human(pawn) || !PawnHelper.is_human(pawn2))
        {
            return null;
        }

        if (IsMate(pawn, pawn2))
        {
            return new Job(Constants.JobWooHoo_Baby, pawn2, bed)
            {
                count = 1
            };
        }

        return new Job(Constants.JobWooHoo, pawn2, bed)
        {
            count = 1
        };
    }

    public virtual float MateChance()
    {
        return WoohooSettingHelper.latest.woohooChildChance;
    }

    public virtual bool IsMate(Pawn pawn, Pawn pawn2)
    {
        var num = FertilityChecker.getFetility(pawn) + (FertilityChecker.getFetility(pawn2) / 2f);
        num *= MateChance();
        if (pawn.gender == pawn2.gender && !WoohooSettingHelper.latest.sameGender)
        {
            return false;
        }

        return Rand.Value < num;
    }
}