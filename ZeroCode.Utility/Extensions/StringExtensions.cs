using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.Utility.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 为指定格式的字符串填充相应对象来生成字符串
        /// </summary>
        /// <param name="format">字符串格式，占位符以{n}表示</param>
        /// <param name="args">用于填充占位符的参数</param>
        /// <returns>格式后的字符串</returns>
        [DebuggerStepThrough]
        public static string FormatWith(this string format,params object[] args)
        {
            format.CheckNotNull("format");
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        [DebuggerStepThrough]
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 将字符串反转
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReverseString(this string value)
        {
            value.CheckNotNull("value");
            return new string(value.Reverse().ToArray());
        }

        /// <summary>
        /// 将Json字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string json)
        {
            json.CheckNotNull("json");
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 给Url添加查询参数
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <param name="queries">要添加的参数，例如："id=2,name=jack"</param>
        /// <returns></returns>
        public static string AddUrlQuery(this string url,params string[] queries)
        {
            foreach(string query in queries)
            {
                if (!url.Contains("?"))
                {
                    url += "?";
                }
                else if (!url.EndsWith("&"))
                {
                    url += "&";
                }
                url = url + query;
            }
            return url;
        }
    }
}
