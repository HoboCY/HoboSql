using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HoboSql
{
    public interface IDbHelper<TEntity, in TKey> where TKey : struct 
        where TEntity : new()

    {
        Task<TEntity> GetAsync(TKey id);
    }
}
