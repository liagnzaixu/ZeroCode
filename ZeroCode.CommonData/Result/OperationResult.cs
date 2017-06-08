using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.Utility.Extensions;

namespace ZeroCode.CommonData
{
    public class OperationResult:OperationResult<object>
    {
        
        static OperationResult()
        {
            Success = new OperationResult(OperationResultType.Success);
            NoChanged = new OperationResult(OperationResultType.NoChanged);
        }
        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        /// <param name="resultType"></param>
        public OperationResult(OperationResultType resultType)
            : this(resultType, null, null)
        {

        }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的实例
        /// </summary>
        /// <param name="resultType">操作结果</param>
        /// <param name="message">提示消息</param>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType, message,null)
        {

        }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的实例
        /// </summary>
        /// <param name="resultType">操作结果</param>
        /// <param name="message">提示消息</param>
        /// <param name="data">返回数据</param>
        public OperationResult(OperationResultType resultType,string message,object data)
            :base(resultType,message,data)
        {

        }

        /// <summary>
        /// 获取 成功的操作结果
        /// </summary>
        public static OperationResult Success { get; private set; }

        /// <summary>
        /// 获取 未变更的操作结果
        /// </summary>
        public new static OperationResult NoChanged { get; private set; }
    }

    public class OperationResult<TData> : ZeroCodeResult<OperationResultType, TData>
    {
        static OperationResult()
        {
            NoChanged = new OperationResult<TData>(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的实例
        /// </summary>
        /// <param name="resultType">操作结果</param>
        public OperationResult(OperationResultType resultType)
            : this(resultType, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的实例
        /// </summary>
        /// <param name="resultType">操作结果</param>
        /// <param name="message">提示消息</param>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType, message,default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的实例
        /// </summary>
        /// <param name="resultType">操作结果</param>
        /// <param name="message">提示消息</param>
        /// <param name="data">返回数据</param>
        public OperationResult(OperationResultType resultType,string message,TData data)
            :base(resultType,message,data)
        { }

        /// <summary>
        /// 获取 未变更得操作结果
        /// </summary>
        public static OperationResult<TData> NoChanged { get; private set; }

        /// <summary>
        /// 获取 操作是否成功
        /// </summary>
        public bool Successed
        {
            get { return ResultType == OperationResultType.Success; }
        }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public override string Message
        {
            get { return _message ?? ResultType.ToDescription(); }
            set { _message = value; }
        }
    }
}
