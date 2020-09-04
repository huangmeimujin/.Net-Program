using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace SSO.Interface
{
    public interface IBaseService:IDisposable
    {
        #region Insert
        /// <summary>
        /// 单条插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T Insert<T>(T t) where T : class;
        /// <summary>
        /// 多条sql语句拼接，事务插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists"></param>
        /// <returns></returns>
        IEnumerable<T> Insert<T>(IEnumerable<T> lists) where T : class;
        #endregion

        #region Delete
        void Delete<T>(int id)where T:class;

        void Delete<T>(T t) where T : class;

        void Delete<T>(IEnumerable<T> lists) where T : class;
        #endregion

        #region Update
        void Update<T>(T t) where T : class;

        void Update<T>(IEnumerable<T> lists) where T : class; 
        #endregion

        #region Query
        T Find<T>(int id) where T : class;
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> Set<T>() where T : class;
        /// <summary>
        /// 表达式查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class;
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="expression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> expression, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class;
        #endregion

        IQueryable<T> ExecuteQuery<T>(string sql, SqlParameter[] parameter )where T:class;

        void Execute<T>(string sql, SqlParameter[] parameter) where T : class;
    }
}
