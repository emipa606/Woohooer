using HarmonyLib;
using Verse;

namespace DarkIntentionsWoohoo;

internal static class ChildrenCrossMod
{
    private static readonly CslDelegate _method =
        AccessTools.MethodDelegate<CslDelegate>(AccessTools.Method("MorePawnUtil:Loved"));

    public static void Mated(Pawn donor, Pawn womb)
    {
        _method(donor, womb);
    }

    private delegate string CslDelegate(Pawn donor, Pawn womb);
}