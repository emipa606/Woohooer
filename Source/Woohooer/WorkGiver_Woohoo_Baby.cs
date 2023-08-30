using DarkIntentionsWoohoo.mod.settings;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class WorkGiver_Woohoo_Baby : WorkGiver_Woohoo
{
    public override float MateChance()
    {
        return WoohooModSettings.woohooBabyChildChance;
    }

    public override bool IsMate(Pawn pawn, Pawn pawn2)
    {
        return true;
    }

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (!base.HasJobOnThing(pawn, t, forced))
        {
            return false;
        }

        var mate = t as Pawn;

        if (!PawnHelper.IsSameRaceHumanoid(pawn, mate))
        {
            JobFailReason.Is("Whohooer.SameRace".Translate());
            return false;
        }

        if (pawn.gender == mate.gender)
        {
            JobFailReason.Is("Whohooer.NotFertile".Translate(pawn.Name.ToStringShort));
            return false;
        }

        if (!FertilityChecker.is_fertile(pawn))
        {
            JobFailReason.Is("Whohooer.NotFertile".Translate(pawn.Name.ToStringShort));
            return false;
        }

        if (FertilityChecker.is_fertile(mate))
        {
            return (FertilityChecker.is_FemaleForBabies(pawn) || FertilityChecker.is_FemaleForBabies(mate));
        }

        JobFailReason.Is("Whohooer.NotFertile".Translate(mate?.Name.ToStringShort));
        return false;
    }
}