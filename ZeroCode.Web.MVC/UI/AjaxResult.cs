using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.Web.MVC.UI
{
    public class AjaxResult
    {
        public AjaxResult()
        {

        }

        public AjaxResult(AjaxResultType type,string message, object data)
        {
            Status = type.ToString();
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string messages, AjaxResultType type = AjaxResultType.Success, object data = null)
            : this(messages, data, type)
        { }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string message, object data, AjaxResultType type = AjaxResultType.Success)
        {
            Status = type.ToString();
            Message = message;
            Data = data;
        }


        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
