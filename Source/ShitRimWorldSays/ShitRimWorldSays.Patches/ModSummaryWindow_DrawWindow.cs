using HarmonyLib;
using UnityEngine;
using Verse;

namespace ShitRimWorldSays.Patches;

[HarmonyPatch(typeof(ModSummaryWindow), nameof(ModSummaryWindow.DrawWindow))]
public static class ModSummaryWindow_DrawWindow
{
    public static void Prefix(ref Vector2 offset)
    {
        offset = GameplayTipWindow_DrawWindow.bottomLeft + new Vector2(0f, 17f);
    }
}