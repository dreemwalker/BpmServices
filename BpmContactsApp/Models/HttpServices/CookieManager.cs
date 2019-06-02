using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
namespace BpmContactsApp.Models.HttpServices
{
    public class CookieManager
    {
      
        public static CookieContainer bpmCookieContainer;
        public static bool CheckAuthCookie()
        {
            if (bpmCookieContainer != null)
            {
                return true;
            }
            return false;
        }
    }
}
