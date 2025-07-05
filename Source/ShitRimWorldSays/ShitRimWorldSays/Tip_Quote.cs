using System;
using UnityEngine;
using Verse;

namespace ShitRimWorldSays;

public class Tip_Quote : Tip, IExposable, IEquatable<Tip_Quote>
{
    private string author;

    public string body;

    private string permalink;

    public int score;

    public Tip_Quote()
    {
    }

    public Tip_Quote(string author, string body, string permalink, int score)
    {
        this.author = author;
        this.body = body;
        this.permalink = permalink;
        this.score = score;
    }

    public bool Equals(Tip_Quote other)
    {
        if ((object)other == null)
        {
            return false;
        }

        return (object)this == other || string.Equals(permalink, other.permalink);
    }

    public void ExposeData()
    {
        Scribe_Values.Look(ref author, "author");
        Scribe_Values.Look(ref body, "body");
        Scribe_Values.Look(ref permalink, "permalink");
        Scribe_Values.Look(ref score, "score");
    }

    public static implicit operator Tip_Quote((string, string, string, int) tuple)
    {
        return new Tip_Quote(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((Tip_Quote)obj);
    }

    public override int GetHashCode()
    {
        return permalink == null ? 0 : permalink.GetHashCode();
    }

    public static bool operator ==(Tip_Quote left, Tip_Quote right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Tip_Quote left, Tip_Quote right)
    {
        return !Equals(left, right);
    }

    public override void Draw(Rect rect)
    {
        var rect2 = rect.ContractedBy(margin);
        var rect3 = rect.ContractedBy(margin);
        rect2.yMax -= 30f;
        rect3.yMin = rect2.yMax;
        Text.Font = GameFont.Small;
        Text.Anchor = TextAnchor.MiddleCenter;
        Widgets.Label(rect2, body);
        Text.Anchor = TextAnchor.LowerRight;
        GUI.color = Mouse.IsOver(rect3) ? GenUI.MouseoverColor : GenUI.MouseoverColor.Darken(0.2f);
        Widgets.Label(rect3, (" - " + author).Italic());
        if (!permalink.NullOrEmpty() && Widgets.ButtonInvisible(rect3))
        {
            Application.OpenURL($"https://reddit.com/{permalink}");
        }

        Text.Anchor = TextAnchor.UpperLeft;
        GUI.color = Color.white;
        var rect4 = new Rect(rect.xMax - 24f - 4f, rect.yMin + 4f, 24f, 24f);
        if (Mouse.IsOver(rect) && Widgets.ButtonImage(rect4, Resources.Refresh))
        {
            TipDatabase.Notify_ResetTimer(true);
        }
    }

    public override float Height(int width)
    {
        return Text.CalcHeight(body, width - (2f * margin.x)) + 30f + 36f;
    }
}