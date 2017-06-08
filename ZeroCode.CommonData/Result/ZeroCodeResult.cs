
namespace ZeroCode.CommonData
{
    /// <summary>
    /// ZeroCode结果基类
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public abstract class ZeroCodeResult<TResultType> : ZeroCodeResult<TResultType, object>, IZeroCodeResult<TResultType>
    {
        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult(TResultType type)
            : this(type, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult(TResultType type, string message)
            : this(type, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult(TResultType type, string message, object data)
            : base(type, message, data)
        { }
    }


    /// <summary>
    /// ZeroCode结果基类
    /// </summary>
    /// <typeparam name="TResultType">结果类型</typeparam>
    /// <typeparam name="TData">结果数据类型</typeparam>
    public abstract class ZeroCodeResult<TResultType, TData> : IZeroCodeResult<TResultType, TData>
    {
        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult(TResultType type)
            : this(type, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult(TResultType type, string message)
            : this(type, message, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="ZeroCodeResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ZeroCodeResult(TResultType type, string message, TData data)
        {
            ResultType = type;
            _message = message;
            Data = data;
        }

        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        public TResultType ResultType { get; set; }


        protected string _message;

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public virtual string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        public TData Data { get; set; }
    }
}
