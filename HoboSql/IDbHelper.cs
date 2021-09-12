using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoboSql
{
    public interface IDbHelper
    {
        Task<int> ExecuteAsync(string sql, object parameter = null);

        Task<int> ExecuteAsync(Dictionary<string, object> commands);

        Task<TEntity> GetAsync<TEntity>(string sql, object parameter = null) where TEntity : class, new();

        Task<object> GetScalarAsync(string sql, object parameter = null);

        Task<object> GetScalarAsync<T>(string sql, List<T> parameters);

        Task<IEnumerable<TEntity>> GetListAsync<TEntity>(string sql, object parameter = null) where TEntity : class, new();
    }
}
