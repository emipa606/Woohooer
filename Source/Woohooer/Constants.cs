using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

internal static class Constants
{
    public static readonly float MINIMUM_REPO = 0.01f;

    public static readonly JobDef JobWooHoo = DefDatabase<JobDef>.GetNamed("WooHoo");

    public static readonly JobDef JobWooHoo_Baby = DefDatabase<JobDef>.GetNamed("WooHoo_Baby");

    public static readonly JobDef JobWooHooRecieve = DefDatabase<JobDef>.GetNamed("WooHoo_Get");

    public static readonly HediffDef BionicWomb = HediffDef.Named("BionicWomb");

    public static readonly HediffDef GivingBirth = HediffDef.Named("GivingBirth");

    public static readonly PawnCapacityDef Fertility = DefDatabase<PawnCapacityDef>.GetNamedSilentFail("Fertility");

    public static readonly PawnCapacityDef Reproduction =
        DefDatabase<PawnCapacityDef>.GetNamedSilentFail("Reproduction");

    public static readonly JoyKindDef Joy_Woohoo = DefDatabase<JoyKindDef>.GetNamed("Woohoo");

    public static readonly RecordDef CountAskedForWoohoo = DefDatabase<RecordDef>.GetNamed("CountAskedForWoohoo");

    public static readonly RecordDef CountGotAskedToWooHoo = DefDatabase<RecordDef>.GetNamed("CountGotAskedToWooHoo");

    public static readonly RecordDef CountGotAskedToWooHooSaidNo =
        DefDatabase<RecordDef>.GetNamed("CountGotAskedToWooHooSaidNo");

    public static readonly RecordDef HorrificMemories = DefDatabase<RecordDef>.GetNamed("HorrificMemories");

    public static readonly RecordDef TimesWooHooedGotPregnant =
        DefDatabase<RecordDef>.GetNamed("TimesWooHooedGotPregnant");
}