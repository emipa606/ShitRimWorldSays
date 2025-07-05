using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShitRimWorldSays;

public class Post
{
    private static readonly Regex urlRegex =
        new("reddit\\.com\\/r\\/(?<sub>\\w+)\\/comments\\/(?<post>(\\w|\\d)+)\\/.+?\\/(?<reply>(\\w|\\d)+)?");

    public string title { get; set; }

    public string author { get; set; }

    public string url { get; set; }

    public string name { get; set; }

    public string permalink { get; set; }

    public int score { get; set; }

    public string selftext { get; set; }

    public bool is_self { get; set; }

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
            using var http = new WebClient();
            http.Headers.Add("user-agent", "shit-rimworld-says rimworld mod v0.1");
            var reply = JsonConvert.DeserializeObject<Reply>(
                JObject.Parse(await http.DownloadStringTaskAsync(address))["data"]["children"][0]["data"].ToString());
            var tipQuote = new Tip_Quote(reply.author, reply.body, reply.permalink, score);
            return tipQuote.body == "[deleted]" ? null : tipQuote;
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
            using var http = new WebClient();
            http.Headers.Add("user-agent", "shit-rimworld-says rimworld mod v0.1");
            var post = JsonConvert.DeserializeObject<Post>(
                JObject.Parse(await http.DownloadStringTaskAsync(address))["data"]["children"][0]["data"].ToString());
            var tipQuote = new Tip_Quote(post.author, post.is_self ? post.selftext : post.title, post.permalink,
                score);
            return tipQuote.body == "[deleted]" ? null : tipQuote;
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