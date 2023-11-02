using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bai6_63135741.Models;

namespace Bai6_63135741.Controllers
{
    public class NhanVien_63135741Controller : Controller
    {
        private B6QLNV_63135741Entities1 db = new B6QLNV_63135741Entities1();

        // GET: NhanVien_63135741
        string LayMaNV()
        {
            var maMax = db.NhanViens.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }

        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.PhongBan);
            return View(nhanViens.ToList());
        }

        // GET: NhanVien_63135741/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanVien_63135741/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = LayMaNV();
            ViewBag.MaPB = new SelectList(db.PhongBans, "MaPB", "TenPB");
            return View();
        }

        // POST: NhanVien_63135741/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV, HoNV, TenNV, GioiTinh, NgaySinh, Luong, AnhNV, DiaChi, MaPB")] NhanVien nhanVien)
        {
            var imgNV = Request.Files["Avatar"];

            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);

            var path = Server.MapPath("/Images/" + postedFileName);
            imgNV.SaveAs(path);

            if (ModelState.IsValid)
            {
                nhanVien.MaNV = LayMaNV();
                nhanVien.AnhNV = postedFileName;

                try
                {
                    db.NhanViens.Add(nhanVien);
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

            ViewBag.MaPB = new SelectList(db.PhongBans, "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
        }

        // GET: NhanVien_63135741/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPB = new SelectList(db.PhongBans, "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
        }

        // POST: NhanVien_63135741/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,DiaChi,MaPB")] NhanVien nhanVien)
        {
            var imgNV = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgNV.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPB = new SelectList(db.PhongBans, "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
        }

        // GET: NhanVien_63135741/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien_63135741/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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

        public ActionResult PrintList()
        {
            var nhanViens = db.NhanViens.Include(n => n.PhongBan).OrderBy(n => n.TenNV);
            return PartialView(nhanViens.ToList());
        }

        // TIM KIEM NHAN VIEN
        public ActionResult TimKiem()
        {
            var nhanViens = db.NhanViens.Include(n => n.PhongBan);
            return View(nhanViens.ToList());
        }
        [HttpPost]
        public ActionResult TimKiem(string maNV)
        {

            //var nhanViens = db.NhanViens.SqlQuery("exec NhanVien_DS '"+maNV+"' ");
            /// var nhanViens = db.NhanViens.SqlQuery("SELECT * FROM NhanVien WHERE MaNV='" + maNV + "'");
            var nhanViens = db.NhanViens.Where(abc => abc.MaNV == maNV);
            return View(nhanViens.ToList());
        }
        [HttpGet]

        public ActionResult TimKiem_63135741(string maNV = "", string hoTen = "", string gioiTinh = "", string luongMin = "", string luongMax = "", string diaChi = "", string maPB = "")
        {
            string min = luongMin, max = luongMax;
            if (gioiTinh != "1" && gioiTinh != "0")
                gioiTinh = null;
            ViewBag.maNV = maNV;
            ViewBag.hoTen = hoTen;
            ViewBag.gioiTinh = gioiTinh;
            if (luongMin == "")
            {
                ViewBag.luongMin = "";
                min = "0";
            }
            else
            {
                ViewBag.luongMin = luongMin;
                min = luongMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.luongMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.luongMax = luongMax;
                max = luongMax;
            }
            ViewBag.diaChi = diaChi;
            ViewBag.MaPB = new SelectList(db.PhongBans, "MaPB", "TenPB");
            var nhanViens = db.NhanViens.SqlQuery("NhanVien_TimKiem'" + maNV + "',N'" + hoTen + "','" + gioiTinh + "','" + min + "','" + max + "',N'" + diaChi + "','" + maPB + "'");
            if (nhanViens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(nhanViens.ToList());
        }
    }
}
