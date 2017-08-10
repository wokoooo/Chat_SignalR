using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Web.Models;
using Web.hub;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// 发送广播
        /// </summary>
        /// <returns></returns>
        public ActionResult SendNotification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendNotification(MessageViewModel msg)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            
            context.Clients.All.addNewMsg("administrator", msg.Message);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}