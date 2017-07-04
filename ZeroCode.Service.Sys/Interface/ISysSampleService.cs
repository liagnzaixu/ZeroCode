using System.Collections.Generic;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Model.Sys;

namespace ZeroCode.Service.Sys
{
    public interface ISysSampleService
    {
        List<SysSampleDto> GetAllSys();

        PageResult<SysSampleDto> GetSysToPage(GridRequest request);

        OperationResult Create(SysSampleDto model);
    }
}
