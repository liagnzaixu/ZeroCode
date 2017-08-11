using System;
using ZeroCode.CommonData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZeroCode.Model.Sys
{
    public class SysSampleDto:IOutputDto,IInputDto<string>
    {
        [Required]
        [DisplayName("账号")]
        [MaxLength(50, ErrorMessage = "上限50个字符")]
        public string Id { get; set; }

        [Required]
        [DisplayName("姓名")]
        [MaxLength(30, ErrorMessage = "上限30个字符")]
        public string Name { get; set; }

        [Required]
        [DisplayName("年龄")]
        [Range(1,120)]
        public int Age { get; set; }

        [DisplayName("生日")]
        public DateTime Bir { get; set; }

        [DisplayName("头像")]
        public string Photo { get; set; }

        [DisplayName("备注")]
        public string Note { get; set; }

        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
