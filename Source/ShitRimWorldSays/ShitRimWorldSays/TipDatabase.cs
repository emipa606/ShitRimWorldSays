using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ShitRimWorldSays;

public class TipDatabase : IExposable
{
    private static HashSet<Tip_Quote> _quotes = [];

    private static List<Tip_Gameplay> _vanilla;

    private static List<Tip> _tips;

    private static int _currentTipIndex;

    private static float _lastUpdateTime;

    private static List<Tip> Tips
    {
        get
        {
            _vanilla ??= DefDatabase<TipSetDef>.AllDefsListForReading.SelectMany(set => set.tips)
                .Select((Func<string, Tip_Gameplay>)(tip => tip)).ToList();

            if (ShitRimWorldSays.Settings.replaceGameTips)
            {
                _tips ??= ((IEnumerable<Tip>)_quotes.InRandomOrder()).ToList();
            }
            else if (_tips == null)
            {
                _tips = _quotes.Cast<Tip>().Concat(_vanilla).InRandomOrder()
                    .ToList();
            }

            return _tips;
        }
    }

    public static Tip CurrentTip
    {
        get
        {
            if (!Tips.Any())
            {
                return new Tip_Quote("Fluffy", "no quotes found", null, 999);
            }

            if (!(Time.realtimeSinceStartup - _lastUpdateTime > 17.5) && !(_lastUpdateTime < 0f))
            {
                return Tips[_currentTipIndex];
            }

            _currentTipIndex = (_currentTipIndex + 1) % Tips.Count;
            _lastUpdateTime = Time.realtimeSinceStartup;

            return Tips[_currentTipIndex];
        }
    }

    public void ExposeData()
    {
        Scribe_Collections.Look(ref _quotes, "quotes");
    }

    public static void Notify_TipsUpdated()
    {
        _tips = null;
        _vanilla = null;
        _quotes = _quotes.Where(q => q.score >= ShitRimWorldSays.Settings.minimumKarma).ToHashSet();
        _currentTipIndex = 0;
        Notify_ResetTimer(true);
    }

    public static void Notify_ResetTimer(bool force = false)
    {
        if (force || LongEventHandler.AnyEventNowOrWaiting)
        {
            _lastUpdateTime = -1f;
        }
    }

    public static async Task FetchNewQuotes()
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.ParseAdd("shit-rimworld-says rimworld mod v0.1");
            using var document = JsonDocument.Parse(await http.GetStringAsync(
                "https://reddit.com/r/ShitRimworldSays/hot/.json?limit=100"));
            var posts = new List<Post>();
            foreach (var child in document.RootElement.GetProperty("data").GetProperty("children").EnumerateArray())
            {
                var post = child.GetProperty("data").Deserialize<Post>();
                if (post != null && post.score >= ShitRimWorldSays.Settings.minimumKarma)
                {
                    posts.Add(post);
                }
            }

            var list = (await Task.WhenAll(posts.Select(post => post.getQuote())))
                .Where(quote => quote != null).ToList();
            _quotes.AddRange(list);
            ShitRimWorldSays.Settings.database = new TipDatabase();
            ShitRimWorldSays.Settings.Write();
            Notify_TipsUpdated();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}