//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeroCode.Repository.Data
{
    
    
    using System;
    using System.Collections.Generic;
    
    public partial class User:IEntity<string>
    {
        public string Id { get; set; }
        public int Age { get; set; }
        public string Gander { get; set; }
        public Nullable<int> Position { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public string IDCardNo { get; set; }
        public string TelePhone { get; set; }
        public string MobilePhone { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public string CreatorID { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public string Wechat { get; set; }
        public string Address { get; set; }
    
    }
}
