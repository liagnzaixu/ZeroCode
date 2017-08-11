using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.CommonData;

namespace ZeroCode.Model.Core
{
    public class SysModuleDto: IOutputDto, IInputDto<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Iconic { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public bool State { get; set; }
        public bool IsLast { get; set; }
    }
}
