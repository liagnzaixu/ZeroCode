using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZeroCode.CommonData;

namespace ZeroCode.Repository.Data
{
    /// <summary>
    /// 实体仓储模型的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseRepository<TEntity,TKey> where TEntity:IEntity<TKey> where TKey:IEquatable<TKey>
    {
        /// <summary>
        /// 获取当前单元操作对象
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> Entities { get; }

        IQueryable<TEntity> TrackEntities { get; }

        int Insert(TEntity entity);

        int Insert(IEnumerable<TEntity> entities);

        OperationResult Insert<TInputDto>(ICollection<TInputDto> dtos, Action<TInputDto> checkAction = null, Func<TInputDto, TEntity, TEntity> convertFunc = null) where TInputDto : IInputDto<TKey>;

        int Recycle(TEntity entity);

        int Recycle(TKey key);

        int Recycle(Expression<Func<TEntity, bool>> predicate);

        int Recycle(IEnumerable<TEntity> entities);

        int Restore(TEntity entity);

        int Restore(TKey key);

        int Restore(Expression<Func<TEntity, bool>> predicate);

        int Restore(IEnumerable<TEntity> entities);

        int Delete(TEntity entity);

        int Delete(TKey key);

        int Delete(Expression<Func<TEntity, bool>> predicate);

        int Delete(IEnumerable<TEntity> entities);

        OperationResult Delete(ICollection<TKey> ids, Action<TEntity> checkAction = null, Func<TEntity, TEntity> deleteFunc = null);

        int DeleteDirect(TKey key);

        int DeleteDirect(Expression<Func<TEntity, bool>> predicate);

        int Update(TEntity entity);

        OperationResult Update<TEditDto>(ICollection<TEditDto> dtos, Action<TEditDto, TEntity> checkAction = null, Func<TEditDto, TEntity, TEntity> convertFunc = null) where TEditDto : IInputDto<TKey>;

        int UpdateDirect(TKey key, Expression<Func<TEntity, TEntity>> updateExpression);

        int UpdateDirect(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExperssion);

        bool CheckExists(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey));

        TEntity GetByKey(TKey key);

        IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path);

        IQueryable<TEntity> GetIncludes(params string[] paths);

        IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters);
    }
}
