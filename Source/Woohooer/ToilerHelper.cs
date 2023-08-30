using System;
using DarkIntentionsWoohoo.mod.settings;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

public static class ToilerHelper
{
    public static Toil GotoThing(Pawn pawn, Thing thing, ToilCompleteMode mode = ToilCompleteMode.PatherArrival)
    {
        if (pawn == null || thing == null)
        {
            Log.Error("Not Going Anywhere...");
            return null;
        }

        var toil = new Toil
        {
            initAction = delegate
            {
                pawn.pather?.StartPath(thing, PathEndMode.OnCell);
                if (thing is not Pawn pawn1)
                {
                    return;
                }

                try
                {
                    pawn1.pather?.StartPath(thing, PathEndMode.OnCell);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        };
        toil.AddFinishAction(delegate { WoohooMod.LogMessage($"Got to [{thing}]."); });
        toil.socialMode = RandomSocialMode.Off;
        toil.defaultCompleteMode = mode;
        return toil;
    }
}