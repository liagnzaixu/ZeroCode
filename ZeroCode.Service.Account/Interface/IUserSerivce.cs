using System.Collections.Generic;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Model.Account;

namespace ZeroCode.Service.Account
{
    public interface IUserService
    {
        OperationResult<List<UserOutputDto>> GetUsersToPage(PageCondition condition,FilterGroup filter);
    }
}
