using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postcodes.API.Testing.Models;
using Postcodes.API.Testing.Helpers;

namespace Postcodes.API.Test
{

    [TestClass]
    public class PostCodesAPITest
    {

        private Executors _executors;

        [TestInitialize]
        public void PostCodesAPITestSetup()
        {
            _executors = new Executors();
        }


        [TestMethod]
        [TestCategory("PostCodes API Test")]
        public void PostCodeAPI_PostcodeValidation_WithValidPostcode()
        {
            //Arrange
            string queryString = "postcodes/M45 6GN/Validate";
            string response = string.Empty;

            //Act
            Task.Run(() =>
            {
                response = _executors.GETPostcodeAsync(queryString).Result;

            }).Wait();

            dynamic jsonValidatePostCodeData = JObject.Parse(response);

            //Assert
            Assert.AreEqual(jsonValidatePostCodeData.status.ToString(), "200");
            Assert.AreEqual(jsonValidatePostCodeData.result.ToString(), "True");

        }


        [TestMethod]
        [TestCategory("PostCodes API Test")]
        public void PostCodeAPI_PostcodeValidation_WithInValidPostcode()
        {
            //Arrange
            string queryString = "postcodes/M45 XXX/Validate";
            string response = string.Empty;

            //Act
            Task.Run(() =>
            {
                response = _executors.GETPostcodeAsync(queryString).Result;

            }).Wait();

            dynamic jsonValidatePostCodeData = JObject.Parse(response);

            //Assert
            Assert.AreEqual(jsonValidatePostCodeData.status.ToString(), "200");
            Assert.AreEqual(jsonValidatePostCodeData.result.ToString(), "False");

        }

        [TestMethod]
        [TestCategory("PostCodes API Test")]
        public void PostCodeAPI_BulkPostcodeLookup()
        {
            //Arrange
            PostCodes mypcodes = new PostCodes();
            mypcodes.postcodes = new List<string>(new string[] { "PR3 0SG", "M45 6GN", "EX16 5BL" });
            var jsonBulkPostcodeData = JsonConvert.SerializeObject(mypcodes);
            string response = string.Empty;

            //Act
            Task.Run(() =>
            {
                response = _executors.POSTPostcodeAsync("postcodes/", jsonBulkPostcodeData).Result;

            }).Wait();

            dynamic jsonBulkPcodeData = JObject.Parse(response);
            var resultArray = (JArray)jsonBulkPcodeData["result"];


            //Assert
            Assert.AreEqual(jsonBulkPcodeData.status.ToString(), "200");
            Assert.AreEqual(resultArray.Count(), 3);
            Assert.AreEqual(jsonBulkPcodeData["result"][0]["result"]["postcode"].ToString(), "PR3 0SG");
            Assert.AreEqual(jsonBulkPcodeData["result"][1]["result"]["postcode"].ToString(), "M45 6GN");
            Assert.AreEqual(jsonBulkPcodeData["result"][2]["result"]["postcode"].ToString(), "EX16 5BL");
        }

        [TestMethod]
        [TestCategory("PostCodes API Test")]
        public void PostCodeAPI_BulkReverseGeocoding_PostcodeLookUp()
        {
            //Arrange
            Geolocation geo1 = new Geolocation();
                geo1.longitude = -3.15807731271522;
                geo1.latitude = 51.4799900627036;
                geo1.limit = 20;
                geo1.radius = 500;

            Geolocation geo2 = new Geolocation();
                geo2.longitude = -3.15807731271522;
                geo2.latitude = 51.4799900627036;
                geo2.limit = 20;
                geo2.radius = 500;

            List<Geolocation> geoTestList = new List<Geolocation>();
            geoTestList.Add(geo1);
            geoTestList.Add(geo2);

            Geolocations geos = new Geolocations();
            geos.geolocations = geoTestList;

            var jsonGeolocationData = JsonConvert.SerializeObject(geos);
            string response = string.Empty;

            //Act
            Task.Run(() =>
            {
                response = _executors.POSTPostcodeAsync("postcodes/", jsonGeolocationData).Result;

            }).Wait();

            dynamic jsonGeoPostCodeData = JObject.Parse(response);
            var resultArray = (JArray)jsonGeoPostCodeData["result"];

            //Assert
            Assert.AreEqual(jsonGeoPostCodeData.status.ToString(),"200");
            Assert.AreEqual(resultArray.Count(), 2);
            Assert.AreEqual(jsonGeoPostCodeData["result"][1]["result"][0]["postcode"].ToString(), "CF24 2BT");
        }
    }
}
