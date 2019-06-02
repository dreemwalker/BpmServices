using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace BpmContactsApp.Models.HttpServices
{
   
    public class HttpDataService : IDataService
    {
        private CookieContainer _bpmCookieContainer;
        private string _serverUri;
        private string _authUri;
        // Ссылки на пространства имен XML.
        private static readonly XNamespace ds = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        private static readonly XNamespace dsmd = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        private static readonly XNamespace atom = "http://www.w3.org/2005/Atom";

        public HttpDataService (ServicesOptions options, CookieContainer AuthCookies)
        {
            _serverUri = options.serverUri;
            _authUri = options.authServiceUtri;
            _bpmCookieContainer = AuthCookies;
        }
        public void DeleteContact(string id)
        {
            // Id записи объекта, который необходимо удалить.
            string contactId =id;
            // Создание запроса к сервису, который будет удалять данные.
            var request = (HttpWebRequest)HttpWebRequest.Create(_serverUri
                    + "ContactCollection(guid'" + contactId + "')");
            //request.Credentials = new NetworkCredential("BPMUserName", "BPMUserPassword");
            request.CookieContainer = _bpmCookieContainer;
            request.Method = "DELETE";
            // Получение ответа от сервися о результате выполненя операции.
            using (WebResponse response = request.GetResponse())
            {
                // Обработка результата выполнения операции.
            }
        }

        public Contact GetContactByID(string id)
        {
            List<Contact> ContactList = new List<Contact>();

            string requestUri = _serverUri + "ContactCollection(guid'" + id + "')";

            var request = HttpWebRequest.Create(requestUri) as HttpWebRequest;
            request.Method = "GET";
            request.CookieContainer = _bpmCookieContainer;

            using (var response = request.GetResponse())
            {
              
                XDocument xmlDoc = XDocument.Load(response.GetResponseStream());

                ContactList = ConvertXmlToContact(xmlDoc);
            }
            if (ContactList.Count > 0)
                return ContactList[0];
            else return null;

        }
        

        private List<Contact>ConvertXmlToContact(XDocument xmlContacts)
        {
            List<Contact> contactList = new List<Contact>();
            XDocument xmlDoc = xmlContacts;
            var contacts = from entry in xmlDoc.Descendants(atom + "entry")
                           select new
                           {
                               Id = new Guid(entry.Element(atom + "content")
                                                      .Element(dsmd + "properties")
                                                      .Element(ds + "Id").Value),
                               Name = entry.Element(atom + "content")
                                               .Element(dsmd + "properties")
                                               .Element(ds + "Name").Value,
                               MobilePhone = entry.Element(atom + "content")
                                               .Element(dsmd + "properties")
                                               .Element(ds + "MobilePhone").Value,
                               Dear = entry.Element(atom + "content")
                                               .Element(dsmd + "properties")
                                               .Element(ds + "Dear").Value,
                               JobTitle = entry.Element(atom + "content")
                                               .Element(dsmd + "properties")
                                               .Element(ds + "JobTitle").Value,
                               BirthDate = entry.Element(atom + "content")
                                               .Element(dsmd + "properties")
                                               .Element(ds + "BirthDate").Value
                           };

            foreach (var contact in contacts)
            {
                Contact tmp = new Contact();
                tmp.Id = contact.Id;
                tmp.Name = contact.Name;
                tmp.MobilePhone = contact.MobilePhone;
                tmp.Dear = contact.Dear;
                tmp.JobTitle = contact.JobTitle;
                tmp.BirthDate = Convert.ToDateTime(contact.BirthDate);
                contactList.Add(tmp);

            }
            return contactList;
        }
        private XElement ConvertContactToXml(Contact contact)
        {
            // Создание сообщения xml, содержащего данные о создаваемом объекте.
            XElement content = new XElement(dsmd + "properties",
                          new XElement(ds + "Name", contact.Name),
                          new XElement(ds + "MobilePhone", contact.MobilePhone),
                          new XElement(ds + "Dear", contact.Dear),
                          new XElement(ds + "JobTitle", contact.JobTitle),
                          new XElement(ds + "BirthDate", contact.BirthDate.ToShortDateString()));

            XElement entry = new XElement(atom + "entry",
                        new XElement(atom + "content",
                        new XAttribute("type", "application/xml"), content));

            return entry;
        }
        public List<Contact> GetContacts()
        {
                List<Contact> ContactList = new List<Contact>();
               
                var dataRequest = HttpWebRequest.Create(_serverUri + "ContactCollection?select=Id, Name")
                                        as HttpWebRequest;
              
                dataRequest.Method = "GET";
               
                dataRequest.CookieContainer = _bpmCookieContainer;
               
                using (var dataResponse = (HttpWebResponse)dataRequest.GetResponse())
                {
                    // Загрузка ответа сервера в xml-документ для дальнейшей обработки.
                    XDocument xmlDoc = XDocument.Load(dataResponse.GetResponseStream());
                    // Получение коллекции объектов контактов, соответствующих условию запроса.
                    ContactList = ConvertXmlToContact(xmlDoc);
                }

            return ContactList;
            
        }

        public bool InsertContact(Contact contact)
        {

            XElement entry = ConvertContactToXml(contact);
          
            // Создание запроса к сервису, который будет добавлять новый объект в коллекцию контактов.
            var request = (HttpWebRequest)HttpWebRequest.Create(_serverUri + "ContactCollection/");
            //request.Credentials = new NetworkCredential("Supervisor", "Supervisor");

             request.CookieContainer = _bpmCookieContainer;
            CookieCollection cookieCollection = _bpmCookieContainer.GetCookies(new Uri(_authUri));
            string csrfToken = cookieCollection["BPMCSRF"].Value;
            request.Method = "POST";
            request.Accept = "application/atom+xml";
            request.ContentType = "application/atom+xml;type=entry";
            request.Headers.Add("BPMCSRF", csrfToken);
           
            using (var writer = XmlWriter.Create(request.GetRequestStream()))
            {
                entry.WriteTo(writer);
            }
            // Получение ответа от сервиса о результате выполнения операции.
            using (WebResponse response = request.GetResponse())
            {
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
               
            }
            return false;
        }

        public void UpdateContact(Contact contact)
        {
         
            string contactId = contact.Id.ToString();
          
            XElement entry = ConvertContactToXml(contact);
         
            var request = (HttpWebRequest)HttpWebRequest.Create(_serverUri
                    + "ContactCollection(guid'" + contactId + "')");
            // request.Credentials = new NetworkCredential("BPMUserName", "BPMUserPassword");

            // или request.Method = "MERGE";
            request.CookieContainer = _bpmCookieContainer;
            request.Method = "PUT";
            request.Accept = "application/atom+xml";
            request.ContentType = "application/atom+xml;type=entry";
            
            using (var writer = XmlWriter.Create(request.GetRequestStream()))
            {
                entry.WriteTo(writer);
            }
           
            using (WebResponse response = request.GetResponse())
            {
                
                // Обработка результата выполнения операции.
            }
        }
    }
}
