using HarmonyLib;
using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo.harmony;

[HarmonyPatch(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn), typeof(PawnGenerationRequest))]
public static class PawnGenerator_GeneratePawn
{
    public static void Postfix(ref Pawn __result, PawnGenerationRequest request)
    {
        if (__result?.guest is { IsPrisoner: true } && __result.guest.HostFaction == Faction.OfPlayer)
        {
            __result.guest.SetGuestStatus(null);
        }
    }
}