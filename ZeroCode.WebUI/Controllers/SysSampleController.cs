using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Service.Sys;
using ZeroCode.Model.Sys;
using ZeroCode.Components.Models;
using ZeroCode.Components;
using ZeroCode.HtmlHelpers;
using ZeroCode.Web.MVC.UI;
using ZeroCode.Web.MVC.Extensions;

namespace ZeroCode.WebUI.Controllers
{
    public class SysSampleController : Controller
    {
        private ISysSampleService _sysService;
        public SysSampleController(ISysSampleService sysService)
        {
            this._sysService = sysService;
        }
        //
        // GET: /SysSample/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSysSample()
        {
            GridRequest request = Request.ToGridRequest();
            var page= _sysService.GetSysToPage(request);
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestError()
        {
            throw new Exception();
        }

        public ActionResult TestError2()
        {
            var methodInfo = typeof (string).GetMethod("StartsWith");
            return View();
        }


        public ActionResult TestComponents()
        {
            var d = new Components.Demo();
            return View(d.GetSomething());
        }
    }
}