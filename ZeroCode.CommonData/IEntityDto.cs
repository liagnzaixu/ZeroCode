namespace ZeroCode.CommonData
{
    /// <summary>
    /// 定义输入DTO
    /// </summary>
    public interface IInputDto<TKey>
    {
        /// <summary>
        /// 获取胡设置主键，唯一标识
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// 定义输出DTO
    /// </summary>
    public interface IOutputDto
    {

    }
}
