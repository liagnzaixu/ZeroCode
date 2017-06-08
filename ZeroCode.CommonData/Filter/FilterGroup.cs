using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.CommonData.Filter
{
    public class FilterGroup
    {
        private FilterOperate _operate;
        public ICollection<FilterRule> Rules { get; set; }
        public ICollection<FilterGroup> Groups { get; set; }
        public FilterOperate Operate
        {
            get { return _operate; }
            set
            {
                if(value!=FilterOperate.And&&value!=FilterOperate.Or)
                {
                    throw new InvalidOperationException("查询条件组中的操作类型错误，只能为\"Or\"或者\"And\"。");
                }
                _operate = value;    
            }
        }

        public FilterGroup():this(FilterOperate.And)
        {

        }

        public FilterGroup(string operateCode)
        {

        }

        public FilterGroup(FilterOperate operate)
        {
            Operate = operate;
            Rules = new List<FilterRule>();
            Groups = new List<FilterGroup>();
        }
    }
}
