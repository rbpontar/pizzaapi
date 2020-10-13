

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApi.Repositories
{
    public abstract class GenericRepo<T> where T : class
    {
        protected ApiContext _contexto;

        public GenericRepo(ApiContext contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<T> GetAll()
        {
            return _contexto.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await _contexto.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {

            return _contexto.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {
            _contexto.Set<T>().Add(t);
            _contexto.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            _contexto.Set<T>().Add(t);
            await _contexto.SaveChangesAsync();
            return t;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _contexto.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _contexto.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _contexto.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _contexto.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _contexto.Set<T>().Remove(entity);
            _contexto.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            _contexto.Set<T>().Remove(entity);
            return await _contexto.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _contexto.Set<T>().Find(key);

            if (exist != null)
            {
                _contexto.Entry(exist).CurrentValues.SetValues(t);
                _contexto.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _contexto.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _contexto.Entry(exist).CurrentValues.SetValues(t);
                await _contexto.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return _contexto.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _contexto.Set<T>().CountAsync();
        }

        public virtual void Save()
        {
            _contexto.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _contexto.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _contexto.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        public virtual IEnumerable<dynamic> CollectionFromSql(string sql, Dictionary<string, object> parameters)
        {
            using (var cmd = _contexto.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> para in parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = para.Key;
                    dbParameter.Value = para.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;
                    }
                }
            }
        }

        private dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
            {
                var nome = dataReader.GetName(fieldCount);
                if (dataRow.Any(d => d.Key.Equals(nome)))
                    nome = nome + fieldCount.ToString();

                dataRow.Add(nome, dataReader[fieldCount]);
            }

            return dataRow;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }






    }
}