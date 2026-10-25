using UnityEngine;

namespace ShitRimWorldSays;

public static class Utilities
{
    extension(Rect rect)
    {
        public Rect ContractedBy(Vector2 margin)
        {
            return new Rect(rect.min + margin, rect.size - (2f * margin));
        }

        public Vector2 BottomLeft()
        {
            return new Vector2(rect.xMin, rect.yMax);
        }
    }

    public static string Italic(this string msg)
    {
        return $"<i>{msg}</i>";
    }

    public static Color Darken(this Color color, float amount)
    {
        return color * (1f - amount);
    }
}