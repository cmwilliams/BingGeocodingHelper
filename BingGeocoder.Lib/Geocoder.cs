using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace BingGeocoder
{
    public class BingGeocoderClient
    {
        private readonly string _apiKey = "";

        public BingGeocoderClient(string bingKey)
        {
            _apiKey = bingKey;
        }


        private static BingGeocoderResult MakeRequest(string request)
        {
            var result = new BingGeocoderResult
                             {
                                 Confidence = "No results",
                                 Latitude = null,
                                 Longitude = null,
                                 ErrorMessage = null
                             };

            try
            {
                var webReq = (HttpWebRequest) WebRequest.Create(request);
                webReq.Method = "GET";

                var webResp = (HttpWebResponse) webReq.GetResponse();
                var webRespStream = webResp.GetResponseStream();
                var webStreamReader = new StreamReader(webRespStream);

                var respJson = webStreamReader.ReadToEnd();
                var jsonObject = JObject.Parse(respJson);
                var respArray = (JArray) jsonObject["resourceSets"];
                var respStatus = jsonObject["statusCode"].ToString().Trim();

                var resultArray = (JArray) respArray[0]["resources"];
                if (respStatus == "200" && resultArray.Count >= 1)
                {
                    var geoArray = (JArray) resultArray[0]["point"]["coordinates"];
                    var confidence = resultArray[0]["confidence"].ToString();
                    result.Confidence = confidence;
                    result.Latitude = geoArray[0].ToString();
                    result.Longitude = geoArray[1].ToString();
                }

                return result;
            }
            catch (Exception e)
            {

                result.ErrorMessage = e.Message;
                return result;
            }
        }

        public BingGeocoderResult Geocode(string street, string city, string state, string zipcode)
        {
            var address = state.Trim() + "/" + zipcode.Trim() + "/" + city.Trim() + "/" + street.Trim();
            var request = "http://dev.virtualearth.net/REST/v1/Locations/US/" + address + "?output=json&key=" + _apiKey;
            return MakeRequest(request);
        }

        public BingGeocoderResult Geocode(string query)
        {
            var request = "http://dev.virtualearth.net/REST/v1/Locations/" + query + "?output=json&key=" + _apiKey;
            return MakeRequest(request);
        }
    }
}
