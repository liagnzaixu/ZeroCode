using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.Repository.Data;
using System.Data.Entity;

using SysSample = ZeroCode.Repository.Data.SysSample;


namespace ZeroCode.Repository.Sys
{
    public class SysSampleRepository: BaseRepository<SysSample,string>,ISysSampleRepository
    {
        public SysSampleRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
