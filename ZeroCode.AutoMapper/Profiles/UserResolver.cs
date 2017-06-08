using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeroCode.Repository.Data;

namespace ZeroCode.AutoMapper.Profiles
{
    public class UserResolver : ValueResolver<User,string>
    {
        protected override string ResolveCore(User source)
        {
            switch(source.Position)
            {
                case 0:
                    return "实习生";
                case 1:
                    return "普通职工";
                case 2:
                    return "资深职工";
                case 3:
                    return "团队组长";
                case 4:
                    return "部门经理";
                case 5:
                    return "公司经理";
                default:
                    return "未知";
                    
            }
        }
    }
}
