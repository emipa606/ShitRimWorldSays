using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShitRimWorldSays;

public class Post
{
    private static readonly Regex urlRegex =
        new("reddit\\.com\\/r\\/(?<sub>\\w+)\\/comments\\/(?<post>(\\w|\\d)+)\\/.+?\\/(?<reply>(\\w|\\d)+)?",
            RegexOptions.None, TimeSpan.FromSeconds(1));

    public string title { get; init; }

    public string author { get; init; }

    public string url { get; init; }

    public string name { get; init; }

    public string permalink { get; init; }

    public int score { get; init; }

    public string selftext { get; init; }

    public bool is_self { get; init; }

    public async Task<Tip_Quote> getQuote()
    {
        if (string.IsNullOrEmpty(url))
        {
            return (author, title, permalink, score);
        }

        var match = urlRegex.Match(url);
        if (!match.Success)
        {
            return (author, title, permalink, score);
        }

        var value = match.Groups["sub"].Value;
        var value2 = match.Groups["post"].Value;
        var value3 = match.Groups["reply"].Value;
        if (value3 != string.Empty)
        {
            return await getQuoteFromReply(value, value3);
        }

        return await getQuoteFromPost(value, value2);
    }

    private async Task<Tip_Quote> getQuoteFromReply(string sub, string replyId)
    {
        var address = $"https://reddit.com/r/{sub}/api/info.json?id=t1_{replyId}";
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.ParseAdd("shit-rimworld-says rimworld mod v0.1");
            using var document = JsonDocument.Parse(await http.GetStringAsync(address));
            var reply = JsonSerializer.Deserialize<Reply>(document.RootElement
                .GetProperty("data").GetProperty("children")[0].GetProperty("data").GetRawText());
            var tipQuote = new Tip_Quote(reply?.author, reply?.body, reply?.permalink, score);
            return tipQuote.body is "[removed]" or "[deleted]" ? null : tipQuote;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task<Tip_Quote> getQuoteFromPost(string sub, string postId)
    {
        var address = $"https://reddit.com/r/{sub}/api/info.json?id=t3_{postId}";
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.ParseAdd("shit-rimworld-says rimworld mod v0.1");
            using var document = JsonDocument.Parse(await http.GetStringAsync(address));
            var post = JsonSerializer.Deserialize<Post>(document.RootElement
                .GetProperty("data").GetProperty("children")[0].GetProperty("data").GetRawText());
            var tipQuote = new Tip_Quote(post?.author, post is { is_self: true } ? post.selftext : post?.title,
                post?.permalink,
                score);
            return tipQuote.body is "[removed]" or "[deleted]" ? null : tipQuote;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override string ToString()
    {
        return $"{title}{(is_self ? "\n" + selftext : "")} \n\t - {author}\n{url}({name})";
    }
}