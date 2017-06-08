using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCode.Utility.Properties;
using System.IO;

namespace ZeroCode.Utility.Extensions
{
    public static class ParamterCheckExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion"></param>
        /// <param name="message"></param>
        private static void Require<TException>(bool assertion,string message) where TException:Exception
        {
            if(assertion)
            {
                return;
            }
            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
        }

        /// <summary>
        /// 检测参数不能为空，否则抛出<see cref="ArgumentNullException"/>类型的异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNull<T>(this T value,string paramName) where T:class
        {
            Require<ArgumentNullException>(value != null, string.Format(Resources.ParameterCheck_NotNull, paramName));
        }

        /// <summary>
        /// 检测字符串参数不能为空引用或者空字符串，否则抛出<see cref="ArgumentNullException"/>异常或者<see cref="ArgumentException"/>异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotOrEmpty(this string value, string paramName)
        {
            value.CheckNotNull(paramName);
            Require<ArgumentException>(value.Length > 0, string.Format(Resources.ParameterCheck_NotNullOrEmpty_String, paramName));
        }

        /// <summary>
        /// 检测Guid不能为Guid.Empty，否则抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        public static void CheckNotEmpty(this Guid value,string paramName)
        {
            Require<ArgumentException>(value != Guid.Empty, string.Format(Resources.ParameterCheck_NotNull, paramName));
        }

        /// <summary>
        /// 检测集合不能为空引用或者空集合，否则抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNullOrEmpty<T>(this IEnumerable<T> collection,string paramName)
        {
            collection.CheckNotNull(paramName);
            Require<ArgumentException>(collection.Any(), string.Format(Resources.ParameterCheck_NotNullOrEmpty_Collection, paramName));
        }

        /// <summary>
        /// 检测参数必须小于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名</param>
        /// <param name="target">对比值</param>
        /// <param name="canEqual">是否可等于</param>
        public static void CheckLessThan<T>(this T value, string paramName, T target, bool canEqual = false) where T:IComparable<T>
        {
            bool flag = canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0;
            string message = canEqual ? string.Format(Resources.ParameterCheck_NotGreaterThanOrEqual, paramName, target) : string.Format(Resources.ParameterCheck_NotGreaterThan, paramName, target);
            Require<ArgumentOutOfRangeException>(flag, message);
        }

        /// <summary>
        /// 检测参数必须大于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名</param>
        /// <param name="target">对比值</param>
        /// <param name="canEqual">是否可等于</param>
        public static void CheckGreaterThan<T>(this T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
            string message = canEqual ? string.Format(Resources.ParameterCheck_NotLessThanOrEqual, paramName, target) : string.Format(Resources.ParameterCheck_NotLessThan, paramName, target);
            Require<ArgumentOutOfRangeException>(flag, message);
        }

        /// <summary>
        /// 检查参数必须在指定范围之间，否则抛出<see cref="ArgumentOutOfRangeException"/>异常
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称</param>
        /// <param name="start">比较范围的起始值</param>
        /// <param name="end">比较范围的结束值</param>
        /// <param name="startEqual">是否可等于起始值</param>
        /// <param name="endEqual">是否可等于结束值</param>
        public static void CheckBetween<T>(this T value, string paramName, T start, T end, bool startEqual = false, bool endEqual = false) where T : IComparable<T>
        {
            bool flag = startEqual ? value.CompareTo(start) >= 0 : value.CompareTo(end) > 0;
            string message = startEqual ? string.Format(Resources.ParameterCheck_NotLessThanOrEqual, value, start) : string.Format(Resources.ParameterCheck_NotLessThan, value, start);
            Require<ArgumentOutOfRangeException>(flag, message);

            flag = startEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0;
            message = startEqual ? string.Format(Resources.ParameterCheck_NotGreaterThanOrEqual, value, end) : string.Format(Resources.ParameterCheck_NotGreaterThan, value, end);
            Require<ArgumentOutOfRangeException>(flag, message);
        }

        /// <summary>
        /// 检查指定路径文件夹必须存在，否则抛出<see cref="DirectoryNotFoundExecption"/>异常
        /// </summary>
        /// <param name="directory">路径</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckDirectoryExists(this string directory, string paramName = null)
        {
            CheckNotOrEmpty(directory, paramName);
            Require<DirectoryNotFoundException>(Directory.Exists(directory), string.Format(Resources.ParameterCheck_DirectoryNotExists, directory));
        }

        /// <summary>
        /// 检查指定路径的文件必须存在，否则抛出<see cref="FileNotFoundException"/>异常
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckFileExists(this string filename, string paramName = null)
        {
            CheckNotOrEmpty(filename, paramName);
            Require<FileNotFoundException>(File.Exists(filename), string.Format(Resources.ParameterCheck_DirectoryNotExists, filename));
        }
    }
}
