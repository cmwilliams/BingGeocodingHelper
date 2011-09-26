using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace BingGeocoder
{
    public class BingGeocoderClient
    {
        private string API_KEY = "";

        public BingGeocoderClient(string BingKey)
        {
            API_KEY = BingKey;
        }

        public BingGeocoderResult Geocode(string street, string city, string state, string zipcode)
        {
            BingGeocoderResult result = new BingGeocoderResult()
                                            {
                                                Confidence = "No results",
                                                Latitude = null,
                                                Longitude = null,
                                                ErrorMessage = null
                                            };

            try
            {
                var address = state.Trim() + "/" + zipcode.Trim() + "/" + city.Trim() + "/" + street.Trim();
                string req = "http://dev.virtualearth.net/REST/v1/Locations/US/" + address + "?output=json&key=" +
                             API_KEY;
                HttpWebRequest WebReq = (HttpWebRequest) WebRequest.Create(req);
                WebReq.Method = "GET";

                HttpWebResponse WebResp = (HttpWebResponse) WebReq.GetResponse();
                Stream WebRespStream = WebResp.GetResponseStream();
                StreamReader WebStreamReader = new StreamReader(WebRespStream);

                string respJson = WebStreamReader.ReadToEnd().ToString();
                JObject JsonObject = JObject.Parse(respJson);
                JArray RespArray = (JArray) JsonObject["resourceSets"];
                string respStatus = JsonObject["statusCode"].ToString().Trim();

                JArray resultArray = (JArray) RespArray[0]["resources"];
                if (respStatus == "200" && resultArray.Count >= 1)
                {
                    JArray geoArray = (JArray) resultArray[0]["point"]["coordinates"];
                    string confidence = resultArray[0]["confidence"].ToString();
                    result.Confidence = confidence;
                    result.Latitude = geoArray[0].ToString();
                    result.Longitude = geoArray[1].ToString();
                }

                return result;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                return result;
            }


        }

        public BingGeocoderResult Geocode(string query)
        {
            BingGeocoderResult result = new BingGeocoderResult()
                                            {
                                                Confidence = "No results",
                                                Latitude = null,
                                                Longitude = null,
                                                ErrorMessage = null
                                            };

            try
            {
                string req = "http://dev.virtualearth.net/REST/v1/Locations/" + query + "?output=json&key=" + API_KEY;
                HttpWebRequest WebReq = (HttpWebRequest) WebRequest.Create(req);
                WebReq.Method = "GET";

                HttpWebResponse WebResp = (HttpWebResponse) WebReq.GetResponse();
                Stream WebRespStream = WebResp.GetResponseStream();
                StreamReader WebStreamReader = new StreamReader(WebRespStream);

                string respJson = WebStreamReader.ReadToEnd().ToString();
                JObject JsonObject = JObject.Parse(respJson);
                JArray RespArray = (JArray) JsonObject["resourceSets"];
                string respStatus = JsonObject["statusCode"].ToString().Trim();

                JArray resultArray = (JArray) RespArray[0]["resources"];
                if (respStatus == "200" && resultArray.Count >= 1)
                {
                    JArray geoArray = (JArray) resultArray[0]["point"]["coordinates"];
                    string confidence = resultArray[0]["confidence"].ToString();
                    result.Confidence = confidence;
                    result.Latitude = geoArray[0].ToString();
                    result.Longitude = geoArray[1].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                return result;
            }

        }
    }
}
