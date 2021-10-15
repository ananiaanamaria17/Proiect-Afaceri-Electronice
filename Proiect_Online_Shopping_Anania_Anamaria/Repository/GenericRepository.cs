using Proiect_Online_Shopping_Anania_Anamaria.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Proiect_Online_Shopping_Anania_Anamaria.Repository
{
    public class GenericRepository<Entity> : IRepository<Entity> where Entity : class
    {
        DbSet<Entity> _dbSet;
        private AEOnlineShoppingEntities _DBEntity;
        public GenericRepository(AEOnlineShoppingEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<Entity>();
        }
        public IEnumerable<Entity> GetProduct()
        {
            return _dbSet.ToList();
        }

        public void Add(Entity Entity)
        {
            _dbSet.Add(Entity);
            _DBEntity.SaveChanges();
        }

        public IEnumerable<Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public int GetAllRecordsCount()
        {

            return _dbSet.Count();
        }

        public IQueryable<Entity> GetAllRecordsIQuerayble()
        {
            return _dbSet;
        }

        public IEnumerable<object> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public Entity GetFirstOrDefault(int RecordId)
        {
            return _dbSet.Find(RecordId);
        }

        public Entity GetFirstOrDefaultByParameter(Expression<Func<Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Entity> GetListParameter(Expression<Func<Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Entity, bool>> wherePredict, Expression<Func<Entity, int>> OrderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(OrderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(OrderByPredict).ToList();
            }    
        }

        public IEnumerable<Entity> GetResultBySqlProcedure(string Query, params object[] Parameters)
        {
            if (Parameters != null)
            {
                return _DBEntity.Database.SqlQuery<Entity>(Query, Parameters).ToList();
            }
            else
            { return _DBEntity.Database.SqlQuery<Entity>(Query).ToList(); 
            }
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(Entity Entity)
        {
            if (_DBEntity.Entry(Entity).State == EntityState.Detached) 
            _dbSet.Attach(Entity);
            _dbSet.Remove(Entity);
        }

        public void RemoveByWhereClause(Expression<Func<Entity, bool>> wherePredict)
        {
            Entity Entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(Entity);

        }

        public void RemoveRangeByWhereClause(Expression<Func<Entity, bool>> wherePredict)
        {
            List<Entity> Entity = _dbSet.Where(wherePredict).ToList();
            foreach(var ent in Entity)
            {
                Remove(ent);
            }
        }

        public void Update(Entity Entity)
        {
            _dbSet.Attach(Entity);
            _DBEntity.Entry(Entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public object Add(int productId)
        {
            throw new NotImplementedException();
        }
    }
}