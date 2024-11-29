using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace ShitRimWorldSays.Patches;

[HarmonyPatch(typeof(MainMenuDrawer), nameof(MainMenuDrawer.MainMenuOnGUI))]
public class MainMenuDrawer_MainMenuOnGui
{
    public static void Postfix()
    {
        if (!ShitRimWorldSays.Settings.tipsOnMainMenu)
        {
            return;
        }

        var num = Mathf.Min(UI.screenWidth, 400);
        var num2 = TipDatabase.CurrentTip.Height(num);
        var num3 = (UI.screenWidth - num) / 2;
        var y = UI.screenHeight - num2 - 8f;
        var canvas = new Rect(num3, y, num, num2);
        Find.WindowStack.ImmediateWindow(62893997, canvas, WindowLayer.Super,
            delegate { TipDatabase.CurrentTip.Draw(canvas.AtZero()); });
    }
}