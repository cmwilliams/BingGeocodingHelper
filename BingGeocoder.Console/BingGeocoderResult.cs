using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BingGeocoder.Console
{
    public class BingGeocoderResult
    {
        public string Confidence { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ErrorMessage { get; set; }
    }
}
