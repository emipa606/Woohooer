using System;
using System.Linq;
using Children;
using RimWorld;
using Verse;

namespace DarkIntentionsWoohoo;

internal class ChildrenCrossMod
{
    public static readonly HediffDef PregnancyDiscovered =
        DefDatabase<HediffDef>.GetNamedSilentFail("PregnancyDiscovered");

    public static bool isChildrenModOn()
    {
        return PregnancyDiscovered != null;
    }

    public static void Mated(Pawn donor, Pawn womb)
    {
        var gender = womb.gender;
        womb.gender = Gender.Female;
        var gender2 = donor.gender;
        donor.gender = Gender.Male;
        try
        {
            Type type = null;
            var assembly = typeof(BackstoryDef).Assembly;
            foreach (var item in from x in assembly.GetTypes()
                     where x.FullName != null && x.FullName.Contains("MorePawnUtil")
                     select x)
            {
                if (item == null)
                {
                    continue;
                }

                type = item;
                break;
            }

            if (type == null)
            {
                throw new Exception(
                    "Couldnt find Childern.MorePawnUtils in the assembly<--- this is bad practice to control flow with an exception. Dont tell~");
            }

            var foundLove = false;
            using (var enumerator2 = (from aMethod in type.GetMethods()
                       where aMethod.Name.Contains("Loved")
                       select aMethod).GetEnumerator())
            {
                if (enumerator2.MoveNext())
                {
                    var current2 = enumerator2.Current;
                    var obj = current2?.Invoke(null, new object[] { donor, womb, true });
                    if (obj != null)
                    {
                        foundLove = true;
                    }
                }
            }

            if (!foundLove)
            {
                throw new Exception("Coundnt find the Loved method in Children");
            }
        }
        catch (Exception)
        {
            Mate.DefaultMate(donor, womb);
        }
        finally
        {
            donor.gender = gender2;
            womb.gender = gender;
        }
    }
}