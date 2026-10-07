using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo.harmony;

[HarmonyPatch(typeof(Hediff_Pregnant), nameof(Hediff_Pregnant.DoBirthSpawn), null)]
public static class Hediff_Pregnant_DoBirthSpawn
{
    public static bool Prefix(Pawn mother, Pawn father)
    {
        if (mother == null || !mother.IsHumanoid())
        {
            return true;
        }

        switch (mother.guest)
        {
            case { IsPrisoner: true } when mother.Faction != Faction.OfPlayer && father != null &&
                                           father.IsHumanoid() && father.IsKind():
                mother.SetFaction(Faction.OfPlayer, father);
                TaleRecorder.RecordTale(TaleDefOf.Recruited, father, mother);
                father.records.Increment(RecordDefOf.PrisonersRecruited);
                mother.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.RecruitedMe, father);
                break;
            case { IsPrisoner: true } when mother.Faction != Faction.OfPlayer:
            {
                var heDiffPrisonerGivingBirth =
                    (HeDiffPrisonerGivingBirth)HediffMaker.MakeHediff(Constants.GivingBirth, mother);
                heDiffPrisonerGivingBirth.Faction = mother.Faction;
                mother.health.AddHediff(heDiffPrisonerGivingBirth);
                mother.SetFactionDirect(Faction.OfPlayer);
                break;
            }
        }

        return true;
    }

    public static void Postfix(Pawn mother)
    {
        if (mother == null || !mother.IsHumanoid())
        {
            return;
        }

        var l = new List<HeDiffPrisonerGivingBirth>();
        mother.health.hediffSet.GetHediffs(ref l);
        var enumerable = (from x in l
            where true
            select x).ToArray();
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