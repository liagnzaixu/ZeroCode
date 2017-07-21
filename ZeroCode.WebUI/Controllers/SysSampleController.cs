using System;
using System.Web.Mvc;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Model.Sys;
using ZeroCode.Service.Sys;
using ZeroCode.Web.MVC.Extensions;
using ZeroCode.Web.MVC.UI;
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
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SysSampleDto inputModel)
        {
            if (!ModelState.IsValid)
            {
                if(Request.IsAjaxRequest())
                { 
                    return Json(new AjaxResult(AjaxResultType.ClientError, "数据验证错误", null));
                }
                else
                {
                    return View();
                }
            }
            if (Request.IsAjaxRequest())
            {
                OperationResult result = _sysService.Create(inputModel);
                return Json(result.ToAjaxResult());
            }
            else
            {
                OperationResult result = _sysService.Create(inputModel);
                return View();
            }
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            OperationResult result = _sysService.Delete(id);
            return Json(result.ToAjaxResult());
        }

        public ActionResult Detail(string id)
        {
            OperationResult<SysSampleDto> result = _sysService.GetDetail(id);
            if (!result.Successed)
            {
                return View(Resources.Url_View_NotFound);
            }
            return View(result.Data);
        }

        public ActionResult Edit(string id)
        {
            OperationResult<SysSampleDto> result = _sysService.GetDetail(id);
            if (!result.Successed)
            {
                return View(Resources.Url_View_NotFound);
            }
            return View(result.Data);
        }

        [HttpPost]
        public JsonResult Edit(SysSampleDto inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult(AjaxResultType.ClientError, "数据验证错误", null));
            }
            OperationResult result = _sysService.Update(inputModel);
            return Json(result.ToAjaxResult());
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