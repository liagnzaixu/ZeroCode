using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZeroCode.CommonData;
using ZeroCode.Utility.Extensions;

namespace ZeroCode.Web.MVC.UI
{
    public static class Extensions
    {
        /// <summary>
        /// 将业务操作结果转换为ajax操作结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">业务操作结果</param>
        /// <returns></returns>
        public static AjaxResult ToAjaxResult<T>(this OperationResult<T> result)
        {
            string message= result.Message ?? result.ResultType.ToDescription();
            return new AjaxResult(result.ResultType.ToAjaxResultType(), message, result.Data);
        }

        /// <summary>
        /// 将业务操作结果转换为ajax操作结果
        /// </summary>
        /// <param name="result">业务操作结果</param>
        /// <returns></returns>
        public static AjaxResult ToAjaxResult(this OperationResult result)
        {
            string message = result.Message ?? result.ResultType.ToDescription();
            return new AjaxResult(result.ResultType.ToAjaxResultType(), message, result.Data);
        }

        /// <summary>
        /// 将业务操作结果类型<see cref="OperationResultType"/>转换为ajax操作结果类型<see cref="AjaxResultType"/>
        /// </summary>
        /// <param name="type">业务操作结果</param>
        /// <returns></returns>
        public static AjaxResultType ToAjaxResultType(this OperationResultType type)
        {
            switch(type)
            {
                case OperationResultType.Error:
                    return AjaxResultType.ServerError;
                case OperationResultType.NoChanged:
                    return AjaxResultType.Info;
                case OperationResultType.QueryNull:
                    return AjaxResultType.Info;
                case OperationResultType.Success:
                    return AjaxResultType.Success;
                case OperationResultType.ValidError:
                    return AjaxResultType.ClientError;
                default:
                    return AjaxResultType.Success;
            }
        }

        /// <summary>
        /// 判断业务结果类型是否是Error结果
        /// </summary>
        public static bool IsError(this OperationResultType resultType)
        {
            return resultType == OperationResultType.QueryNull || resultType == OperationResultType.ValidError
                || resultType == OperationResultType.Error;
        }

        public static GridRequest ToGridRequest(this HttpRequestBase httpRequestBase)
        {
            return new GridRequest(httpRequestBase.Params["where"], httpRequestBase.Params["sort"], httpRequestBase.Params["order"], httpRequestBase.Params["page"].CastTo(1), httpRequestBase.Params["rows"].CastTo(20));
        }
    }
}
