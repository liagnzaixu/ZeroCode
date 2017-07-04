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

           List<SysSampleDto> list = new List<SysSampleDto>() 
           { 
                new SysSampleDto()
                {
                    Id = "222225",
                    Age = "15",
                    CreateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Bir = "1991-04-06 08:08:08",
                    Name = "Aries",
                    Note = "I'm liagnzaixu",
                    Photo = "http://www.baidu.com"
                },
                new SysSampleDto()
                {
                    Id = "222223",
                    Age = "17",
                    CreateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Bir = "1991-04-06 08:08:08",
                    Name = "Aries",
                    Note = "I'm liagnzaixu",
                    Photo = "http://www.baidu.com"
                },
                new SysSampleDto()
                {
                    Id = "222224",
                    Age = "18",
                    CreateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Bir = "1991-04-06 08:08:08",
                    Name = "Aries",
                    Note = "I'm liagnzaixu",
                    Photo = "http://www.baidu.com"
                },
           };
           OperationResult result = _sysService.Create(list);
            return View();
        }

        [HttpPost]
        public JsonResult Create(SysSampleDto inputModel)
        {
            inputModel = new SysSampleDto()
            {
                Id = "222222",
                Age = "15",
                CreateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Bir = "1991-04-06 08:08:08",
                Name = "Aries",
                Note = "I'm liagnzaixu",
                Photo = "http://www.baidu.com"
            };
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
            return View();
        }

        public ActionResult Edit(string id)
        {
            return View();
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