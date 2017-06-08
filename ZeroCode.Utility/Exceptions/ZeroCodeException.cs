using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ZeroCode.Utility.Exceptions
{
    /// <summary>
    /// 框架异常类，当框架逻辑需要抛出异常时使用该类
    /// </summary>
    public class ZeroCodeException:Exception
    {
        /// <summary>
        /// 初始化<see cref="ZeroCodeException"/>类的新实例
        /// </summary>
        public ZeroCodeException()
        {
        }

        /// <summary>
        /// 指定错误信息初始化<see cref="ZeroCodeException"/>类的新实例
        /// </summary>
        /// <param name="message">错误信息</param>
        public ZeroCodeException(string message):base(message)
        {
            
        }

        /// <summary>
        /// 使用指定错误信息和对作为此异常原因的内部异常引用初始化<see cref="ZeroCodeException"/>类的新实例
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public ZeroCodeException(string message,Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// 用序列化数据初始化<see cref="ZeroCodeException"/>类的新实例
        /// </summary>
        /// <param name="info"<>它存储有关引发异常的序列化数据对象/param>
        /// <param name="context">包含源或目标的上下文信息</param>
        public ZeroCodeException(System.Runtime.Serialization.SerializationInfo info,StreamingContext context) : base(info, context)
        {

        }
    }
}
