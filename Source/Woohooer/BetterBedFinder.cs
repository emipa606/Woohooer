using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

internal class BetterBedFinder
{
    public static Building_Bed DoBetterBedFinder(Pawn pawn, Pawn mate)
    {
        if (pawn == null || mate == null)
        {
            return null;
        }

        Building_Bed result;
        if ((result = PawnBedBigEnough(pawn)) != null)
        {
            return result;
        }

        if ((result = PawnBedBigEnough(pawn)) != null)
        {
            return result;
        }

        var list = pawn.Map.listerBuildings.allBuildingsColonist.ConvertAll(x => x as Building_Bed);
        if (!list.Any())
        {
            return null;
        }

        IEnumerable<Building_Bed>
            source = list.Where(x => x is { SleepingSlotsCount: > 1, Medical: false }).ToList();
        if (!source.Any())
        {
            return null;
        }

        var source2 = source.Where(x => x.OwnersForReading.Contains(pawn) || x.OwnersForReading.Contains(mate));
        var list2 = source2.ToList();
        if (list2.Any())
        {
            foreach (var item in list2)
            {
                if (item != null)
                {
                    return item;
                }

                Log.Error("How the hell is the owned bed null?");
            }
        }

        foreach (var item2 in source.Where(x => x.OwnersForReading == null || !x.OwnersForReading.Any()))
        {
            if (canReserve(pawn, item2) && canReserve(mate, item2))
            {
                return item2;
            }
        }

        foreach (var item3 in source.Where(bed => bed.CurOccupants == null || !bed.CurOccupants.Any()))
        {
            if (canReserve(pawn, item3) && canReserve(mate, item3))
            {
                return item3;
            }
        }

        return null;
    }

    private static bool canReserve(Pawn traveler, Building_Bed building_Bed)
    {
        LocalTargetInfo target = building_Bed;
        var peMode = PathEndMode.OnCell;
        var maxDanger = Danger.Some;
        var sleepingSlotsCount = building_Bed.SleepingSlotsCount;
        if (!traveler.CanReserveAndReach(target, peMode, maxDanger, sleepingSlotsCount))
        {
            return false;
        }

        return true;
    }

    private static Building_Bed PawnBedBigEnough(Pawn pawn)
    {
        if (pawn == null)
        {
            return null;
        }

        var building_Bed = pawn.CurrentBed();
        if (building_Bed is { SleepingSlotsCount: > 1 })
        {
            return building_Bed;
        }

        return null;
    }
}