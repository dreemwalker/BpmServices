using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BpmContactsApp.Models.HttpServices;
using BpmContactsApp.Models;

using System.Net;
namespace BpmContactsApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult DeleteContact(string id)
        {
            ServicesOptions servicesOptions = new ServicesOptions();
            CookieManager cookieManager = new CookieManager();
            IDataService dataService = new HttpDataService(servicesOptions, cookieManager.GetAuthCookies());
            IRepository<Contact> repositoryHttp = new ContactsRepository(dataService);
            repositoryHttp.Delete(id);
            return Redirect("~/Home");
        }
        public IActionResult Index()
        {

            ServicesOptions servicesOptions = new ServicesOptions();
            CookieManager cookieManager = new CookieManager();
            IDataService dataService = new HttpDataService(servicesOptions, cookieManager.GetAuthCookies());
            IRepository<Contact> repositoryHttp = new ContactsRepository(dataService);

            if(!cookieManager.CheckAuthCookies())
            {
                return Redirect("~/Home/Login");
            }
           // ViewBag.ContactList = repositoryHttp.GetItems();
            return View(repositoryHttp.GetItems());
        }
        
        public IActionResult Login()
        {
            ViewBag.AuthLabel = true;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, string UserPassword)
        {
            CookieContainer cookies= HttpAuthorization.LogIn(UserName, UserPassword);
            if(cookies!=null)
            {
                CookieManager.bpmCookieContainer = cookies;
                return Redirect("~/Home");
            }
            ViewBag.AuthLabel = false;
             return View();

        }
        [HttpGet]
        public IActionResult EditContact(string id)
        {
            string contactId = id;
            ServicesOptions servicesOptions = new ServicesOptions();
            CookieManager cookieManager = new CookieManager();
            IDataService dataService = new HttpDataService(servicesOptions, cookieManager.GetAuthCookies());
            IRepository<Contact> repositoryHttp = new ContactsRepository(dataService);

            Contact contactForEdit= repositoryHttp.GetItem(contactId);
            return View(contactForEdit);
        }
        [HttpPost]
        public IActionResult EditContact(Contact contact)
        {
            
            ServicesOptions servicesOptions = new ServicesOptions();
            CookieManager cookieManager = new CookieManager();
            IDataService dataService = new HttpDataService(servicesOptions, cookieManager.GetAuthCookies());
            IRepository<Contact> repositoryHttp = new ContactsRepository(dataService);

            repositoryHttp.Update(contact);
            return Redirect("~/Home");
            // return View();
        }
        public IActionResult AddContact()
        {
            ViewBag.Error = false;
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            ServicesOptions servicesOptions = new ServicesOptions();
            CookieManager cookieManager = new CookieManager();
            IDataService dataService = new HttpDataService(servicesOptions, cookieManager.GetAuthCookies());
            IRepository<Contact> repositoryHttp = new ContactsRepository(dataService);

            if(repositoryHttp.Create(contact))
                return Redirect("~/Home");
            ViewBag.Error = true;
            return View();
        }




    }
}
