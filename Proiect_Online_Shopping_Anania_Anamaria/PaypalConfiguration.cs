using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proiect_Online_Shopping_Anania_Anamaria
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "AYZRG8Z2WvC62KRkIrP2m2PM9bHxh6ev7gbctzXiSSH07Boe0TMr-PCb7hPq8pMWooBBmYZOjr6DDpeT";
            clientSecret = "EMuje4mtHnOCF_boIHf6fIHU_lQ2_XzN7k-pWEdgemAuTHluoDO1DXL4SXl3nLI20eeA8ComiJXsVicK";
        }

        private static Dictionary<string,string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}