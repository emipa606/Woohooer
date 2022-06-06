using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

public static class JailHelper
{
    public static bool IsThisJailLovin(Pawn pawn, Pawn mate, Building_Bed bed = null)
    {
        return pawn is { guest.IsPrisoner: true } || mate is { guest.IsPrisoner: true } ||
               (bed?.ForPrisoners ?? false);
    }
}