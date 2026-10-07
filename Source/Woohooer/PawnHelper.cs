using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

internal static class PawnHelper
{
    extension(Pawn pawn)
    {
        public bool IsHumanoid()
        {
            return pawn.RaceProps.Humanlike;
        }

        public bool IsAdult()
        {
            return pawn.ageTracker.Adult;
        }
    }

    public static bool IsSameRaceHumanoid(Pawn pawn, Pawn mate)
    {
        return pawn.RaceProps.Humanlike && mate.RaceProps.Humanlike && pawn.kindDef.race == mate.kindDef.race;
    }

    extension(Pawn pawn)
    {
        public bool IsMasochist()
        {
            return pawn is { story.traits: not null } && pawn.story.traits.HasTrait(TraitDef.Named("Masochist"));
        }

        public bool IsPsychopath()
        {
            return pawn is { story.traits: not null } && pawn.story.traits.HasTrait(TraitDefOf.Psychopath);
        }

        public bool IsBloodlust()
        {
            return pawn is { story.traits: not null } && pawn.story.traits.HasTrait(TraitDefOf.Bloodlust);
        }

        public bool IsBrawler()
        {
            return pawn is { story.traits: not null } && pawn.story.traits.HasTrait(TraitDefOf.Brawler);
        }

        public bool IsKind()
        {
            return pawn is { story.traits: not null } && pawn.story.traits.HasTrait(TraitDefOf.Kind);
        }

        public bool IsNotWoohooing()
        {
            if (pawn.CurJob == null)
            {
                return true;
            }

            return pawn.CurJob.def != JobDefOf.Lovin && pawn.CurJob.def != Constants.JobWooHoo &&
                   pawn.CurJob.def != Constants.JobWooHoo_Baby && pawn.CurJob.def != Constants.JobWooHooRecieve;
        }
    }

    public static bool IsStranger(Pawn pawn, Pawn mate)
    {
        return pawn.guest == null && mate.guest != null;
    }

    public static void DelayNextWooHoo(Pawn pawn)
    {
        pawn.mindState.canLovinTick = Find.TickManager.TicksGame +
                                      Rand.Range((int)(WoohooMod.Instance.Settings.MinAITicks * 0.9f),
                                          (int)(WoohooMod.Instance.Settings.MinAITicks * 1.1f));
    }
}