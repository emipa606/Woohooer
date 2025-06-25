using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

internal static class FertilityChecker
{
    public static bool is_fertile(Pawn pawn)
    {
        return GetFertility(pawn) > Constants.MINIMUM_REPO;
    }

    public static float GetFertility(Pawn pawn)
    {
        if (alreadyPregnant(pawn))
        {
            return 0f;
        }

        if (hasBionicWomb(pawn))
        {
            return 1f;
        }

        float level;
        if (Constants.Fertility != null && (level = pawn.health.capacities.GetLevel(Constants.Fertility)) >= 0f ||
            Constants.Reproduction != null && (level = pawn.health.capacities.GetLevel(Constants.Reproduction)) >= 0f)
        {
            return level;
        }

        return pawn.ageTracker.CurLifeStage.reproductive ? 1f : 0f;
    }

    private static bool alreadyPregnant(Pawn pawn)
    {
        return pawn.health.hediffSet.HasHediff(HediffDefOf.Pregnant);
    }

    public static bool is_FemaleForBabies(Pawn pawn)
    {
        return pawn.gender == Gender.Female || hasBionicWomb(pawn);
    }

    private static bool hasBionicWomb(Pawn pawn)
    {
        return pawn.health.hediffSet.HasHediff(Constants.BionicWomb);
    }
}