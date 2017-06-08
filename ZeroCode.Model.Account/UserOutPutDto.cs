using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.CommonData;

namespace ZeroCode.Model.Account
{
    public class UserOutputDto: IOutputDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 出身年份
        /// </summary>
        public int Bir { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gander { get; set; }
        /// <summary>
        /// 职位，从上至下可分为：
        /// 5.公司经理
        /// 4.部门经理
        /// 3.团队组长
        /// 2.资深职工
        /// 1.普通职工
        /// 0.实习生
        /// </summary>
        public string Position { get; set; }
    }
}
