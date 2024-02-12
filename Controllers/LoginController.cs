using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TilausDB.Models;
using System.Web.UI.WebControls;

namespace TilausDB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilausDBEntities db = new TilausDBEntities();
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);

            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); 

            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";

                return View("Index", LoginModel);
            }


        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("LoggedOut", "login");
        }

        public ActionResult LoggedOut()
        {
            ViewBag.LoggedOut = "Olet kirjautunut ulos järjestelmästä.";
            return View(); 
        }
    }
}