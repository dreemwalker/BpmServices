using BpmContactsApp.Models;
using BpmContactsApp.Models.HttpServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace BpmContactsApp.Controllers
{
    public class HomeController : Controller
    {
      
        private CookieManager _cookieManager;
        private IRepository<Contact> _repository;
        public HomeController (IDataService dataService, CookieManager cookieManager)
        {

            _cookieManager = cookieManager;
            _repository = new ContactsRepository(dataService);
           
        }

        [HttpGet]
        public IActionResult DeleteContact(string id)
        {

            _repository.Delete(id);
            return Redirect("~/Home");
        }
        public IActionResult Index()
        {

          

            if(!_cookieManager.CheckAuthCookies())
            {
                return Redirect("~/Home/Login");
            }
           // ViewBag.ContactList = repositoryHttp.GetItems();
            return View(_repository.GetItems());
        }
        
        public IActionResult Login()
        {
            ViewBag.AuthLabel = true;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, string UserPassword)
        {
            CookieContainer cookies = HttpAuthorization.LogIn(UserName, UserPassword);
            if (cookies != null)
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
            Contact contactForEdit = _repository.GetItem(contactId);
            return View(contactForEdit);
        }
        [HttpPost]
        public IActionResult EditContact(Contact contact)
        { 

            _repository.Update(contact);
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
        

            if (_repository.Create(contact))
                return Redirect("~/Home");
            ViewBag.Error = true;
            return View();
        }




    }
}
