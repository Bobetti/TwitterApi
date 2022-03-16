using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi
{
    public class TwitterAPI_3
    {

        private readonly string _oAuthConsumerKey;
        private readonly string _oAuthConsumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;        
        private readonly string _endPoint;


        public TwitterAPI_3(string oAuthConsumerKey, string oAuthConsumerSecret, string accessToken, string accessTokenSecret,  string endPoint)
        {
            _oAuthConsumerKey = oAuthConsumerKey;
            _oAuthConsumerSecret = oAuthConsumerSecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;            
            _endPoint = endPoint;            
        }




        public void Tweet(string message)
        {                   

            // set the oauth version and signature method
            string oauth_version = "1.0";
            string oauth_signature_method = "HMAC-SHA1";

            // create unique request details
            string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            System.TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // create oauth signature
            string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";

            string baseString = string.Format(
                baseFormat,
                _oAuthConsumerKey,
                oauth_nonce,
                oauth_signature_method,
                oauth_timestamp, 
                _accessToken,
                oauth_version,
                Uri.EscapeDataString(message)
            );

            string oauth_signature = null;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(_oAuthConsumerSecret) + "&" + Uri.EscapeDataString(_accessTokenSecret))))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(_endPoint) + "&" + Uri.EscapeDataString(baseString))));
            }

            // create the request header
            string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

            string authorizationHeader = string.Format(
                authorizationFormat,
                Uri.EscapeDataString(_oAuthConsumerKey),
                Uri.EscapeDataString(oauth_nonce),
                Uri.EscapeDataString(oauth_signature),
                Uri.EscapeDataString(oauth_signature_method),
                Uri.EscapeDataString(oauth_timestamp),
                Uri.EscapeDataString(_accessToken),
                Uri.EscapeDataString(oauth_version)
            );

            HttpWebRequest objHttpWebRequest = (HttpWebRequest) WebRequest.Create(_endPoint);
            objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
            objHttpWebRequest.Method = "POST";
            objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            using (Stream objStream = objHttpWebRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(message));
                objStream.Write(content, 0, content.Length);
            }

            var responseResult = "";

            //success posting
            WebResponse objWebResponse = objHttpWebRequest.GetResponse();
            StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
            responseResult = objStreamReader.ReadToEnd().ToString();
        }

       
    }
}
