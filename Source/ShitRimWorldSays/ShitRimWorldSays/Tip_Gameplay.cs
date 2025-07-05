using UnityEngine;
using Verse;

namespace ShitRimWorldSays;

public class Tip_Gameplay : Tip
{
    private string tip;

    public static implicit operator Tip_Gameplay(string tip)
    {
        return new Tip_Gameplay
        {
            tip = tip
        };
    }

    public static implicit operator string(Tip_Gameplay tip)
    {
        return tip.tip;
    }

    public override void Draw(Rect rect)
    {
        Text.Font = GameFont.Small;
        Text.Anchor = TextAnchor.MiddleCenter;
        Widgets.Label(rect.ContractedBy(margin), tip);
        Text.Anchor = TextAnchor.UpperLeft;
    }

    public override float Height(int width)
    {
        return 60f;
    }
}