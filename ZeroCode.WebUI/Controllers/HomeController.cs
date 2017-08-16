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
        public JsonResult GetTree(string moduleId)
        {
            OperationResult<List<SysModuleTreeDto>> op = _sysService.GetModuleTree(moduleId);
            //if(op.Successed)
            //{
            //    var jsonData = (
            //        from m in op.Data
            //        select new
            //        {
            //            id = m.Id,
            //            text = m.Name,
            //            value = m.Id,
            //            showcheck = false,
            //            complete = false,
            //            isexpand = false,
            //            checkstate = 0,
            //            hasChildren = m.IsLast ? false : true,
            //            Icon = m.Iconic
            //        }).ToArray();

            //    AjaxResult result = new AjaxResult(AjaxResultType.Success, "success", jsonData);
            //    return Json(result);
            //}
            return Json(op.ToAjaxResult());
        }
    }
}