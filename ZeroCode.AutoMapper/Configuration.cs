using AutoMapper;
using ZeroCode.AutoMapper.Profiles;

namespace ZeroCode.AutoMapper
{
    public class Configuration
    {
        public static void Configure()
        {
            Mapper.Initialize(iconfig =>
            {
                iconfig.AddProfile<SysSampleProfile>();
                iconfig.AddProfile<UserProfile>();
            });
        }
    }
}
