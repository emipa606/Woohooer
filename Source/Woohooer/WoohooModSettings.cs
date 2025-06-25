using Verse;

namespace DarkIntentionsWoohoo.mod.settings;

internal class WoohooModSettings : ModSettings
{
    public bool AllowAIWoohoo;
    public float FamilyWeight;
    public float LovedItChance;
    public int MinAITicks;
    public bool RestrictToAdults;
    public bool SameGender;
    public float WoohooBabyChildChance;
    public float WoohooChildChance;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref WoohooChildChance, "woohooChildChance", 0.01f);
        Scribe_Values.Look(ref WoohooBabyChildChance, "woohooBabyChildChance", 0.5f);
        Scribe_Values.Look(ref SameGender, "sameGender", true);
        Scribe_Values.Look(ref FamilyWeight, "familyWeight", 0.25f);
        Scribe_Values.Look(ref AllowAIWoohoo, "allowAIWoohoo", true);
        Scribe_Values.Look(ref RestrictToAdults, "restrictToAdults");
        Scribe_Values.Look(ref MinAITicks, "minAIWoohoo", 2500);
        Scribe_Values.Look(ref LovedItChance, "lovedItChance", 1f);
    }
}