using System.Collections.Generic;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Model.Sys;

namespace ZeroCode.Service.Sys
{
    public interface ISysSampleService
    {
        List<SysSampleOutputDto> GetAllSys();

        PageResult<SysSampleOutputDto> GetSysToPage(GridRequest request);
    }
}
