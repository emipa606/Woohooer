using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class JobDriver_Woohoo_Receive : JobDriver
{
    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return pawn != null;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        var list = new List<Toil>
        {
            Toils_Goto.Goto(TargetIndex.A, PathEndMode.ClosestTouch),
            Toils_Goto.Goto(TargetIndex.B, PathEndMode.ClosestTouch)
        };
        list.AddRange(WoohooManager.AnimateLovin(pawn, TargetA.Thing as Pawn, TargetB.Thing as Building_Bed));
        var obj = new Toil
        {
            initAction = delegate { Log.Message("Getting Woohooing, will be done in 400 ticks"); },
            defaultDuration = 400,
            defaultCompleteMode = ToilCompleteMode.Delay
        };
        list.Add(obj);
        obj.AddFinishAction(delegate { Log.Message("Done Woohing Get"); });
        PawnHelper.DelayNextWooHoo(pawn);
        return list;
    }
}