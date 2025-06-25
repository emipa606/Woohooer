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
    public static WoohooMod Instance;

    public WoohooMod(ModContentPack content)
        : base(content)
    {
        Instance = this;
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
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);

        listingStandard.Label(
            "Whohooer.woohooChildChance".Translate(Settings.WoohooChildChance.ToStringPercent()));
        Settings.WoohooChildChance = listingStandard.Slider(Settings.WoohooChildChance, 0f, 1f);
        listingStandard.Gap();

        listingStandard.Label(
            "Whohooer.woohooBabyChildChance".Translate(Settings.WoohooBabyChildChance.ToStringPercent()));
        Settings.WoohooBabyChildChance =
            listingStandard.Slider(Settings.WoohooBabyChildChance, 0f, 1f);
        listingStandard.Gap();

        listingStandard.Label("Whohooer.familyWeight".Translate(Settings.FamilyWeight.ToStringPercent()));
        Settings.FamilyWeight = listingStandard.Slider(Settings.FamilyWeight, 0f, 1f);
        listingStandard.Gap();

        listingStandard.Label("Whohooer.lovedItChance".Translate(Settings.LovedItChance.ToStringPercent()));
        Settings.LovedItChance = listingStandard.Slider(Settings.LovedItChance, 0f, 1f);
        listingStandard.Gap();

        listingStandard.CheckboxLabeled("Whohooer.sameGender".Translate(), ref Settings.SameGender);
        listingStandard.CheckboxLabeled("Whohooer.restrictToAdults".Translate(),
            ref Settings.RestrictToAdults);
        listingStandard.Gap();

        listingStandard.CheckboxLabeled("Whohooer.allowAIWoohoo".Translate(), ref Settings.AllowAIWoohoo);
        if (Settings.AllowAIWoohoo)
        {
            listingStandard.Label("Whohooer.minAITicks".Translate(Settings.MinAITicks.ToStringTicksToDays()));
            Settings.MinAITicks = (int)listingStandard.Slider(Settings.MinAITicks, 1000f, 1000000f);
        }

        listingStandard.GapLine();
        listingStandard.Label("Whohooer.Credit".Translate());
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("Whohooer.Version".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }
}