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

    public WoohooMod(ModContentPack content)
        : base(content)
    {
        var harmony = new Harmony("DarkIntentionsWoohoo.mod.settings.harmony");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public static void LogMessage(string message)
    {
        if (!Prefs.DevMode)
        {
            return;
        }

        Log.Message($"[Wohooer]: {message}");
    }

    public override string SettingsCategory()
    {
        return "Woohooer!";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);

        listing_Standard.Label(
            "Whohooer.woohooChildChance".Translate(WoohooModSettings.woohooChildChance.ToStringPercent()));
        WoohooModSettings.woohooChildChance = listing_Standard.Slider(WoohooModSettings.woohooChildChance, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.Label(
            "Whohooer.woohooBabyChildChance".Translate(WoohooModSettings.woohooBabyChildChance.ToStringPercent()));
        WoohooModSettings.woohooBabyChildChance =
            listing_Standard.Slider(WoohooModSettings.woohooBabyChildChance, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.Label("Whohooer.familyWeight".Translate(WoohooModSettings.familyWeight.ToStringPercent()));
        WoohooModSettings.familyWeight = listing_Standard.Slider(WoohooModSettings.familyWeight, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.Label("Whohooer.lovedItChance".Translate(WoohooModSettings.lovedItChance.ToStringPercent()));
        WoohooModSettings.lovedItChance = listing_Standard.Slider(WoohooModSettings.lovedItChance, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.CheckboxLabeled("Whohooer.sameGender".Translate(), ref WoohooModSettings.sameGender);
        listing_Standard.CheckboxLabeled("Whohooer.restrictToAdults".Translate(),
            ref WoohooModSettings.restrictToAdults);
        listing_Standard.Gap();

        listing_Standard.CheckboxLabeled("Whohooer.allowAIWoohoo".Translate(), ref WoohooModSettings.allowAIWoohoo);
        if (WoohooModSettings.allowAIWoohoo)
        {
            listing_Standard.Label("Whohooer.minAITicks".Translate(WoohooModSettings.minAITicks.ToStringTicksToDays()));
            WoohooModSettings.minAITicks = (int)listing_Standard.Slider(WoohooModSettings.minAITicks, 1000f, 1000000f);
        }

        listing_Standard.GapLine();
        listing_Standard.Label("Whohooer.Credit".Translate());
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("Whohooer.Version".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        base.DoSettingsWindowContents(inRect);
    }
}