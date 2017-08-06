using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroCode.Service.Sys;



namespace ZeroCode.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ISysSampleService _sysService;
        public HomeController(ISysSampleService sysService)
        {
            this._sysService = sysService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyIndex()
        {
            return View();
        }
    }
}