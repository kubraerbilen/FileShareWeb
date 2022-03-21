using FileShareWeb.Filter;
using FileShareWeb.Models.File;
using FileShareWeb.Models.Folder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileShareWeb.Controllers
{
    public class HomeController : BaseController
    {
        // [AllowAnonymous] anonim kullanıcın ulaşmasına izin verir.
        [HttpGet]
        public ActionResult FolderCreate()
        {
            var ff = new DB.FOLDER_TABLE();
            return View(ff);
        }
        [HttpPost]
        public ActionResult FolderCreate(DB.FOLDER_TABLE folder)
        {


            folder.ID = Guid.NewGuid();
            // giriş yapılmış değilse user ıd ayarlanamıyor ve hata veriyor.hatayı almamak için giriş yap!!!
            folder.UserId = CurrentUser().ID;
            context.FOLDER_TABLE.Add(folder);
            context.SaveChanges();
            return RedirectToAction("FolderCreate", "Home");

        }
        [HttpGet]
        public ActionResult i()                            // klasör listeleme
        {
            var fold = context.FOLDER_TABLE.Where(x => x.ID != null).ToList();
            return View(fold.OrderByDescending(x => x.KlasorAdi).ToList());
        }
        [HttpGet]
        public ActionResult Delete(string id)               //kalsör silme
        {
            var guid = new Guid(id);
            var folders = context.FOLDER_TABLE.FirstOrDefault(x => x.ID == guid);
            context.FOLDER_TABLE.Remove(folders);
            context.SaveChanges();
            return RedirectToAction("i", "Home");
        }

        public ActionResult Edit(string id = null)   // string id olan i.cshtml deki method 
        {

            // var cat = context.FOLDER_TABLE.FirstOrDefault(x => x.ID ==id );
            var categories = context.FOLDER_TABLE.Select(x => new SelectListItem()
            {
                Text = x.KlasorAdi,
                Value = x.ID.ToString()
            }).ToList();
            categories.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Ana Kategori",
                Selected = true
            });
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(DB.FOLDER_TABLE folder)   //edit.cshtml edit methodu 
        {
            DB.FOLDER_TABLE _folder = null;
            if (folder.ID == Guid.Empty)
            {
                var fol = context.FOLDER_TABLE.FirstOrDefault(x => x.ID == folder.ID);
                folder.ID = Guid.NewGuid();
                folder.KlasorAdi = fol.KlasorAdi;
                folder.KlasorAciklamasi = fol.KlasorAciklamasi;
                folder.UserId = CurrentUser().ID;
                context.FOLDER_TABLE.Add(folder);
            }
            else
            {
                _folder = context.FOLDER_TABLE.FirstOrDefault(x => x.ID == folder.ID);
                _folder.KlasorAdi = folder.KlasorAdi;
                _folder.KlasorAciklamasi = folder.KlasorAciklamasi;
                _folder.UserId = CurrentUser().ID;
            }
            context.SaveChanges();
            return RedirectToAction("i", "Home");
        }
        public ActionResult DosyaYukle(System.Web.HttpPostedFileBase yuklenecekDosya)
        {
            DB.FILE_TABLE _file = null;
            if (yuklenecekDosya != null)
            {
                string dosyaYolu = Path.GetFileName(yuklenecekDosya.FileName);
                string dosyaAdi = "-" + Guid.NewGuid().ToString().Replace("-", "");
                string klasor = CurrentUser().ID.ToString() ;
                var yuklemeYeri = Path.Combine(Server.MapPath("~/App_Data" + "/" + klasor + "/" + dosyaAdi)); // her kullanıcı id si için nasıl dosya oluşur ????
                yuklenecekDosya.SaveAs(yuklemeYeri);
                //_file.DosyaAdi = yuklenecekDosya.FileName;
                //_file.DosyaBoyutu = (yuklenecekDosya).ContentLength;
                //_file.DosyayuklemeZamani = DateTime.Now;
                //_file.FolderId = CurrentUser().ID;
                //_file.DosyaUzantisi = yuklenecekDosya.ContentType;
            }
            return View();
        }
        public ActionResult Files()
        {
            var fold = context.FILE_TABLE.Where(x => x.ID != null).ToList();
            return View(fold.OrderByDescending(x => x.DosyaAdi).ToList());
        }
        [HttpGet]
        public ActionResult DeleteFile(string id)               //kalsör silme
        {
            var guid = new Guid(id);
            var files = context.FILE_TABLE.FirstOrDefault(x => x.ID == guid);
            context.FILE_TABLE.Remove(files);
            context.SaveChanges();
            return RedirectToAction("i", "Home");
        }


    }
}

