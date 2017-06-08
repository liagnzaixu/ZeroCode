using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.CommonData.Filter
{
    public class FilterRule
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public FilterOperate Operate { get; set; }

        public FilterRule():this(null,null)
        {

        }

        public FilterRule(string filed,object value):this(filed,value,FilterOperate.Equal)
        {

        }

        public FilterRule(string filed,object value,FilterOperate operate)
        {
            Field = filed;
            Value = value;
            Operate = operate;
        }
    }
}
