
namespace ZeroCode.Web.MVC.UI
{
    public enum AjaxResultType
    {
        /// <summary>
        /// 信息结果类型
        /// </summary>
        Info = 100,
        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success =200,
        /// <summary>
        /// 错误请求结果类型
        /// </summary>
        ClientError=400,
        /// <summary>
        /// 服务器异常结果类型
        /// </summary>
        ServerError=500
    }
}
