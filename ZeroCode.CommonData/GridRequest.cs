using System;
using System.Collections.Generic;
using ZeroCode.CommonData.Filter;
using ZeroCode.Utility.Extensions;
using ZeroCode.Utility.Data;
using System.ComponentModel;

namespace ZeroCode.CommonData
{
    /// <summary>
    /// Grid查询请求
    /// </summary>
    public class GridRequest
    {
        /// <summary>
        /// 获取查询条件组
        /// </summary>
        public FilterGroup FilterGroup { get; private set; }

        /// <summary>
        /// 获取分页查询条件信息
        /// </summary>
        public PageCondition PageCondition { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="GridRequest"/>类型的新实例
        /// </summary>
        public GridRequest(string filterGroup, string sortField, string sortOrder,int pageIndex=1,int pageSize=20)
        {
            //string jsonWhere = request.Params["where"];
            //int pageIndex = request.Params["page"].CastTo(1);
            //int pageSize = request.Params["rows"].CastTo(20);
            //string sortField = request.Params["sort"];
            //string sortOrder = request.Params["order"];

            FilterGroup = !filterGroup.IsNullOrEmpty() ? JsonHelper.FromJson<FilterGroup>(filterGroup) : new FilterGroup();
            PageCondition = new PageCondition(pageIndex, pageSize);

            if(!sortField.IsNullOrEmpty()&&!sortField.IsNullOrEmpty())
            {
                string[] fields = sortField.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] orders = sortOrder.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if(fields.Length!=orders.Length)
                {
                    throw new ArgumentException("查询列表的排序参数个数不一样");
                }
                List<SortCondition> sortConditions = new List<SortCondition>();
                for (int i = 0; i < fields.Length; i++)
                {
                    ListSortDirection direction = orders[i].ToLower() == "desc" ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    sortConditions.Add(new SortCondition(fields[i], direction));    
                }
                PageCondition.SortConditions = sortConditions.ToArray();
            }
            else
            {
                PageCondition.SortConditions = new SortCondition[] { };
            }
        }

        /// <summary>
        /// 添加默认排序条件，只有排序条件为空时有效
        /// </summary>
        /// <param name="conditions"></param>
        public void AddDefaultSortCondition(params SortCondition[] conditions)
        {
            if(PageCondition.SortConditions.Length==0)
            {
                PageCondition.SortConditions = conditions;
            }
        }
    }
}
