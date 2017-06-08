using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.Repository.Data;

namespace ZeroCode.Repository.Account
{
    public class UserRepository: BaseRepository<User,string>,IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
