using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.Entity;

namespace Kutuphane.Controllers
{
    public class KategoriController : Controller
    {
        //dbLibraryEntities db = new dbLibraryEntities();
        dbLibrarySomeeEntities db = new dbLibrarySomeeEntities();
        public static bool isInsert = false;

        // GET: Kategori
        [HttpGet]
        public ActionResult Index()
        {
            var dataList = db.Kategori.ToList();

            return View(dataList);
        }


        [HttpGet]
        public ActionResult Insert()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Kategori kategori)
        {
            if (isInsert)
            {
                return Json(data: new { success = 2, message = "İŞLEM DEVAM EDİYOR!" }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(kategori.DeweyId))
            {
                ModelState.AddModelError("DeweyId", "");
            }

            if (string.IsNullOrEmpty(kategori.Isim))
            {
                ModelState.AddModelError("Isim", "");
            }

            if (kategori.DeweyId != null && kategori.Isim != null)
            {
                if (ModelState.IsValid)
                {
                    isInsert = true;

                    try
                    {
                        db.Kategori.Add(kategori);
                        db.SaveChanges();

                        isInsert = false;
                        return Json(data: new { success = 0, message = "KAYIT BAŞARILI!" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e) { }
                } else
                {
                    return View();
                }
            }
            else
            {
                return Json(data: new { success = 1, message = "BOŞ YERLERİ DOLDURUNUZ!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(data: new { success = 1, message = "BİR HATA OLDU!" }, JsonRequestBehavior.AllowGet);
        }
    }
}