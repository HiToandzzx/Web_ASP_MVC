using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KT0720_NguyenHuyToan_63135741.Models;

namespace KT0720_NguyenHuyToan_63135741.Controllers
{
    public class SinhVien_63135741Controller : Controller
    {
        private KT0720_63135741Entities db = new KT0720_63135741Entities();

        public ActionResult GioiThieu_63135741()
        {
            return View();
        }

        // GET: SinhVien_63135741
        string LayMaSV()
        {
            var maMax = db.SinhViens.ToList().Select(n => n.MaSV).Max();
            int maSV = int.Parse(maMax.Substring(5)) + 1;
            string SV = String.Concat("010", maSV.ToString());
            return "17TH" + SV.Substring(maSV.ToString().Length - 1);
        }

        public ActionResult Index()
        {
            var sinhViens = db.SinhViens.Include(s => s.Lop);
            return View(sinhViens.ToList());
        }

        // GET: SinhVien_63135741/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: SinhVien_63135741/Create
        public ActionResult Create()
        {
            ViewBag.MaSV = LayMaSV();
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            return View();
        }

        // POST: SinhVien_63135741/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SinhVien sinhVien)
        {
            var imgSV = Request.Files["Avatar"];

            string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);

            var path = Server.MapPath("/Images/" + postedFileName);
           imgSV.SaveAs(path);

            if (ModelState.IsValid)
            {
                sinhVien.MaSV = LayMaSV();
                sinhVien.AnhSV = postedFileName;

                try
                {
                    db.SinhViens.Add(sinhVien);
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

            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: SinhVien_63135741/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // POST: SinhVien_63135741/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: SinhVien_63135741/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhVien_63135741/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SinhVien sinhVien = db.SinhViens.Find(id);
            db.SinhViens.Remove(sinhVien);
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
        public ActionResult TimKiem_63135741(string maSV = "", string hoTen = "", string maLop = "")
        {  
            ViewBag.maSV = maSV;
            ViewBag.hoTen = hoTen;  
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            var sinhViens = db.SinhViens.SqlQuery("TimKiem'" + maSV + "',N'" + hoTen + "','" + maLop + "'");
            if (sinhViens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(sinhViens.ToList());
        }
    }
}
