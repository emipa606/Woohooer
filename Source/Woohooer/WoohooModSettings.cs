using Verse;

namespace DarkIntentionsWoohoo.mod.settings;

internal class WoohooModSettings : ModSettings
{
    private static readonly float base_woohooChildChance = 0.01f;

    private static readonly float base_familyWeight = 0.25f;

    private static readonly float base_woohooBabyChildChance = 0.5f;

    private static readonly bool base_sameGender = true;

    public bool allowAIWoohoo;

    public float familyWeight;

    public float lovedItChance;

    public int minAITicks;
    public bool restrictToAdults;

    public bool sameGender = base_sameGender;

    public float woohooBabyChildChance = base_woohooBabyChildChance;
    public float woohooChildChance = base_woohooChildChance;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref woohooChildChance, "woohooChildChance", base_woohooChildChance);
        Scribe_Values.Look(ref woohooBabyChildChance, "woohooBabyChildChance", base_woohooBabyChildChance);
        Scribe_Values.Look(ref sameGender, "sameGender", base_sameGender);
        Scribe_Values.Look(ref familyWeight, "familyWeight", base_familyWeight);
        Scribe_Values.Look(ref allowAIWoohoo, "allowAIWoohoo", true);
        Scribe_Values.Look(ref restrictToAdults, "restrictToAdults");
        Scribe_Values.Look(ref minAITicks, "minAIWoohoo", 2500);
        Scribe_Values.Look(ref lovedItChance, "lovedItChance", 1f);
    }
}