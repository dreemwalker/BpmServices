using Newtonsoft.Json;
using System.IO;
using System.Net;
namespace BpmContactsApp.Models.HttpServices
{
    public class HttpAuthorization
    {

      
        private static string _serverUri;
        private static string _authServiceUtri;

     

       public HttpAuthorization(ServicesOptions options)
        {
            _serverUri = options.serverUri;
            _authServiceUtri = options.authServiceUtri;
        }
        public static CookieContainer LogIn(string userName, string userPassword)
        {
           
            var authRequest = HttpWebRequest.Create(_authServiceUtri) as HttpWebRequest;
            authRequest.Method = "POST";
            authRequest.ContentType = "application/json";
            var bpmCookieContainer = new CookieContainer();
         
            authRequest.CookieContainer = bpmCookieContainer;
            using (var requestStream = authRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(@"{
                                ""UserName"":""" + userName + @""",
                                ""UserPassword"":""" + userPassword + @""",
                                ""SolutionName"":""TSBpm"",
                                ""TimeZoneOffset"":-120,
                                ""Language"":""Ru-ru""
                                }");
                }
            }
          
           
            ResponseStatus status = null;
            using (var response = (HttpWebResponse)authRequest.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    status =JsonConvert.DeserializeObject<ResponseStatus>(responseText);
                }

            }

           
            if (status != null)
            {
                if (status.Code == 0)
                {
                    return bpmCookieContainer;
                }

              //  return status.Message
            }
            return null;//нет ответа от сервера


        }
       
    }
}
