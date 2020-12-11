using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Kutuphane.Controllers
{
    public class KategoriController : Controller
    {
        //dbLibraryEntities db = new dbLibraryEntities();
        dbLibrarySomeeEntities db = new dbLibrarySomeeEntities();
        public bool isInsert = false;
        public bool isDelete = false;
        public bool isUpdate = false;

        // GET: Kategori
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var dataList = db.Kategori.ToList().OrderBy(kat => kat.DeweyId).ToPagedList(page ?? 1, 10);

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
            // Aynı kayıt işlemini sürekli tıklayarak yapmasın diye, kayıt işlemi devam ediyormu kontrol et?
            if (isInsert)
            {
                return Json(data: new { success = 2, message = "İŞLEM DEVAM EDİYOR!" }, JsonRequestBehavior.AllowGet);
            }

            // Textbox Dewey Kodu Boşmu Kontrol Et
            if (string.IsNullOrEmpty(kategori.DeweyId))
            {
                ModelState.AddModelError("DeweyId", "");
                return Json(data: new { success = 1, message = "BOŞ YERLERİ DOLDURUNUZ!" }, JsonRequestBehavior.AllowGet);
            }

            // Textbox İsim Boşmu Kontrol Et
            if (string.IsNullOrEmpty(kategori.Isim))
            {
                ModelState.AddModelError("Isim", "");
                return Json(data: new { success = 1, message = "BOŞ YERLERİ DOLDURUNUZ!" }, JsonRequestBehavior.AllowGet);
            }

            // Textboxlarda Validateler (Yazım kuralı) düzgünmü kontrol et.
            if (ModelState.IsValid)
            {
                // Kayıt işleminin başladığını belirten değişkenimizi true yapıyoruz
                isInsert = true;

                try
                {
                    // DeweyId veritabanında kayıtlımı
                    int data = db.Kategori.Where(i => i.DeweyId == kategori.DeweyId).ToList().Count;
                    if (data != 0)
                    {
                        isInsert = false;
                        return Json(data: new { success = 3, message = "DEWEY KODU MEVCUT!" }, JsonRequestBehavior.AllowGet);
                    }

                    // İsim veritabanında kayıtlımı
                    data = db.Kategori.Where(i => i.Isim == kategori.Isim).ToList().Count;
                    if (data != 0)
                    {
                        isInsert = false;
                        return Json(data: new { success = 3, message = "KATREGORI ADI MEVCUT!" }, JsonRequestBehavior.AllowGet);
                    }


                    // Burada Kayıt İşlemi
                    db.Kategori.Add(kategori);
                    db.SaveChanges();

                    isInsert = false;
                    return Json(data: new { success = 0, message = "KAYIT BAŞARILI!" }, JsonRequestBehavior.AllowGet);

                }
                // Herhangi bir hata durumunda kayıt işleminin devam ettiğini belirten değişkenimizi false yapıyoruz
                catch (Exception e) { isInsert = false; }
            }
            else
            {
                // Validate hatası olursa buraya giriyor
                return Json(data: new { success = 3, message = "LÜTFEN HATALARI DÜZELTİNİZ!" }, JsonRequestBehavior.AllowGet);
            }

            // Bilmediğim bir hata olursa bitiriyor
            return Json(data: new { success = 1, message = "BİR HATA OLDU!" }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Delete(int katID) 
        {
            if (isDelete)
            {
                return Json(data: new { success = 2, message = "İŞLEM DEVAM EDİYOR!" }, JsonRequestBehavior.AllowGet);
            }

            isDelete = true;
            var datas = db.Kategori.Where(x => x.ID == katID).SingleOrDefault();
            if (datas != null)
            {

                db.Kategori.Remove(datas);
                db.SaveChanges();

                isDelete = false;
                return Json(data: new { success = 0, message = "SİLME İŞLEMİ BAŞARILI!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(data: new { success = -1, message = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Update(int ID)
        {
            //var data = db.Kategori.Find(ID);
            var data = db.Kategori.Where(item => item.ID == ID).Single();
            if (data != null)
            {
                return View("Update", data);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Update(Kategori kategori)
        {
            // Aynı kayıt işlemini sürekli tıklayarak yapmasın diye, kayıt işlemi devam ediyormu kontrol et?
            if (isUpdate)
            {
                return Json(data: new { success = 2, message = "İŞLEM DEVAM EDİYOR!" }, JsonRequestBehavior.AllowGet);
            }

            // Textbox Dewey Kodu Boşmu Kontrol Et
            if (string.IsNullOrEmpty(kategori.DeweyId))
            {
                ModelState.AddModelError("DeweyId", "");
                return Json(data: new { success = 1, message = "BOŞ YERLERİ DOLDURUNUZ!" }, JsonRequestBehavior.AllowGet);
            }

            // Textbox İsim Boşmu Kontrol Et
            if (string.IsNullOrEmpty(kategori.Isim))
            {
                ModelState.AddModelError("Isim", "");
                return Json(data: new { success = 1, message = "BOŞ YERLERİ DOLDURUNUZ!" }, JsonRequestBehavior.AllowGet);
            }

            // Textboxlarda Validateler (Yazım kuralı) düzgünmü kontrol et.
            if (ModelState.IsValid)
            {
                // Kayıt işleminin başladığını belirten değişkenimizi true yapıyoruz
                isUpdate = true;

                try
                {
                    // DeweyId veritabanında kayıtlımı
                    int data = db.Kategori.Where(i => i.DeweyId == kategori.DeweyId && i.ID != kategori.ID).ToList().Count;
                    if (data != 0)
                    {
                        isUpdate = false;
                        return Json(data: new { success = 3, message = "DEWEY KODU MEVCUT!" }, JsonRequestBehavior.AllowGet);
                    }

                    // İsim veritabanında kayıtlımı
                    data = db.Kategori.Where(i => i.Isim == kategori.Isim && i.ID != kategori.ID).ToList().Count;
                    if (data != 0)
                    {
                        isUpdate = false;
                        return Json(data: new { success = 3, message = "KATREGORİ ADI MEVCUT!" }, JsonRequestBehavior.AllowGet);
                    }


                    // Burada Kayıt İşlemi
                    var ktg = db.Kategori.Where(item => item.ID == kategori.ID).SingleOrDefault();
                    ktg.DeweyId = kategori.DeweyId;
                    ktg.Isim = kategori.Isim;

                    db.SaveChanges();

                    isUpdate = false;
                    return Json(data: new { success = 0, message = "BİLGİLERİ GÜNCELLENDİ!" }, JsonRequestBehavior.AllowGet);

                }
                // Herhangi bir hata durumunda kayıt işleminin devam ettiğini belirten değişkenimizi false yapıyoruz
                catch (Exception e) { isUpdate = false; }
            }
            else
            {
                // Validate hatası olursa buraya giriyor
                return Json(data: new { success = 3, message = "LÜTFEN HATALARI DÜZELTİNİZ!" }, JsonRequestBehavior.AllowGet);
            }

            // Bilmediğim bir hata olursa bitiriyor
            return Json(data: new { success = 1, message = "BİR HATA OLDU!" }, JsonRequestBehavior.AllowGet);
        }
    }
}