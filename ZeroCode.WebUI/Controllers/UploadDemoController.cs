using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
            string result = "上传成功";
            HttpPostedFileBase file= Request.Files["UserPhoto"];
            file.SaveAs(Server.MapPath("~/Upload/")+ file.FileName);
            return Content(result);
        }

        [HttpPost]
        public ActionResult UploadFile_2()
        {
            HttpPostedFileBase file = Request.Files["UserPhoto"];
            file.SaveAs(Server.MapPath("~/Upload/") + file.FileName);

            return Json("success");
        }
    }
}