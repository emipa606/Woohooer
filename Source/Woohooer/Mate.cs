using RimWorld;
using Verse;
using HarmonyLib;

namespace DarkIntentionsWoohoo;

public class Mate
{
    private static readonly AccessTools.FieldRef<float> _pregnancyChance = AccessTools.StaticFieldRefAccess<float>(AccessTools.Field(typeof(JobDriver_Lovin), "PregnancyChance"));
 
    public static void Mated(Pawn donor, Pawn hasWomb, bool isMakeBaby = false)
    {
        if (ModsConfig.BiotechActive)
        {
            PregnancyApproach currentApproach = hasWomb.relations.GetPregnancyApproachForPartner(donor);
            if (isMakeBaby) 
            {
                hasWomb.relations.SetPregnancyApproach(donor, PregnancyApproach.TryForBaby);
            }
            Log.Message($"[{hasWomb?.Name}] found Biotech");
            var chance = Rand.Chance(_pregnancyChance() * PregnancyUtility.PregnancyChanceForPartners(hasWomb, donor));
            if (donor != null && hasWomb != null && chance)
            {
                Hediff_Pregnant hediff_Pregnant = (Hediff_Pregnant)HediffMaker.MakeHediff(HediffDefOf.PregnantHuman, hasWomb, null);
                hediff_Pregnant.SetParents(null, donor, PregnancyUtility.GetInheritedGeneSet(donor, hasWomb));
                hasWomb.health.AddHediff(hediff_Pregnant, null, null, null);
                Log.Message($"[{hasWomb?.Name}] succeeded: {chance}");
            }
            else
            {
                Log.Message($"[{hasWomb?.Name}] failed: {chance}");
            }
            hasWomb.relations.SetPregnancyApproach(donor, currentApproach);
        }
        else if (ModsConfig.IsActive("Dylan.CSL"))
        {
            ChildrenCrossMod.Mated(donor, hasWomb);
        } 
        else
        {
            DefaultMate(donor, hasWomb);
        }
    }

    public static void DefaultMate(Pawn donor, Pawn womb)
    {
        PawnUtility.Mated(donor, womb);
    }
}