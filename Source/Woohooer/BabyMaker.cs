using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class BabyMaker
{
    public static Toil DoMakeBaby(Pawn pawn, Pawn target, bool isMakeBaby)
    {
        return new Toil
        {
            initAction = delegate
            {
                if (!FertilityChecker.is_fertile(pawn) || !FertilityChecker.is_fertile(target))
                {
                    return;
                }

                if (FertilityChecker.is_FemaleForBabies(pawn))
                {
                    Mate.Mated(target, pawn, isMakeBaby);
                    pawn.records.Increment(Constants.TimesWooHooedGotPregnant);
                } 
                else if (FertilityChecker.is_FemaleForBabies(target))
                {
                    Mate.Mated(pawn, target, isMakeBaby);
                    target.records.Increment(Constants.TimesWooHooedGotPregnant);
                }

            },
            socialMode = RandomSocialMode.Off,
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }
}