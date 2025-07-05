using System.Diagnostics;

namespace ShitRimWorldSays;

internal static class Log
{
    private static void message(string msg)
    {
        Verse.Log.Message($"ShitRimWorldSays :: {msg}");
    }

    [Conditional("DEBUG")]
    public static void Debug(string msg)
    {
        message(msg);
    }
}