using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Google.Apis.Auth.OAuth2;
using log4net;

namespace WebApplication7.Controllers
{
    public class LoggerController : ApiController
    {
        // GET api/logger
        public string Get()
        {
            try
            {
                
                var credentials = GoogleCredential.GetApplicationDefault();

                if (credentials == null)
                {
                    return "error, credentials are null";
                }

                ILog log = LogManager.GetLogger(typeof(LoggerController));
                log.Info("An exciting log entry! 1.0");
                log.Info(credentials.GetType().FullName);
                ServiceCredential serviceCredentials = credentials.UnderlyingCredential as ServiceCredential;
                string serviceId = "";
                if (serviceCredentials is ServiceAccountCredential)
                    serviceId = ((ServiceAccountCredential)serviceCredentials).Id;
                else if (serviceCredentials is ComputeCredential)
                {
                    var token = ((ComputeCredential)serviceCredentials).GetOidcTokenAsync(OidcTokenOptions.FromTargetAudience("https://test.com")).Result.GetAccessTokenAsync().Result;
                    serviceId = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.First((claim) => claim.Type == "email").Value;
                }
                
                if (serviceId == "")
                {
                    return "Error: credentials are not null, they are: " + credentials.UnderlyingCredential.GetType().FullName;
                }
                return "Wrote a log record using SA " + serviceId;
                //LogManager.Flush(5000);
            }
            catch (Exception ex)
            {
                return "Unknown error: " + ex.ToString();
            }    
        }
    }
}
