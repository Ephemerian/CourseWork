using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Hotels()
        {
            ViewBag.Title = "Adding Hotel";

            return View();
        }

        public ActionResult HotelRooms()
        {
            ViewBag.Title = "Adding Hotelroom";

            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Title = "Registering User";

            return View();
        }

        public ActionResult Tours()
        {
            ViewBag.Title = "Creating tour";

            return View();
        }

        public ActionResult RoomReservation()
        {
            ViewBag.Title = "Reserving room";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";

            return View();
        }
    }
}
