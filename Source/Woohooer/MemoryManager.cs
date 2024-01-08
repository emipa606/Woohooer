using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class MemoryManager
{
    public static readonly ThoughtDef PrisonerWoohoo = DefDatabase<ThoughtDef>.GetNamed("PrisonerWoohoo");

    public static readonly ThoughtDef MasochistPrisonerWoohoo =
        DefDatabase<ThoughtDef>.GetNamed("MasochistPrisonerWoohoo");

    public static readonly ThoughtDef PrisonerWoohooMemory = DefDatabase<ThoughtDef>.GetNamed("PrisonerWoohooMemory");

    public static readonly ThoughtDef MasochistPrisonerWoohooMemory =
        DefDatabase<ThoughtDef>.GetNamed("MasochistPrisonerWoohooMemory");

    public static readonly ThoughtDef WoohooColonist = DefDatabase<ThoughtDef>.GetNamed("WoohooColonist");
    public static readonly ThoughtDef WoohooColonistRegret = DefDatabase<ThoughtDef>.GetNamed("WoohooColonistRegret");
    public static readonly ThoughtDef WoohooNeutral = DefDatabase<ThoughtDef>.GetNamed("WoohooNeutral");
    public static readonly ThoughtDef WoohooKink = DefDatabase<ThoughtDef>.GetNamed("WoohooKink");
    public static readonly ThoughtDef WoohooKinkMemory = DefDatabase<ThoughtDef>.GetNamed("WoohooKinkMemory");

    public static Toil addMoodletsToil(Pawn pawn, Pawn mate)
    {
        return new Toil
        {
            initAction = delegate { addMoodlets(pawn, mate); },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }

    public static void addMoodlets(Pawn pawn, Pawn mate)
    {
        if (mate.guest is { IsPrisoner: true })
        {
            addPrisonMoodlets(pawn, mate);
        }
        else if (mate.guest is { IsPrisoner: true })
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
            addMemory(mate, WoohooKink);
            addMemoryOfOther(mate, WoohooKinkMemory, pawn);
            addMemory(pawn, WoohooKink);
            addMemoryOfOther(pawn, WoohooKinkMemory, mate);
            return;
        }

        addMemory(mate, WoohooColonist);
        if (Rand.Value < WoohooMod.instance.Settings.lovedItChance)
        {
            addMemoryOfOther(mate, ThoughtDefOf.GotSomeLovin, pawn);
        }

        addMemory(pawn, WoohooColonist);
        if (Rand.Value < WoohooMod.instance.Settings.lovedItChance)
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
            addMemory(torturer, WoohooColonist);
        }
        else if (torturer.IsKind())
        {
            addMemory(torturer, WoohooColonistRegret);
        }
        else
        {
            addMemory(torturer, WoohooNeutral);
        }

        if (victim.IsMasochist())
        {
            addMemory(victim, MasochistPrisonerWoohoo);
            addMemoryOfOther(victim, MasochistPrisonerWoohooMemory, torturer);
            return;
        }

        addMemory(victim, PrisonerWoohoo);
        if (victim.IsPsychopath() || victim.IsBloodlust())
        {
            addMemoryOfOther(victim, WoohooNeutral, torturer);
        }
        else
        {
            addMemoryOfOther(victim, PrisonerWoohooMemory, torturer);
        }
    }

    public static void addMemory(Pawn p, ThoughtDef thoughtDef)
    {
        p.needs.mood.thoughts.memories.TryGainMemory(thoughtDef);
    }

    public static void addMemoryOfOther(Pawn p, ThoughtDef thoughtDef, Pawn other)
    {
        p.needs.mood.thoughts.memories.TryGainMemory(thoughtDef, other);
    }
}