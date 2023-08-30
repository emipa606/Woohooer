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
        if ((result = PawnBedBigEnough(pawn)) != null && canReach(mate, result))
        {
            return result;
        }

        if ((result = PawnBedBigEnough(mate)) != null && canReach(pawn, result))
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

    private static bool canReach(Pawn pawn, Building_Bed bed)
    {
        return pawn.CanReach(bed, PathEndMode.Touch, Danger.Some);
    }

    private static bool canReserve(Pawn traveler, Building_Bed building_Bed)
    {
        return traveler.CanReserveAndReach(building_Bed, PathEndMode.OnCell, Danger.Some,
            building_Bed.SleepingSlotsCount);
    }

    private static Building_Bed PawnBedBigEnough(Pawn pawn)
    {
        var building_Bed = pawn?.CurrentBed();
        return building_Bed is { SleepingSlotsCount: >= 1 } ? building_Bed : null;
    }
}