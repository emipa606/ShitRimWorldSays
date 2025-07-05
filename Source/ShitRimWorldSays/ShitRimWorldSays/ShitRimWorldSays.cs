using HarmonyLib;
using Mlie;
using UnityEngine;
using Verse;

namespace ShitRimWorldSays;

public class ShitRimWorldSays : Mod
{
    public static string currentVersion;

    public ShitRimWorldSays(ModContentPack content)
        : base(content)
    {
        Instance = this;
        GetSettings<Settings>();
        TipDatabase.FetchNewQuotes();
        new Harmony("Fluffy.ShitRimWorldSays").PatchAll();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    private static ShitRimWorldSays Instance { get; set; }

    public static Settings Settings => Instance.GetSettings<Settings>();

    public override string SettingsCategory()
    {
        return I18n.ShitRimWorldSays;
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
        Settings.DoWindowContents(canvas);
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        TipDatabase.Notify_TipsUpdated();
    }
}