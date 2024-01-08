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
    public static WoohooMod instance;

    public WoohooMod(ModContentPack content)
        : base(content)
    {
        instance = this;
        var harmony = new Harmony("DarkIntentionsWoohoo.mod.settings.harmony");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        Settings = GetSettings<WoohooModSettings>();
    }

    internal WoohooModSettings Settings { get; }

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
            "Whohooer.woohooChildChance".Translate(Settings.woohooChildChance.ToStringPercent()));
        Settings.woohooChildChance = listing_Standard.Slider(Settings.woohooChildChance, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.Label(
            "Whohooer.woohooBabyChildChance".Translate(Settings.woohooBabyChildChance.ToStringPercent()));
        Settings.woohooBabyChildChance =
            listing_Standard.Slider(Settings.woohooBabyChildChance, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.Label("Whohooer.familyWeight".Translate(Settings.familyWeight.ToStringPercent()));
        Settings.familyWeight = listing_Standard.Slider(Settings.familyWeight, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.Label("Whohooer.lovedItChance".Translate(Settings.lovedItChance.ToStringPercent()));
        Settings.lovedItChance = listing_Standard.Slider(Settings.lovedItChance, 0f, 1f);
        listing_Standard.Gap();

        listing_Standard.CheckboxLabeled("Whohooer.sameGender".Translate(), ref Settings.sameGender);
        listing_Standard.CheckboxLabeled("Whohooer.restrictToAdults".Translate(),
            ref Settings.restrictToAdults);
        listing_Standard.Gap();

        listing_Standard.CheckboxLabeled("Whohooer.allowAIWoohoo".Translate(), ref Settings.allowAIWoohoo);
        if (Settings.allowAIWoohoo)
        {
            listing_Standard.Label("Whohooer.minAITicks".Translate(Settings.minAITicks.ToStringTicksToDays()));
            Settings.minAITicks = (int)listing_Standard.Slider(Settings.minAITicks, 1000f, 1000000f);
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