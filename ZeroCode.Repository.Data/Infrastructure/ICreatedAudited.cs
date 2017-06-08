namespace ZeroCode.Repository.Data
{
    /// <summary>
    /// 给信息添加创建时间、创建者属性，在实体创建是，将自动提取当前用户为创建者
    /// </summary>
    public interface ICreatedAudited:ICreatedTime
    {
        /// <summary>
        /// 获取或设置 创建者编号
        /// </summary>
        string CreatorUserId { get; set; }
    }
}
