using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace BpmContactsApp.Models.HttpServices
{
    public class ServicesOptions
    {
        public  string serverUri = "http://shedko.beesender.com//0/ServiceModel/EntityDataService.svc/";
        public  string authServiceUtri = "http://shedko.beesender.com//ServiceModel/AuthService.svc/Login";
    }
}
