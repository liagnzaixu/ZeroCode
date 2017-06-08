using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ZeroCode.Utility.Extensions
{
    public static class MapperExtensions
    {
        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            return Mapper.Map<TTarget>(source);
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
            return Mapper.Map(source, target);
        }
    }
}
