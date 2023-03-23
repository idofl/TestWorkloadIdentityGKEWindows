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
            var credentials = GoogleCredential.GetApplicationDefault();

            ILog log = LogManager.GetLogger(typeof(LoggerController));
            log.Info("An exciting log entry!");
            //LogManager.Flush(5000);
            return "Wrote a log record using SA " + (credentials.UnderlyingCredential as ServiceAccountCredential).Id;
        }
    }
}
