using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo.harmony;

internal class wrapps
{
    [HarmonyPatch(typeof(Hediff_Pregnant), "DoBirthSpawn", null)]
    public static class Hediff_Pregnant_DoBirthSpawn_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(Pawn mother, Pawn father)
        {
            if (mother == null || !mother.IsHumanoid())
            {
                return true;
            }

            if (mother.guest is { IsPrisoner: true } && mother.Faction != Faction.OfPlayer && father != null &&
                father.IsHumanoid() && father.IsKind())
            {
                mother.SetFaction(Faction.OfPlayer, father);
                TaleRecorder.RecordTale(TaleDefOf.Recruited, father, mother);
                father.records.Increment(RecordDefOf.PrisonersRecruited);
                mother.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.RecruitedMe, father);
            }
            else if (mother.guest is { IsPrisoner: true } && mother.Faction != Faction.OfPlayer)
            {
                var heDiffPrisonerGivingBirth =
                    (HeDiffPrisonerGivingBirth)HediffMaker.MakeHediff(Constants.GivingBirth, mother);
                heDiffPrisonerGivingBirth.Faction = mother.Faction;
                mother.health.AddHediff(heDiffPrisonerGivingBirth);
                mother.SetFactionDirect(Faction.OfPlayer);
            }

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(Pawn mother, Pawn father)
        {
            if (mother == null || !mother.IsHumanoid())
            {
                return;
            }
            var l = new System.Collections.Generic.List<HeDiffPrisonerGivingBirth>();
            mother.health.hediffSet.GetHediffs<HeDiffPrisonerGivingBirth>(ref l);
            var enumerable = from x in l
                             //GetHediffs<HeDiffPrisonerGivingBirth>()
                where true
                select x;
            if (!enumerable.Any())
            {
                return;
            }

            foreach (var item in enumerable)
            {
                if (item.Faction != null)
                {
                    mother.SetFactionDirect(item.Faction);
                }

                mother.health.RemoveHediff(item);
            }
        }
    }
}