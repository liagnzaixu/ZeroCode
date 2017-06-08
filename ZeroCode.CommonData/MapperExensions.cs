using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.Utility.Extensions;

namespace ZeroCode.Core.Mapping
{
    public static class MapperExensions
    {
        private static IMapper _mapper;

        public static void SetMapper(IMapper mapper)
        {
            mapper.CheckNotNull("mapper");
            _mapper = mapper;
        }

        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            CheckMapper();
            return _mapper.MapTo<TTarget>(source);
        }

        /// <summary>
        /// 使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            CheckMapper();
            return _mapper.MapTo(source, target);
        }

        /// <summary>
        /// 验证映射执行者是否为空
        /// </summary>
        private static void CheckMapper()
        {
            if (_mapper == null)
            {
                throw new NullReferenceException(Resources.Map_MapperIsNull);
            }
        }
    }
}
