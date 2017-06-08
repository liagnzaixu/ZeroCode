using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.Utility.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 循环集合每一项，调用委托生成字符串，返回合并后的字符串。默认分割符为逗号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">待处理的集合</param>
        /// <param name="itemFormatFunc">单个集合想的转换委托</param>
        /// <param name="separetor">泛型类型</param>
        /// <returns></returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection,Func<T,string> itemFormatFunc,string separetor=",")
        {
            collection = collection as IList<T> ?? collection.ToList();
            itemFormatFunc.CheckNotNull("itemFormatFunc");
            if(!collection.Any())
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            int count = collection.Count();
            foreach(T item in collection)
            {
                if(i==count-1)
                {
                    sb.Append(itemFormatFunc(item));
                }
                else
                {
                    sb.Append(itemFormatFunc(item));
                    sb.Append(separetor);
                }
                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号“,”
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">待处理的集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection,string separator=",")
        {
           return ExpandAndToString(collection, t => t.ToString(), separator);
        }

        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            collection = collection as IList<T> ?? collection.ToList();
            return !collection.Any();
        }


    }
}
