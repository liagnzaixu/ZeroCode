using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;

namespace ZeroCode.Web.MVC.Extensions
{
    /// <summary>
    /// 扩展辅助操作方法
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 将分页数据转换为表格数据格式
        /// </summary>
        public static GridData<TData> ToGridData<TData>(this PageResult<TData> pageResult)
        {
            return new GridData<TData>(pageResult.Data, pageResult.Total);
        }
    }
}
