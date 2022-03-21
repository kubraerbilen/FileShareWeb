using FileShareWeb.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileShareWeb.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected FileShareEntities context { get; private set; }
        public BaseController()
        {
            context = new FileShareEntities();
        }
        protected DB.USER_TABLE CurrentUser()
        {
            if (Session["LogonUser"] == null) return null;
            return (DB.USER_TABLE)Session["LogonUser"];
        }
        //curentuserıd hata veriyor yeni yorum yapamıyorum.
        //protected int CurrentUserId()
        //{
        //    if (Session["LogonUser"] == null) return 0;
        //    return ((DB.USER_TABLE)Session["LogonUser"]).ID;
        //}
        protected bool IsLogon()
        {
            if (Session["LogonUser"] == null)
                return false;
            else
                return true;
        }
    }
}