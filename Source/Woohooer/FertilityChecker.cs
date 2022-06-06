using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

internal static class FertilityChecker
{
    public static bool is_fertile(Pawn pawn)
    {
        return getFetility(pawn) > Constants.MINIMUM_REPO;
    }

    public static float getFetility(Pawn pawn)
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
        if (Constants.Fertility != null && (level = pawn.health.capacities.GetLevel(Constants.Fertility)) >= 0f)
        {
            return level;
        }

        if (Constants.Reproduction != null && (level = pawn.health.capacities.GetLevel(Constants.Reproduction)) >= 0f)
        {
            return level;
        }

        return pawn.ageTracker.CurLifeStage.reproductive ? 1f : 0f;
    }

    public static bool alreadyPregnant(Pawn pawn)
    {
        return pawn.health.hediffSet.HasHediff(HediffDefOf.Pregnant);
    }

    public static bool is_FemaleForBabies(Pawn pawn)
    {
        return pawn.gender == Gender.Female || hasBionicWomb(pawn);
    }

    public static bool hasBionicWomb(Pawn pawn)
    {
        return pawn.health.hediffSet.HasHediff(Constants.BionicWomb);
    }
}