using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Proiect_Online_Shopping_Anania_Anamaria.Repository
{
    public interface IRepository<Entity> where Entity:class
    {
        IEnumerable<Entity> GetProduct();
        IEnumerable<Entity> GetAllRecords();
        IQueryable<Entity> GetAllRecordsIQuerayble();
        int GetAllRecordsCount();
        void Add(Entity Entity);
        void Update(Entity Entity);
        void UpdateByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict);
        Entity GetFirstOrDefault(int RecordId);
        void Remove(Entity Entity);
        void RemoveByWhereClause(Expression<Func<Entity, bool>> wherePredict);
        IEnumerable<object> GetAllRecordsIQueryable();
        void RemoveRangeByWhereClause(Expression<Func<Entity,bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<Entity, bool>> wherePredict,Action<Entity>ForEachPredict);
        Entity GetFirstOrDefaultByParameter(Expression<Func<Entity, bool>> wherePredict);
        IEnumerable<Entity> GetListParameter(Expression<Func<Entity,bool>> wherePredict);
        IEnumerable<Entity> GetResultBySqlProcedure(String Query, params object[] Parameters);
        IEnumerable<Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Entity, bool>> wherePredict, Expression<Func<Entity, int>> OrderByPredict);
        object Add(int productId);
    }
}