// See https://aka.ms/new-console-template for more information


using TwitterApi;

var oAuthConsumerKey = "";
var oAuthConsumerSecret = "";
var accessToken = "";
var accessTokenSecret = "";
var bearer = "";
string endPoint = "https://api.twitter.com/2/tweets";
string endPoint_1 = "https://api.twitter.com/1.1/statuses/update.json";



string tweet = "Hellow #world";


/// TwitterAPI_1

try
{
    //var twitAp_1 = new TwitterAPI_1(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, endPoint);
    //twitAp_1.SendTweet(tweet);
}
catch (Exception ex)
{
    //Unauthorized
}

/// -----------



/// TwitterAPI_2

try
{
    //var twitAp_2 = new TwitterAPI_2(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, endPoint_1);
    //var response = await twitAp_2.Tweet(tweet);
}
catch (Exception ex)
{
    //readonly app can't post
}

/// -----------



/// TwitterAPI_3

try
{
    //var twitAp_3 = new TwitterAPI_3(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, endPoint_1);
    //twitAp_3.Tweet(tweet);
}
catch(Exception ex)
{
    //Unauthorized
}

/// -----------


/// TwitterAPI_4

try
{
    //var twitAp_4 = new TwitterAPI_4(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, bearer, endPoint_1);
    //var resp = twitAp_4.PublishToTwitter(tweet);
}
catch(Exception ex)
{
    //readonly app can't post
}

/// -----------



/// TwitterAPI_5

try
{
    var twitAp_5 = new TwitterAPI_5(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, endPoint);
    twitAp_5.Tweet(tweet);
}
catch (Exception ex)
{
    //forbidden
}

/// -----------




















