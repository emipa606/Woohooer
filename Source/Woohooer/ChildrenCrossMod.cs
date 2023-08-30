using HarmonyLib;
using Verse;

namespace DarkIntentionsWoohoo;

internal class ChildrenCrossMod
{
    private static readonly CSLDelegate _method =
        AccessTools.MethodDelegate<CSLDelegate>(AccessTools.Method("MorePawnUtil:Loved"));

    public static void Mated(Pawn donor, Pawn womb)
    {
        _method(donor, womb);
    }

    private delegate string CSLDelegate(Pawn donor, Pawn womb);
}