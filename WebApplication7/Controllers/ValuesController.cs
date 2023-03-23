using System;
using System.Collections.Generic;
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
                ServiceAccountCredential saCredentials = credentials.UnderlyingCredential as ServiceAccountCredential;
                if (saCredentials == null)
                {
                    return "Error: credentials are not null, they are: " + credentials.UnderlyingCredential.GetType().FullName;
                }
                return "Wrote a log record using SA " + saCredentials.Id;
                //LogManager.Flush(5000);
            }
            catch (Exception ex)
            {
                return "Unknown error: " + ex.ToString();
            }    
        }
    }
}
