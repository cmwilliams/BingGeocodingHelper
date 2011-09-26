using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BingGeocoder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new BingGeocoderResult();
            result = Geocode("70 Dundee Pl", "Sharpsburg", "GA", "30277");
        }


        public static BingGeocoderResult Geocode(string street = "", string city = "", string state = "", string zipcode = "")
        {
            BingGeocoderResult result = new BingGeocoderResult() { Confidence = "No results", Latitude = null, Longitude = null, ErrorMessage = null };
            string API_KEY = "AnOcTOfyJGgVzJL7eL9CEvbIUIoC7QgUQatnn7kGc_jXVX3ZgmC6NLsgZqifCt67";
            try
            {
                var address = state.Trim() + "/" + zipcode.Trim() + "/" + city.Trim() + "/" + street.Trim();
                string req = "http://dev.virtualearth.net/REST/v1/Locations/US/" + address + "?output=json&key=" +
                             API_KEY;
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(req);
                WebReq.Method = "GET";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                Stream WebRespStream = WebResp.GetResponseStream();
                StreamReader WebStreamReader = new StreamReader(WebRespStream);

                string respJson = WebStreamReader.ReadToEnd().ToString();
                JObject JsonObject = JObject.Parse(respJson);
                JArray RespArray = (JArray)JsonObject["resourceSets"];
                string respStatus = JsonObject["statusCode"].ToString().Trim();

                JArray resultArray = (JArray)RespArray[0]["resources"];
                if (respStatus == "200" && resultArray.Count >= 1)
                {
                    JArray geoArray = (JArray)resultArray[0]["point"]["coordinates"];
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
