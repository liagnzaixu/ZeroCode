using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using ZeroCode.Repository.Sys;
using ZeroCode.Repository.Data;
using ZeroCode.Model.Sys;
using AutoMapper;
using ZeroCode.CommonData.Filter;
using ZeroCode.CommonData;
using ZeroCode.Repository.Data.Extensions;
using Microsoft.Practices.Unity;

namespace ZeroCode.Service.Sys
{
    public class SysSampleService:ISysSampleService
    {
        public IBaseRepository<SysSample, string> SysRep;
        public SysSampleService(IBaseRepository<SysSample,string> sysRep)
        {
            if (sysRep == null) throw new ArgumentNullException("");
            SysRep = sysRep;
        }

        public List<SysSampleDto> GetAllSys()
        {
            List<SysSample> result = SysRep.Entities.ToList();
            return Mapper.Map<List<SysSampleDto>>(result);
        }

        public PageResult<SysSampleDto> GetSysToPage(GridRequest request)
        {
            request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
            Expression<Func<SysSample, bool>> predicate = FilterHelper.GetExpression<SysSample>(request.FilterGroup);
            return SysRep.Entities.ToPage(predicate, request.PageCondition, 
                m => new SysSampleDto
                {
                    Id=m.Id,
                    Name=m.Name,
                    Note=m.Note,
                    Photo=m.Photo,
                    Age=m.Age.ToString(),
                    Bir=m.Bir.ToString(),
                    CreateTime= m.CreateTime.ToString(),
                });
        }

        public OperationResult Create(SysSampleDto model)
        {
            int execResult= SysRep.Insert(Mapper.Map<SysSample>(model));
            return new OperationResult(execResult == 1 ? OperationResultType.Success:OperationResultType.NoChanged);
        }

        public OperationResult Create(List<SysSampleDto> list)
        {
            return SysRep.Insert(list);
        }


        public string GetS()
        {
            return "";
        }
    }
}
