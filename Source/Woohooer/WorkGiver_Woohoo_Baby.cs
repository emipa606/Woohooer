using DarkIntentionsWoohoo.mod.settings;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class WorkGiver_Woohoo_Baby : WorkGiver_Woohoo
{
    public override float MateChance()
    {
        return WoohooSettingHelper.latest.woohooBabyChildChance;
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

        if (!FertilityChecker.is_fertile(pawn))
        {
            JobFailReason.Is("Whohooer.NotFertile".Translate(pawn.Name.ToStringShort));
            return false;
        }

        var other = t as Pawn;
        if (FertilityChecker.is_fertile(other))
        {
            return FertilityChecker.is_FemaleForBabies(pawn) ||
                   FertilityChecker.is_FemaleForBabies(other);
        }

        JobFailReason.Is("Whohooer.NotFertile".Translate(other?.Name.ToStringShort));
        return false;
    }
}