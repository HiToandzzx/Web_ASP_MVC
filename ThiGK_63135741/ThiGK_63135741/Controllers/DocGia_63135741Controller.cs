using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThiGK_63135741.Models;

namespace ThiGK_63135741.Controllers
{
    public class DocGia_63135741Controller : Controller
    {
        private ThiGK_63135741Entities1 db = new ThiGK_63135741Entities1();

        public ActionResult GioiThieu_63135741()
        {
            return View();
        }

        // GET: DocGia_63135741
        string LayMaDG()
        {
            var maMax = db.DocGias.ToList().Select(n => n.MaDG).Max();
            int maDG = int.Parse(maMax.Substring(5)) + 1;
            string DG = String.Concat("00", maDG.ToString());
            return "16TH" + DG.Substring(maDG.ToString().Length - 1);
        }

        public ActionResult Index()
        {
            var docGias = db.DocGias.Include(d => d.LoaiDocGia);
            return View(docGias.ToList());
        }

        // GET: DocGia_63135741/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = db.DocGias.Find(id);
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // GET: DocGia_63135741/Create
        public ActionResult Create()
        {
            ViewBag.MaDG = LayMaDG();
            ViewBag.MaLoaiDG = new SelectList(db.LoaiDocGias, "MaLoaiDG", "TenLoaiDG");
            return View();
        }

        // POST: DocGia_63135741/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDG,HoDG,TenDG,NgaySinh,GioiTinh,AnhDG,Email,MaLoaiDG")] DocGia docGia)
        {
            var imgDG = Request.Files["Avatar"];

            string postedFileName = System.IO.Path.GetFileName(imgDG.FileName);

            var path = Server.MapPath("/Images/" + postedFileName);
            imgDG.SaveAs(path);

            if (ModelState.IsValid)
            {
                docGia.MaDG = LayMaDG();
                docGia.AnhDG = postedFileName;

                try
                {
                    db.DocGias.Add(docGia);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError("", $"Lỗi: {validationError.ErrorMessage}");
                        }
                    }
                }
            }

            ViewBag.MaLoaiDG = new SelectList(db.LoaiDocGias, "MaLoaiDG", "TenLoaiDG", docGia.MaLoaiDG);
            return View(docGia);
        }

        // GET: DocGia_63135741/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = db.DocGias.Find(id);
            if (docGia == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiDG = new SelectList(db.LoaiDocGias, "MaLoaiDG", "TenLoaiDG", docGia.MaLoaiDG);
            return View(docGia);
        }

        // POST: DocGia_63135741/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDG,HoDG,TenDG,NgaySinh,GioiTinh,AnhDG,Email,MaLoaiDG")] DocGia docGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiDG = new SelectList(db.LoaiDocGias, "MaLoaiDG", "TenLoaiDG", docGia.MaLoaiDG);
            return View(docGia);
        }

        // GET: DocGia_63135741/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = db.DocGias.Find(id);
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // POST: DocGia_63135741/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DocGia docGia = db.DocGias.Find(id);
            db.DocGias.Remove(docGia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // TIM KIEM NHAN VIEN
        public ActionResult TimKiem_63135741(string maDG = "", string hoTen = "", string maLoaiDG = "")
        {
            ViewBag.maDG = maDG;
            ViewBag.hoTen = hoTen;
            ViewBag.MaLoaiDG = new SelectList(db.LoaiDocGias, "MaLoaiDG", "TenLoaiDG");
            var docGias = db.DocGias.SqlQuery("TimKiem'" + maDG + "',N'" + hoTen + "','" + maLoaiDG + "'");
            if (docGias.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(docGias.ToList());
        }
    }
}
