using Bai2_63135741.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bai2_63135741.Controllers
{
    public class SinhVien_63135741Controller : Controller
    {
        // GET: SinhVien_63135741
        public ActionResult Index()
        {
            return View();
        }

        //SU DUNG REQUEST
        public ActionResult Register1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register1(FormCollection form)
        {
            ViewBag.Id = Request["Id"];
            ViewBag.Name = Request["Name"];
            ViewBag.Marks = Request["Marks"];
            return View("Registered");
        }

        // SU DUNG DOI SO ACTION
        public ActionResult Register2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register2(string Id, string Name, string Marks)
        {
            ViewBag.Id = Id;
            ViewBag.Name = Name;
            ViewBag.Marks = Marks;
            return View("Registered");
        }

        // SU DUNG FORM COLLECTION
        public ActionResult Register3()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register3(FormCollection form)
        {
            ViewBag.Id = form["Id"];
            ViewBag.Name = form["Name"];
            ViewBag.Marks = form["Marks"];
            return View("Registered");
        }

        public ActionResult Registered()
        {
            return View();
        }

    }
}