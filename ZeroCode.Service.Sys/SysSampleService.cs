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
using ZeroCode.Utility.Extensions;
using ZeroCode.Model.Core;

namespace ZeroCode.Service.Sys
{
    public class SysSampleService:ISysSampleService
    {
        public IBaseRepository<SysSample, string> SysRep;
        public IBaseRepository<SysModule, string> ModuleRep;
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
                    Age=(int)m.Age,
                    Bir=(DateTime)m.Bir,
                    CreateTime= (DateTime)(m.CreateTime),
                });
        }

        public OperationResult Create(SysSampleDto model)
        {
            model.CreateTime = DateTime.Now;
            int execResult= SysRep.Insert(Mapper.Map<SysSample>(model));
            return new OperationResult(execResult == 1 ? OperationResultType.Success:OperationResultType.NoChanged);
        }

        public OperationResult Create(List<SysSampleDto> list)
        {
            return SysRep.Insert(list);
        }

        public OperationResult Delete(string id)
        {
            if (id.IsNullOrEmpty()) throw new ArgumentNullException("id");
            int execResult= SysRep.DeleteDirect(id);
            return new OperationResult(execResult == 1 ? OperationResultType.Success : OperationResultType.QueryNull);
        }

        public OperationResult Update(SysSampleDto model)
        {
            if (model==null) throw new ArgumentNullException("id");
            SysSample entity = Mapper.Map<SysSample>(model);
            int execResult = SysRep.UpdateDirect(model.Id, user => new SysSample { Name = entity.Name, Age=entity.Age, Bir= entity.Bir, Note= entity.Note, Photo= entity.Photo });
            return new OperationResult(execResult == 1 ? OperationResultType.Success : OperationResultType.Error);
        }

        public OperationResult<SysSampleDto> GetDetail(string id)
        {
            SysSampleDto dto= Mapper.Map<SysSampleDto>(SysRep.GetByKey(id));
            if (dto == null)
            {
                return new OperationResult<SysSampleDto>(OperationResultType.QueryNull);
                
            }
            return new OperationResult<SysSampleDto>(OperationResultType.Success, null, dto);
        }

        public OperationResult<List<SysModuleDto>> GetModuleTree()
        {
            try
            { 
                List<SysModule> entityList= ModuleRep.Entities.Where(m => m.Id != "0").Distinct().OrderBy(a=>a.Sort).ToList();
                if(entityList.Count==0)
                {
                    return new OperationResult<List<SysModuleDto>>(OperationResultType.QueryNull);
                }

                List<SysModuleDto> dtoList = Mapper.Map<List<SysModuleDto>>(entityList);
                return new OperationResult<List<SysModuleDto>>(OperationResultType.Success, null, dtoList);
            }
            catch(Exception ex)
            {
                return new OperationResult<List<SysModuleDto>>(OperationResultType.Error);
            }
        }
    }
}
