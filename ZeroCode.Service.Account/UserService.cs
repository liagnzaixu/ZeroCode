using System.Collections.Generic;
using System.Linq;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Model.Account;
using ZeroCode.Repository.Account;
using ZeroCode.Repository.Data;
using ZeroCode.Utility.Extensions;
using AutoMapper;

namespace ZeroCode.Service.Account
{
    public class UserService: IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepositoty)
        {
            this._userRepository = userRepositoty;
        }

        public OperationResult<List<UserOutputDto>> GetUsersToPage(PageCondition condition, FilterGroup filter)
        {
            IQueryable<User> query = _userRepository.Entities.Where(FilterHelper.GetExpression<User>(filter));
            List<User> list = CollectionPropertySorter<User>.OrderBy(query, condition.SortConditions[0].SortField, condition.SortConditions[0].ListSortDirection)
                .Skip((condition.PageIndex - 1) * condition.PageSize)
                .Take(condition.PageSize)
                .ToList();
            List<UserOutputDto> dtoList=Mapper.Map<List<UserOutputDto>>(list);
            return new OperationResult<List<UserOutputDto>>(OperationResultType.Success,"success",dtoList);
        }
    }
}
