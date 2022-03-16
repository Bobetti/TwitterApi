using OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



//source: https://twittercommunity.com/t/using-post-2-tweets-in-c-i-simply-cant-do-it/167754

namespace TwitterApi
{
    public class TwitterAPI_5
    {
        private readonly string _oAuthConsumerKey;
        private readonly string _oAuthConsumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;        
        private readonly string _endPoint;


        public TwitterAPI_5(string oAuthConsumerKey, string oAuthConsumerSecret, string accessToken, string accessTokenSecret, string endPoint)
        {
            _oAuthConsumerKey = oAuthConsumerKey;
            _oAuthConsumerSecret = oAuthConsumerSecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
            _endPoint = endPoint;
        }



        public void Tweet(string message)
        {
            OAuthRequest client = OAuthRequest.ForProtectedResource("POST", _oAuthConsumerKey, _oAuthConsumerSecret, _accessToken, _accessTokenSecret);
            client.RequestUrl = _endPoint;
            string auth = client.GetAuthorizationHeader();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(client.RequestUrl);
            request.Method = "POST";
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("Authorization", auth);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("{\"text\":"+ message + "");
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }


    }
}
