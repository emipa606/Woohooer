using Verse;

namespace DarkIntentionsWoohoo.mod.settings;

internal class WoohooModSettings : ModSettings
{
    public static int minAITicks;
    public static bool allowAIWoohoo;
    public static bool restrictToAdults;
    public static bool sameGender;
    public static float familyWeight;
    public static float lovedItChance;
    public static float woohooBabyChildChance;
    public static float woohooChildChance;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref woohooChildChance, "woohooChildChance", 0.01f);
        Scribe_Values.Look(ref woohooBabyChildChance, "woohooBabyChildChance", 0.5f);
        Scribe_Values.Look(ref sameGender, "sameGender", true);
        Scribe_Values.Look(ref familyWeight, "familyWeight", 0.25f);
        Scribe_Values.Look(ref allowAIWoohoo, "allowAIWoohoo", true);
        Scribe_Values.Look(ref restrictToAdults, "restrictToAdults");
        Scribe_Values.Look(ref minAITicks, "minAIWoohoo", 2500);
        Scribe_Values.Look(ref lovedItChance, "lovedItChance", 1f);
    }
}