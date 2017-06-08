using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.CommonData.Filter
{
    /// <summary>
    /// 数据分页信息
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 获取或设置 分页数据
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// 获取或设置 总记录数
        /// </summary>
        public int Total { get; set; }
    }
}
