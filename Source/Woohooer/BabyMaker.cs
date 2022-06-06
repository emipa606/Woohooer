using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class BabyMaker
{
    public static Toil DoMakeBaby(Pawn pawn, Pawn TargetA)
    {
        return new Toil
        {
            initAction = delegate
            {
                if (!FertilityChecker.is_fertile(pawn) || !FertilityChecker.is_fertile(TargetA))
                {
                    return;
                }

                if (FertilityChecker.is_FemaleForBabies(pawn))
                {
                    Mate.Mated(TargetA, pawn);
                    pawn.records.Increment(Constants.TimesWooHooedGotPregnant);
                }

                if (!FertilityChecker.is_FemaleForBabies(TargetA))
                {
                    return;
                }

                Mate.Mated(pawn, TargetA);
                TargetA.records.Increment(Constants.TimesWooHooedGotPregnant);
            },
            socialMode = RandomSocialMode.Off,
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }
}