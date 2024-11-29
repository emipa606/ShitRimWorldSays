using Verse;

namespace ShitRimWorldSays;

public class I18n
{
    public static readonly string ShitRimWorldSays = Translate("ShitRimWorldSays");

    public static readonly string ReplaceGameTips = Translate("ReplaceGameTips");

    public static readonly string ReplaceGameTipsTooltip = Translate("ReplaceGameTips.Tooltip");

    public static readonly string MinimumKarma = Translate("MinimumKarma");

    public static readonly string TipsOnMainMenu = Translate("TipsOnMainMenu");

    public static readonly string TipsOnMainMenuTooltip = Translate("TipsOnMainMenu.Tooltip");

    private static string Translate(string key, params NamedArgument[] args)
    {
        return Key(key).Translate(args).Resolve();
    }

    private static string Key(string key)
    {
        return $"Fluffy.ShitRimWorldSays.{key}";
    }
}