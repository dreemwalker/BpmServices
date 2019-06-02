using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace BpmContactsApp.Models.HttpServices
{
    public class HttpAuthorization
    {

        //перенести в конфиг
        // Строка адреса BPMonline сервиса OData.
        private const string serverUri = "http://shedko.beesender.com//0/ServiceModel/EntityDataService.svc/";
        private const string authServiceUtri = "http://shedko.beesender.com//ServiceModel/AuthService.svc/Login";

        // Ссылки на пространства имен XML.
        private static readonly XNamespace ds = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        private static readonly XNamespace dsmd = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        private static readonly XNamespace atom = "http://www.w3.org/2005/Atom";


       
        public static CookieContainer LogIn(string userName, string userPassword)
        {
            // Создание запроса на аутентификацию.
            var authRequest = HttpWebRequest.Create(authServiceUtri) as HttpWebRequest;
            authRequest.Method = "POST";
            authRequest.ContentType = "application/json";
            var bpmCookieContainer = new CookieContainer();
            // Включение использования cookie в запросе.
            authRequest.CookieContainer = bpmCookieContainer;
            using (var requestStream = authRequest.GetRequestStream())
            {
                // Запись в поток запроса учетных данных пользователя BPMonline и дополнительных параметров запроса.
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
            // Получение ответа от сервера. Если аутентификация проходит успешно, в объекте bpmCookieContainer будут 
            // помещены cookie, которые могут быть использованы для последующих запросов.
            //var response = (HttpWebResponse)authRequest.GetResponse();
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
                 //   CookieManager.bpmCookieContainer = bpmCookieContainer;
                    return bpmCookieContainer;
                }

              //  return status.Message
            }
            return null;//нет ответа от сервера


        }
       
    }
}
