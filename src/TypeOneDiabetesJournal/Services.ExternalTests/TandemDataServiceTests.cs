using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Services.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services.External.Tests
{
    [TestClass()]
    public class TandemDataServiceTests
    {
        [TestMethod()]
        public void UpdateTokenTest()
        {
            string content = @"
{
""accessToken"":""4335f8908cbb89d933044de77c4a438329234ffd"",
""accessTokenExpiresAt"":""2023-03-19T23:15:57.010Z"",
""refreshToken"":""19f8aca17e140bcc15f0177b2a8bec2bec9c3eb9"",
""refreshTokenExpiresAt"":""2023-04-18T15:15:57.010Z"",
""scope"":""cloud.account cloud.upload cloud.accepttcpp cloud.email cloud.password"",
""client"":
{
""id"":""C2331CD6-D450-495E-9C19-67215230C85C""
},
""user"":
{
""id"":""5EC131FA-625B-40CC-857A-4A676CC44D7B"",
""patientObjectId"":""65FC2DB7-0F62-E511-938E-02BFAC103C28"",
""patientObjectType"":""account""
}
}"
            ;

            TandemTokenResponse tokenResponse = JsonConvert.DeserializeObject<TandemTokenResponse>(content);
            Assert.AreEqual("4335f8908cbb89d933044de77c4a438329234ffd", tokenResponse.AccessToken);
            Assert.AreEqual("19f8aca17e140bcc15f0177b2a8bec2bec9c3eb9", tokenResponse.RefreshToken);
            Assert.AreEqual("cloud.account cloud.upload cloud.accepttcpp cloud.email cloud.password", tokenResponse.Scope);
            Assert.AreEqual("C2331CD6-D450-495E-9C19-67215230C85C", tokenResponse.Client.Id);
            Assert.AreEqual("5EC131FA-625B-40CC-857A-4A676CC44D7B", tokenResponse.User.Id);
            Assert.AreEqual("65FC2DB7-0F62-E511-938E-02BFAC103C28", tokenResponse.User.PatientObjectId);
            Assert.AreEqual("account", tokenResponse.User.PatientObjectType);
        }
    }
}