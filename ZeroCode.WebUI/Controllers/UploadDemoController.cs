using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZeroCode.WebUI.Controllers
{
    public class UploadDemoController : Controller
    {
        // GET: UploadDemo
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile_1()
        {
            HttpPostedFileBase file= Request.Files["UserPhoto"];
            file.SaveAs(Server.MapPath("~/Upload/")+ file.FileName);
            return Content("上传成功");
        }

        [HttpPost]
        public ActionResult UploadFile_2(HttpPostedFileBase UserPhoto)
        {
            string[] temp = UserPhoto.FileName.Split(new char[] { '.' },StringSplitOptions.RemoveEmptyEntries);
            string format=temp[temp.Length - 1];
            UserPhoto.SaveAs(Server.MapPath("~/Upload/") + DateTime.Now.ToString("yyyyMMddhhmmss.")+ format);
            return Content("上传成功");
        }
    }
}