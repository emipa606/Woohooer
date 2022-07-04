using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

internal static class PawnHelper
{
    public static bool IsHumanoid(this Pawn pawn)
    {
        return pawn.RaceProps.Humanlike;
    }

    public static bool IsAdult(this Pawn pawn)
    {
        return pawn.ageTracker.Adult;
    }

    public static bool IsSameRaceHumanoid(Pawn pawn, Pawn mate)
    {
        return pawn.RaceProps.Humanlike && mate.RaceProps.Humanlike && pawn.kindDef.race == mate.kindDef.race;
    }

    public static bool IsMasochist(this Pawn pawn)
    {
        return pawn is { story.traits: { } } && pawn.story.traits.HasTrait(TraitDef.Named("Masochist"));
    }

    public static bool IsPsychopath(this Pawn pawn)
    {
        return pawn is { story.traits: { } } && pawn.story.traits.HasTrait(TraitDefOf.Psychopath);
    }

    public static bool IsBloodlust(this Pawn pawn)
    {
        return pawn is { story.traits: { } } && pawn.story.traits.HasTrait(TraitDefOf.Bloodlust);
    }

    public static bool IsBrawler(this Pawn pawn)
    {
        return pawn is { story.traits: { } } && pawn.story.traits.HasTrait(TraitDefOf.Brawler);
    }

    public static bool IsKind(this Pawn pawn)
    {
        return pawn is { story.traits: { } } && pawn.story.traits.HasTrait(TraitDefOf.Kind);
    }

    public static bool IsNotWoohooing(this Pawn mate)
    {
        if (mate.CurJob == null)
        {
            return true;
        }

        return mate.CurJob.def != JobDefOf.Lovin && mate.CurJob.def != Constants.JobWooHoo &&
               mate.CurJob.def != Constants.JobWooHoo_Baby && mate.CurJob.def != Constants.JobWooHooRecieve;
    }

    public static bool IsStranger(Pawn pawn, Pawn mate)
    {
        return pawn.guest == null && mate.guest != null;
    }

    public static void DelayNextWooHoo(Pawn pawn)
    {
        pawn.mindState.canLovinTick = Find.TickManager.TicksGame +
                                      Rand.Range((int)(WoohooSettingHelper.latest.minAITicks * 0.9f),
                                          (int)(WoohooSettingHelper.latest.minAITicks * 1.1f));
    }
}