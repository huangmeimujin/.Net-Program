using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SSO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Service
{
    public class BaseService : IBaseService
    {
        protected DbContext DBCountext { get; private set; }

        public BaseService(DbContext dbContext)
        {
            this.DBCountext = dbContext;
        }

        #region Insert
        public T Insert<T>(T t) where T : class
        {
            this.DBCountext.Set<T>().Add(t);
            this.DBCountext.SaveChanges();
            return t;
        }

        public IEnumerable<T> Insert<T>(IEnumerable<T> lists) where T : class
        {
            this.DBCountext.Set<T>().AddRange(lists);
            this.DBCountext.SaveChanges();
            return lists;
        }

        #endregion

        #region Delete
        public void Delete<T>(int id)where T:class
        {
            T t= this.Find<T>(id);
            if (t == null) throw new Exception("t is null");
            this.DBCountext.Set<T>().Remove(t);
            this.DBCountext.SaveChanges();
        }

        public void Delete<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");
            this.DBCountext.Set<T>().Remove(t);
            this.DBCountext.SaveChanges();
        }

        public void Delete<T>(IEnumerable<T> lists) where T : class
        {
            foreach (T t in lists)
            {
                this.DBCountext.Set<T>().Attach(t);
            }
            this.DBCountext.Set<T>().RemoveRange(lists);
            this.DBCountext.SaveChanges();
        }

        #endregion

        #region Query
        public T Find<T>(int id) where T : class
        {
            return this.DBCountext.Set<T>().Find(id);
        }


        public IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class
        {
            if (expression == null)
                return this.DBCountext.Set<T>();
            else
                return this.DBCountext.Set<T>().Where<T>(expression);
        }

        public PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> expression, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = this.Set<T>();
            if(expression!=null)
            {
                list = list.Where<T>(expression);
            }
            if (isAsc)
            {
                list = list.OrderBy(funcOrderby);

            }
            else
            {
                list = list.OrderByDescending(funcOrderby);
            }
            return new PageResult<T>() {
                Results=list.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList(),
                PageIndex=pageIndex,
                PageSize=pageSize,
                TotalCount=list.Count()
            };
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return this.DBCountext.Set<T>();
        }
        #endregion

        #region Update
        public void Update<T>(T t) where T : class
        {
            this.DBCountext.Set<T>().Update(t);
            this.DBCountext.SaveChanges();
        }

        public void Update<T>(IEnumerable<T> lists) where T : class
        {
            foreach (T t in lists)
            {
                this.DBCountext.Set<T>().Attach(t);
            }
            this.DBCountext.Set<T>().UpdateRange(lists);
            this.DBCountext.SaveChanges();
        }
        #endregion
        #region Execute Sql

        public IQueryable<T> ExecuteQuery<T>(string sql, SqlParameter[] parameter) where T : class
        {
            return this.DBCountext.Set<T>().FromSqlRaw<T>(sql, parameter);
        }

        public void Execute<T>(string sql, SqlParameter[] parameter) where T : class
        {
            throw new NotImplementedException();
        } 
        #endregion
        public virtual void Dispose()
        {
            if (this.DBCountext != null)
            {
                this.DBCountext.Dispose();
            }
        }

       
    }
}
