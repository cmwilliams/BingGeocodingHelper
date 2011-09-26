using BingGeocoder;
using NUnit.Framework;

namespace Specs
{
     [TestFixture]
    class BingGeocoderTest
     {
         private string Key = "AnOcTOfyJGgVzJL7eL9CEvbIUIoC7QgUQatnn7kGc_jXVX3ZgmC6NLsgZqifCt67";
         [Test]
         public void ShouldReturnEmptyResultWithFakeKeyProvided()
         {
             var geocoder = new BingGeocoderClient("blah");
             var result = new BingGeocoderResult();
             result = geocoder.Geocode("blah");
             Assert.AreEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.ErrorMessage);
         }

         [Test]
         public void ShouldReturnEmptyResultWithRealKeyProvidedAndFakeAddress()
         {
             var geocoder = new BingGeocoderClient(Key);
             var result = new BingGeocoderResult();
             result = geocoder.Geocode("123 Test", "Test", "Test", "Test");
             Assert.AreEqual(result.Confidence, "No results");
             Assert.IsNull(result.Latitude);
             Assert.IsNull(result.Longitude);
             Assert.IsNull(result.ErrorMessage);
         }

         [Test]
         public void ShouldReturnValidResultWithRealKeyProvidedAndRealAddress()
         {
             var geocoder = new BingGeocoderClient(Key);
             var result = new BingGeocoderResult();
             result = geocoder.Geocode("1230 Peachtree St", "Atlanta", "GA", "30309");
             Assert.AreNotEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.Latitude);
             Assert.IsNotNull(result.Longitude);
             Assert.IsNull(result.ErrorMessage);
         }

         [Test]
         public void ShouldReturnValidResultWithRealKeyProvidedAndRealQuery()
         {
             var geocoder = new BingGeocoderClient(Key);
             var result = new BingGeocoderResult();
             result = geocoder.Geocode("ATL");
             Assert.AreNotEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.Latitude);
             Assert.IsNotNull(result.Longitude);
             Assert.IsNull(result.ErrorMessage);
         }
    }
}
