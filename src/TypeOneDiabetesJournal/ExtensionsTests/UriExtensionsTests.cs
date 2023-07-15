using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Tests
{
    [TestClass()]
    public class UriExtensionsTests
    {
        private const string BaseUrl = "https://example.com";
        private const string Endpoint = "api/data";
        private const string QueryParam1 = "param1";
        private const string QueryValue1 = "value1";
        private const string QueryParam2 = "param2";
        private const string QueryValue2 = "value2";
        private const string AdditionalPath1 = "path1";
        private const string AdditionalPath2 = "path2";

        [TestMethod]
        public void BuildUrl_WithEndpointOnly_ReturnsCorrectUrl()
        {
            // Arrange
            var expectedUrl = $"{BaseUrl}/{Endpoint}";

            // Act
            var actualUrl = BaseUrl.BuildUrl(Endpoint);

            // Assert
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [TestMethod]
        public void BuildUrl_WithQueryParams_ReturnsCorrectUrl()
        {
            // Arrange
            var queryParams = new Dictionary<string, string>
        {
            { QueryParam1, QueryValue1 },
            { QueryParam2, QueryValue2 },
        };
            var expectedUrl = $"{BaseUrl}/{Endpoint}?{QueryParam1}={QueryValue1}&{QueryParam2}={QueryValue2}";

            // Act
            var actualUrl = BaseUrl.BuildUrl(Endpoint, queryParams);

            // Assert
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [TestMethod]
        public void BuildUrl_WithAdditionalPath_ReturnsCorrectUrl()
        {
            // Arrange
            var expectedUrl = $"{BaseUrl}/{Endpoint}/{AdditionalPath1}";

            // Act
            var actualUrl = BaseUrl.BuildUrl(Endpoint, additionalPath: AdditionalPath1);

            // Assert
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [TestMethod]
        public void BuildUrl_WithMultipleAdditionalPaths_ReturnsCorrectUrl()
        {
            // Arrange
            var expectedUrl = $"{BaseUrl}/{Endpoint}/{AdditionalPath1}/{AdditionalPath2}";

            // Act
            var actualUrl = BaseUrl.BuildUrl(Endpoint, null, AdditionalPath1, AdditionalPath2);

            // Assert
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [TestMethod]
        public void BuildUrl_WithQueryParamsAndAdditionalPath_ReturnsCorrectUrl()
        {
            // Arrange
            var queryParams = new Dictionary<string, string>
            {
                { QueryParam1, QueryValue1 },
                { QueryParam2, QueryValue2 },
            };
            var expectedUrl = $"{BaseUrl}/{Endpoint}/{AdditionalPath1}?{QueryParam1}={QueryValue1}&{QueryParam2}={QueryValue2}";

            // Act
            var actualUrl = BaseUrl.BuildUrl(Endpoint, queryParams, AdditionalPath1);

            // Assert
            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}