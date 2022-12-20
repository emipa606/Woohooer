using HarmonyLib;
using Verse;

namespace DarkIntentionsWoohoo;

internal class ChildrenCrossMod
{
    private delegate string CSLDelegate(Pawn donor, Pawn womb);
    private static readonly CSLDelegate _method = AccessTools.MethodDelegate<CSLDelegate>(AccessTools.Method("MorePawnUtil:Loved"));

    public static void Mated(Pawn donor, Pawn womb) => _method(donor, womb);
}