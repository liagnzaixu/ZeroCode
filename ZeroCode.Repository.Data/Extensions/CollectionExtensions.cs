﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using ZeroCode.CommonData;
using ZeroCode.CommonData.Filter;
using ZeroCode.Utility.Extensions;

namespace ZeroCode.Repository.Data.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");
            selector.CheckNotNull("selector");
            return source.ToPage(predicate,
                pageCondition.PageIndex,
                pageCondition.PageSize,
                pageCondition.SortConditions,
                selector);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            SortCondition[] sortConditions,
            Expression<Func<TEntity, TResult>> selector)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);
            selector.CheckNotNull("selector");

            int total;
            TResult[] data = source.Where(predicate, pageIndex, pageSize, out total, sortConditions).Select(selector).ToArray();
            return new PageResult<TResult>() { Total = total, Data = data };
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            out int total)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");

            return source.Where(predicate, pageCondition.PageIndex, pageCondition.PageSize, out total, pageCondition.SortConditions);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            out int total,
            SortCondition[] sortConditions = null)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);

            //if (!typeof(IEntity<>).IsGenericAssignableFrom(type) && !type.IsAbstract)
            //{
            //    throw new InvalidOperationException(Resources.QueryCacheExtensions_TypeNotEntityType.FormatWith(typeof(TEntity).FullName));
            //}

            total = source.Count(predicate);
            source = source.Where(predicate);
            if (sortConditions == null || sortConditions.Length == 0)
            {
                source = source.OrderBy("Id");
            }
            else
            {
                int count = 0;
                IOrderedQueryable<TEntity> orderSource = null;
                foreach (SortCondition sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? CollectionPropertySorter<TEntity>.OrderBy(source, sortCondition.SortField, sortCondition.ListSortDirection)
                        : CollectionPropertySorter<TEntity>.ThenBy(orderSource, sortCondition.SortField, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;
            }
            return source != null
                ? source.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
        }

        #region IQueryable的扩展

        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段与排序方式进行排序
        /// </summary>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <typeparam name="T">动态类型</typeparam>
        /// <returns>排序后的数据集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition<T> sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");
            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.ThenBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.ThenBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /*/// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中筛选指定键范围内的子数据集
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">筛选键类型</typeparam>
        /// <param name="source">要筛选的数据源</param>
        /// <param name="keySelector">筛选键的范围表达式</param>
        /// <param name="start">筛选范围起始值</param>
        /// <param name="end">筛选范围结束值</param>
        /// <param name="startEqual">是否等于起始值</param>
        /// <param name="endEqual">是否等于结束集</param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            TKey start,
            TKey end,
            bool startEqual = false,
            bool endEqual = false) where TKey : IComparable<TKey>
        {
            Expression[] paramters = keySelector.Parameters.Cast<Expression>().ToArray();
            Expression key = Expression.Invoke(keySelector, paramters);
            Expression startBound = startEqual
                ? Expression.GreaterThanOrEqual(key, Expression.Constant(start))
                : Expression.GreaterThan(key, Expression.Constant(start));
            Expression endBound = endEqual
                ? Expression.LessThanOrEqual(key, Expression.Constant(end))
                : Expression.LessThan(key, Expression.Constant(end));
            Expression and = Expression.AndAlso(startBound, endBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }*/

        #endregion
    }
}
