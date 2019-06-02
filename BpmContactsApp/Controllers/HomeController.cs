using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BpmContactsApp.Models.HttpServices;


using System.Net;
namespace BpmContactsApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {

           // var c = CookieManager.bpmCookieContainer;
            //if (!HttpAuthorization.CheckAuth())
            //{
            //    return Redirect("~/Home/Login");
             
            //}
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }




        }
}
