using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DurianExport_63135741.Controllers
{
    public class Login_63135741Controller : Controller
    {
        // GET: Login_63135741
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login loginModel)
        {
            if (ModelState.IsValid)
            {
                // Add your login logic here, e.g., check username and password
                // If login is successful, redirect to the appropriate page
                // If login fails, add an error to ModelState
            }

            return View(loginModel);
        }
    }
}