using UnityEngine;
using Verse;

namespace ShitRimWorldSays;

public class Settings : ModSettings
{
    private string _minimumKarmaBuffer;
    public TipDatabase database = new();

    public int minimumKarma = 150;

    public bool replaceGameTips = true;

    public bool tipsOnMainMenu = true;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Deep.Look(ref database, "database");
        Scribe_Values.Look(ref replaceGameTips, "replaceGameTips");
        Scribe_Values.Look(ref tipsOnMainMenu, "tipsOnMainMenu", true);
        Scribe_Values.Look(ref minimumKarma, "minimumKarma", 150);
    }

    public void DoWindowContents(Rect canvas)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(canvas);
        listingStandard.CheckboxLabeled(I18n.ReplaceGameTips, ref replaceGameTips, I18n.ReplaceGameTipsTooltip);
        listingStandard.CheckboxLabeled(I18n.TipsOnMainMenu, ref tipsOnMainMenu, I18n.TipsOnMainMenuTooltip);
        listingStandard.TextFieldNumericLabeled(I18n.MinimumKarma, ref minimumKarma, ref _minimumKarmaBuffer, 0f,
            999f);
        if (ShitRimWorldSays.currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("Fluffy.ShitRimWorldSays.CurrentVersion".Translate(ShitRimWorldSays.currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}