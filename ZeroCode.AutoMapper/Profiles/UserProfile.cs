using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.Model.Account;
using ZeroCode.Repository.Data;

namespace ZeroCode.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserOutputDto>()
                .ForMember(dest => dest.Bir, options => options.MapFrom(s => DateTime.Now.Year - Convert.ToInt32(s.Age)))
                .ForMember(dest => dest.Position, 
                    options => options.ResolveUsing<UserResolver>());
        }
    }
}
