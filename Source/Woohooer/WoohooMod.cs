using System.Reflection;
using HarmonyLib;
using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace DarkIntentionsWoohoo.mod.settings;

internal class WoohooMod : Mod
{
    private static string currentVersion;
    private readonly WoohooModSettings settings;

    public WoohooMod(ModContentPack content)
        : base(content)
    {
        settings = GetSettings<WoohooModSettings>();
        WoohooSettingHelper.latest = settings;
        var harmony = new Harmony("DarkIntentionsWoohoo.mod.settings.harmony");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(ModLister.GetActiveModWithIdentifier("Mlie.Woohooer"));
    }

    public override string SettingsCategory()
    {
        return "Woohooer!";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);

        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);

        listing_Standard.Label("Whohooer.woohooChildChance".Translate(settings.woohooChildChance.ToStringPercent()));
        settings.woohooChildChance = listing_Standard.Slider(settings.woohooChildChance, 0f, 1f);
        listing_Standard.Gap();
        listing_Standard.Label(
            "Whohooer.woohooBabyChildChance".Translate(settings.woohooBabyChildChance.ToStringPercent()));
        settings.woohooBabyChildChance = listing_Standard.Slider(settings.woohooBabyChildChance, 0f, 1f);
        listing_Standard.Gap();
        listing_Standard.Label("Whohooer.familyWeight".Translate(settings.familyWeight.ToStringPercent()));
        settings.familyWeight = listing_Standard.Slider(settings.familyWeight, 0f, 1f);
        listing_Standard.Gap();
        listing_Standard.Label("Whohooer.lovedItChance".Translate(settings.lovedItChance.ToStringPercent()));
        settings.lovedItChance = listing_Standard.Slider(settings.lovedItChance, 0f, 1f);
        listing_Standard.Gap();
        listing_Standard.CheckboxLabeled("Whohooer.sameGender".Translate(),
            ref settings.sameGender);

        listing_Standard.CheckboxLabeled("Whohooer.allowAIWoohoo".Translate(),
            ref settings.allowAIWoohoo);
        if (settings.allowAIWoohoo)
        {
            listing_Standard.Label("Whohooer.minAITicks".Translate(settings.minAITicks.ToStringTicksToDays()));
            settings.minAITicks = (int)listing_Standard.Slider(settings.minAITicks, 1000f, 1000000f);
        }

        listing_Standard.Gap();
        listing_Standard.Label("Whohooer.Credit".Translate());
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("Whohooer.Version".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}