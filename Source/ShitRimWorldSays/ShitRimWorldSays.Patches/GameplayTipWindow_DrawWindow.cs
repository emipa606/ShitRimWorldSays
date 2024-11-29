using HarmonyLib;
using UnityEngine;
using Verse;

namespace ShitRimWorldSays.Patches;

[HarmonyPatch(typeof(GameplayTipWindow), nameof(GameplayTipWindow.DrawWindow))]
public static class GameplayTipWindow_DrawWindow
{
    public static readonly int WindowWidth = 776;

    public static Vector2 bottomLeft;

    public static bool Prefix(Vector2 offset, bool useWindowStack)
    {
        var tip = TipDatabase.CurrentTip;
        var canvas = new Rect(offset.x, offset.y, WindowWidth, tip.Height(WindowWidth));
        bottomLeft = canvas.BottomLeft();
        if (useWindowStack)
        {
            Find.WindowStack.ImmediateWindow(62893997, canvas, WindowLayer.Super,
                delegate { tip.Draw(canvas.AtZero()); });
        }
        else
        {
            Widgets.DrawShadowAround(canvas);
            Widgets.DrawWindowBackground(canvas);
            tip.Draw(canvas);
        }

        return false;
    }
}