using ZeroCode.Utility.Extensions;

namespace ZeroCode.CommonData
{
    public class PageCondition
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SortCondition[] SortConditions { get; set; }

        /// <summary>
        /// 初始化一个默认参数（第1页，每页20，排序条件为空）的分页查询条件类 实例
        /// </summary>
        public PageCondition():this(1,20)
        {

        }
        /// <summary>
        /// 初始化一个分页查询条件类 实例
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        public PageCondition(int pageIndex,int pageSize)
        {
            pageIndex.CheckGreaterThan("pageIndex",0);
            pageSize.CheckGreaterThan("pageSize",0);
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortConditions = new SortCondition[] { };
        }
    }
}
