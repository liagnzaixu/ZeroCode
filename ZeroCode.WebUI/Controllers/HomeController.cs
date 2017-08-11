using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroCode.Service.Sys;
using ZeroCode.CommonData;
using ZeroCode.Model.Core;
using ZeroCode.Web.MVC.UI;



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

        [HttpPost]
        public JsonResult GetTree()
        {
            OperationResult<List<SysModuleDto>> op = _sysService.GetModuleTree();
            return Json(op.ToAjaxResult());
        }
    }
}