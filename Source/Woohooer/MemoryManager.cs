using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal static class MemoryManager
{
    private static readonly ThoughtDef prisonerWoohoo = DefDatabase<ThoughtDef>.GetNamed("PrisonerWoohoo");

    private static readonly ThoughtDef masochistPrisonerWoohoo =
        DefDatabase<ThoughtDef>.GetNamed("MasochistPrisonerWoohoo");

    private static readonly ThoughtDef prisonerWoohooMemory = DefDatabase<ThoughtDef>.GetNamed("PrisonerWoohooMemory");

    private static readonly ThoughtDef masochistPrisonerWoohooMemory =
        DefDatabase<ThoughtDef>.GetNamed("MasochistPrisonerWoohooMemory");

    private static readonly ThoughtDef woohooColonist = DefDatabase<ThoughtDef>.GetNamed("WoohooColonist");
    private static readonly ThoughtDef woohooColonistRegret = DefDatabase<ThoughtDef>.GetNamed("WoohooColonistRegret");
    private static readonly ThoughtDef woohooNeutral = DefDatabase<ThoughtDef>.GetNamed("WoohooNeutral");
    private static readonly ThoughtDef woohooKink = DefDatabase<ThoughtDef>.GetNamed("WoohooKink");
    private static readonly ThoughtDef woohooKinkMemory = DefDatabase<ThoughtDef>.GetNamed("WoohooKinkMemory");

    public static Toil AddMoodletsToil(Pawn pawn, Pawn mate)
    {
        return new Toil
        {
            initAction = delegate { addMoodlets(pawn, mate); },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }

    private static void addMoodlets(Pawn pawn, Pawn mate)
    {
        if (mate.guest is { IsPrisoner: true } or { IsPrisoner: true })
        {
            addPrisonMoodlets(pawn, mate);
        }
        else
        {
            addEqualsMoodlets(pawn, mate);
        }
    }

    private static void addEqualsMoodlets(Pawn pawn, Pawn mate)
    {
        if (isKinky(pawn) && isKinky(mate) || PawnHelper.IsStranger(pawn, mate))
        {
            addMemory(mate, woohooKink);
            addMemoryOfOther(mate, woohooKinkMemory, pawn);
            addMemory(pawn, woohooKink);
            addMemoryOfOther(pawn, woohooKinkMemory, mate);
            return;
        }

        addMemory(mate, woohooColonist);
        if (Rand.Value < WoohooMod.Instance.Settings.LovedItChance)
        {
            addMemoryOfOther(mate, ThoughtDefOf.GotSomeLovin, pawn);
        }

        addMemory(pawn, woohooColonist);
        if (Rand.Value < WoohooMod.Instance.Settings.LovedItChance)
        {
            addMemoryOfOther(pawn, ThoughtDefOf.GotSomeLovin, mate);
        }
    }

    private static bool isKinky(Pawn pawn)
    {
        return pawn.IsBloodlust() || pawn.IsPsychopath() || pawn.IsMasochist();
    }

    private static void addPrisonMoodlets(Pawn torturer, Pawn victim)
    {
        victim.records.Increment(Constants.HorrificMemories);
        if (torturer.IsBloodlust() || torturer.IsPsychopath())
        {
            addMemory(torturer, woohooColonist);
        }
        else if (torturer.IsKind())
        {
            addMemory(torturer, woohooColonistRegret);
        }
        else
        {
            addMemory(torturer, woohooNeutral);
        }

        if (victim.IsMasochist())
        {
            addMemory(victim, masochistPrisonerWoohoo);
            addMemoryOfOther(victim, masochistPrisonerWoohooMemory, torturer);
            return;
        }

        addMemory(victim, prisonerWoohoo);
        if (victim.IsPsychopath() || victim.IsBloodlust())
        {
            addMemoryOfOther(victim, woohooNeutral, torturer);
        }
        else
        {
            addMemoryOfOther(victim, prisonerWoohooMemory, torturer);
        }
    }

    private static void addMemory(Pawn p, ThoughtDef thoughtDef)
    {
        p.needs.mood.thoughts.memories.TryGainMemory(thoughtDef);
    }

    private static void addMemoryOfOther(Pawn p, ThoughtDef thoughtDef, Pawn other)
    {
        p.needs.mood.thoughts.memories.TryGainMemory(thoughtDef, other);
    }
}