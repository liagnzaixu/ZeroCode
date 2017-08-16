using System.Collections.Generic;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Model.Sys;
using ZeroCode.Model.Core;

namespace ZeroCode.Service.Sys
{
    public interface ISysSampleService
    {
        List<SysSampleDto> GetAllSys();

        PageResult<SysSampleDto> GetSysToPage(GridRequest request);

        OperationResult Create(SysSampleDto model);

        OperationResult Create(List<SysSampleDto> list);

        OperationResult Delete(string id);
        
        OperationResult Update(SysSampleDto model);

        OperationResult<SysSampleDto> GetDetail(string id);

        OperationResult<List<SysModuleTreeDto>> GetModuleTree(string moduleId);
    }

}
