using UnityEngine;
using Verse;

namespace ShitRimWorldSays;

[StaticConstructorOnStartup]
public static class Resources
{
    public static readonly Texture2D Refresh = ContentFinder<Texture2D>.Get("UI/Icons/refresh");
}