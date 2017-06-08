using System;

namespace ZeroCode.Repository.Data
{
    /// <summary>
    /// 表示实体将包含创建时间，在创建实体时
    /// </summary>
    public interface ICreatedTime
    {
        DateTime CreateTime { get; set; }
    }
}
