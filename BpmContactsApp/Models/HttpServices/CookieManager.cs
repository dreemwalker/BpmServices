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
        public  bool CheckAuthCookies()
        {
            if (bpmCookieContainer != null)
            {
                return true;
            }
            return false;
        }
        public  void DeleteAuthCookies()
        {
            bpmCookieContainer = null;
        }
        public CookieContainer GetAuthCookies()
        {
            return bpmCookieContainer;
        }
    }
}
