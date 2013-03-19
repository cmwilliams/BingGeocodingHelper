using BingGeocoder;
using NUnit.Framework;

namespace Specs
{
     [TestFixture]
    class BingGeocoderTest
     {
         private const string Key = "AnOcTOfyJGgVzJL7eL9CEvbIUIoC7QgUQatnn7kGc_jXVX3ZgmC6NLsgZqifCt67";

         [Test]
         public void ShouldReturnEmptyResultWithFakeKeyProvided()
         {
             var geocoder = new BingGeocoderClient("blah");
             var result = geocoder.Geocode("blah");
             Assert.AreEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.ErrorMessage);
         }

         [Test]
         public void ShouldReturnEmptyResultWithRealKeyProvidedAndFakeAddress()
         {
             var geocoder = new BingGeocoderClient(Key);
             var result = geocoder.Geocode("123 Blah Blah", "Test", "Test", "Test");
             Assert.AreNotEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.Latitude);
             Assert.IsNotNull(result.Longitude);
             Assert.IsNull(result.ErrorMessage);
         }

         [Test]
         public void ShouldReturnValidResultWithRealKeyProvidedAndRealAddress()
         {
             var geocoder = new BingGeocoderClient(Key);
             var result = geocoder.Geocode("1230 Peachtree St", "Atlanta", "GA", "30309");
             Assert.AreNotEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.Latitude);
             Assert.IsNotNull(result.Longitude);
             Assert.IsNull(result.ErrorMessage);
         }

         [Test]
         public void ShouldReturnValidResultWithRealKeyProvidedAndRealQuery()
         {
             var geocoder = new BingGeocoderClient(Key);
             var result = geocoder.Geocode("ATL");
             Assert.AreNotEqual(result.Confidence, "No results");
             Assert.IsNotNull(result.Latitude);
             Assert.IsNotNull(result.Longitude);
             Assert.IsNull(result.ErrorMessage);
         }
    }
}
