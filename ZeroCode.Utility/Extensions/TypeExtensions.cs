using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZeroCode.Utility.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type">要处理的类型对象</param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            return (type != null && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 用过类型转换器获取Nullable类型的基础类型
        /// </summary>
        /// <param name="type">要处理的类型对象</param>
        /// <returns></returns>
        public static Type GetUnNullableType(this Type type)
        {
            if(IsNullableType(type))
            {
                NullableConverter nullableConverter = new NullableConverter(type);
                return nullableConverter.UnderlyingType;
            }
            return type;
        }


        /// <summary>
        /// 获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string ToDescription(this MemberInfo member,bool inherit =false)
        {
            DescriptionAttribute desc = member.GetCustomAttribute<DescriptionAttribute>();
            if(desc!=null)
            {
                return desc.Description;
            }
            DisplayNameAttribute displayName = member.GetCustomAttribute<DisplayNameAttribute>();
            if (desc != null)
            {
                return displayName.DisplayName;
            }
            DisplayAttribute display = member.GetCustomAttribute<DisplayAttribute>();
            if (desc != null)
            {
                return display.Name; ;
            }
            return member.Name;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo member,bool inherit=false) where T:Attribute
        {
            var descripts=member.GetCustomAttributes(typeof(T),inherit);
            return descripts.FirstOrDefault() as T;
        }

        /// <summary>
        /// 判断当前泛型类型是否可由指定类型的实例填充
        /// </summary>
        /// <param name="genericType">泛型类型</param>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type genericType, Type type)
        {
            genericType.CheckNotNull("genericType");
            type.CheckNotNull("type");
            if (!genericType.IsGenericType)
            {
                throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");
            }

            List<Type> allOthers = new List<Type> { type };
            if (genericType.IsInterface)
            {
                allOthers.AddRange(type.GetInterfaces());
            }

            foreach (var other in allOthers)
            {
                Type cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType)
                    {
                        cur = cur.GetGenericTypeDefinition();
                    }
                    if (cur.IsSubclassOf(genericType) || cur == genericType)
                    {
                        return true;
                    }
                    cur = cur.BaseType;
                }
            }
            return false;
        }

    }
}
