using System;
using ZeroCode.CommonData;

namespace ZeroCode.Model.Sys
{
    public class SysSampleOutputDto:IOutputDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public DateTime Bir { get; set; }
        public string Photo { get; set; }
        public string Note { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
