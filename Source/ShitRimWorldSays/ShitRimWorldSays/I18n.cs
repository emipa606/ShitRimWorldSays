using Verse;

namespace ShitRimWorldSays;

public class I18n
{
    public static readonly string ShitRimWorldSays = translate("ShitRimWorldSays");

    public static readonly string ReplaceGameTips = translate("ReplaceGameTips");

    public static readonly string ReplaceGameTipsTooltip = translate("ReplaceGameTips.Tooltip");

    public static readonly string MinimumKarma = translate("MinimumKarma");

    public static readonly string TipsOnMainMenu = translate("TipsOnMainMenu");

    public static readonly string TipsOnMainMenuTooltip = translate("TipsOnMainMenu.Tooltip");

    private static string translate(string key, params NamedArgument[] args)
    {
        return Key(key).Translate(args).Resolve();
    }

    private static string Key(string key)
    {
        return $"Fluffy.ShitRimWorldSays.{key}";
    }
}