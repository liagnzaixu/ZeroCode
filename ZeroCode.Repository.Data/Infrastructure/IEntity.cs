using System;

namespace ZeroCode.Repository.Data
{
    /// <summary>
    /// 数据模型接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<out TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取 实体唯一标识，主键
        /// </summary>
        TKey Id { get; }
    }
}
