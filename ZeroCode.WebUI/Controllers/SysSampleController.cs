using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Service.Sys;
using ZeroCode.Model.Sys;
using ZeroCode.Web.MVC.UI;
using ZeroCode.Web.MVC.Extensions;
using ZeroCode.WebUI.Properties;

namespace ZeroCode.WebUI.Controllers
{
    public class SysSampleController : Controller
    {
        private readonly ISysSampleService _sysService;

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
            PageResult<SysSampleDto> page= _sysService.GetSysToPage(request);
            return Json( page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(SysSampleDto inputModel)
        {
            OperationResult result= _sysService.Create(inputModel);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            return Json("");
        }

        public ActionResult Detail(string id)
        {
            OperationResult<SysSampleDto> operationResult  = _sysService.GetDetail(id);
            if (!operationResult.Successed)
            {
                return View(Resources.Url_View_NotFound);
            }
            return View(operationResult.Data);
        }

        public ActionResult Edit(string id)
        {
            OperationResult<SysSampleDto> operationResult = _sysService.GetDetail(id);
            if (!operationResult.Successed)
            {
                return View(Resources.Url_View_NotFound);
            }
            return View(operationResult.Data);
        }

        [HttpPost]
        public JsonResult Edit(SysSampleDto inputModel)
        {
            return Json("");
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
    }
}