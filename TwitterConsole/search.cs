using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Parameters;
using System.Text;

public static class TwitterSearch
{
    public static void TwitterMain()
    {
        Auth.SetUserCredentials("CqaKxD9lsqpVAMK1glAG5EBnE",
                        "wglFk5bWsF3zaHveBVb2hAJZhtJtBYXREAt2U9CWvzd5mS6YXh",
                        "KOBc04INtFXN2sEAHlL2hRTodkXTjqaCFx1UpBcM",
                        "2ie1KHu3lDd6oz1tQLwIH2BEl48P9IAaF5woHkm8fCi44");

        //var user = User.GetLoggedUser();

        //Console.WriteLine("Logged in as {0} (@{1})", user.Name, user.ScreenName);

        SearchTweetsTaggedWithGeo(PromptForSearchQuery(), 38.029722, -84.494722)
            .Tweets
            .Select(tweet => FormatTweet(tweet))
            .ForEach(tweet => Console.WriteLine(tweet));
    }

    static string PromptForSearchQuery()
    {
        Console.Write("Enter a search query: ");
        var searchQuery = "nyc"; // Console.ReadLine();
        //Console.WriteLine(); Console.WriteLine();
        return searchQuery;
    }

    // Note: The latitude/longitude order here is reversed compared to the various
    //       signatures in Tweetinvi (see the GeoCode constructor below for example),
    //       for ease of copy-and-pasting coordinates from the web
    static ISearchResult SearchTweetsTaggedWithGeo(string query,
                                                   double latitude,
                                                   double longitude,
                                                   double radius = 15,
                                                   DistanceMeasure distanceMeasure = DistanceMeasure.Miles)
    {
        var searchParameters = new TweetSearchParameters(query)
        {
            GeoCode = new GeoCode(longitude, latitude, radius, distanceMeasure),
            Lang = Language.English,
            SearchType = SearchResultType.Recent,
            MaximumNumberOfResults = 15,
            Locale = "new york"
        };

        return Search.SearchTweetsWithMetadata(searchParameters);
    }

    static string FormatTweet(ITweet tweet)
    {
        var builder = new StringBuilder();
        builder.AppendLine(string.Format("{0} (@{1}) - {2}",
                           tweet.CreatedBy.Name,
                           tweet.CreatedBy.ScreenName,
                           tweet.CreatedAt.ToLocalTime().ToString("MM/dd/yy h:mm:ss tt")));
        builder.AppendLine(tweet.Text);
        return builder.ToString();
    }
}