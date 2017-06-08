using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.CommonData.Filter
{
    public class OperateCodeAttribute: Attribute
    {
        public string Code { get; private set; }

        public OperateCodeAttribute(string code)
        {
            Code = code;
        }
    }
}
