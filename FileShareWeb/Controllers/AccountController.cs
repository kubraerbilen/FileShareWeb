using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileShareWeb.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Account.RegisterModels user)
        {
            
            try
            {
                
                if (context.USER_TABLE.Any(x => x.EMAIL == user.Member.EMAIL))
                {
                    throw new Exception("E-Posta adresi zaten kayıtlıdır.");
                }
                //
                user.Member.ID = Guid.NewGuid();
                //
                context.USER_TABLE.Add(user.Member);
                context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Account.LoginModels model)
        {
            try
            {
                var user = context.USER_TABLE.FirstOrDefault(x => x.PASSWORD == model.Member.PASSWORD && x.EMAIL == model.Member.EMAIL);
                if (user != null)
                {
                    Session["LogonUser"] = user;
                    return RedirectToAction("i", "Home");
                }
                else
                {
                    ViewBag.reError = "Kullanıcı Bilgileriniz Yanlış!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.reError = ex.Message;
                return View();
            }
        }
    }
}