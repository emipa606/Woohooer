using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace DarkIntentionsWoohoo;

public class HookupBedmanager
{
    private readonly Building_Bed bed;

    private readonly IEnumerable<Pawn> owners;

    public HookupBedmanager(Building_Bed bed)
    {
        this.bed = bed;
        if (bed == null)
        {
            return;
        }

        var enumerable = currentOwners();
        if (enumerable != null)
        {
            owners = enumerable.Where(_ => true);
        }
    }

    public IEnumerable<Toil> GiveBackToil()
    {
        if (bed != null)
        {
            yield return new Toil
            {
                initAction = GiveBack,
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }
    }

    public void claim(Pawn bedPawn1, Pawn bedPawn2)
    {
        if (bed == null)
        {
            return;
        }

        if (owners != null)
        {
            foreach (var owner in owners)
            {
                releaseBed(bed, owner);
            }
        }

        if (currentOwners() != null && currentOwners().Any())
        {
            foreach (var item in currentOwners())
            {
                releaseBed(bed, item);
            }
        }

        _ = claimBed(bed, bedPawn1) && claimBed(bed, bedPawn2);
    }

    public void GiveBack()
    {
        if (bed == null)
        {
            return;
        }

        foreach (var item in currentOwners())
        {
            if (owners == null || !owners.Contains(item))
            {
                releaseBed(bed, item);
            }
        }

        if (owners == null)
        {
            return;
        }

        foreach (var item2 in owners.Where(pawn => currentOwners() != null && !currentOwners().Contains(pawn)))
        {
            claimBed(bed, item2);
        }
    }

    public IEnumerable<Pawn> currentOwners()
    {
        if (bed.OwnersForReading != null && bed.OwnersForReading.Any())
        {
            return bed.OwnersForReading.ToList().AsEnumerable();
        }

        if (bed.OwnersForReading != null)
        {
            return bed.OwnersForReading.ToList().AsEnumerable();
        }

        return null;
    }

    public static bool claimBed(Building_Bed bed, Pawn pawn)
    {
        if (pawn == null || bed == null)
        {
            return false;
        }

        if (!bed.AnyUnownedSleepingSlot)
        {
            return false;
        }

        pawn.ownership.ClaimBedIfNonMedical(bed);
        return true;
    }

    public static void releaseBed(Building_Bed bed, Pawn pawn)
    {
        if (pawn != null && bed != null)
        {
            pawn.ownership.UnclaimBed();
        }
    }
}