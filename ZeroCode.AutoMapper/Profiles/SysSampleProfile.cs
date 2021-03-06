﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeroCode.Model.Sys;
using ZeroCode.Repository.Data;
using ZeroCode.Model.Core;

namespace ZeroCode.AutoMapper.Profiles
{
    public class SysSampleProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SysSample, SysSampleDto>();
            Mapper.CreateMap<SysSampleDto, SysSample>();

            Mapper.CreateMap<SysModuleDto, SysModule>();
            Mapper.CreateMap<SysModule, SysModuleDto>();
        }
    }
}
