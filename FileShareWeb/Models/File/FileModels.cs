using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileShareWeb.Models.File
{
    public class FileModels
    {
        public string dosyaAdi { get; set; }
        public DB.FILE_TABLE FileTable { get; set; }
        public HttpPostedFileBase Dosya { get; set; }
    }
}