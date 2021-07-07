using HoboSql.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HoboSql
{
    public class SqlHelper<TEntity, TKey> : IDbHelper<TEntity, TKey> where TEntity : new() where TKey : struct
    {
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "ConnectionString is invalid");
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            using IDbConnection conn = new SqlConnection(ConnectionString);
            var sql = "SELECT * FROM Student WHERE id = @id";
            return await conn.GetAsync<TEntity>(sql, new { id });
        }

        public async Task<TEntity> SingleOrDefaultAsync(TKey id)
        {
            using IDbConnection conn = new SqlConnection(ConnectionString);
            var sql = "SELECT * FROM Student WHERE Id = @id";
            return await conn.GetAsync<TEntity>(sql, new { id });
        }

        //public async Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> predicate = null) where T : new()
        //{
        //    using IDbConnection conn = new SqlConnection(ConnectionString);

        //    var sql = $@"SELECT * FROM [{typeof(T).Name}]";
        //    IDbCommand command = new SqlCommand(sql, (SqlConnection)conn);
        //    var reader = await ((SqlCommand)command).ExecuteReaderAsync();
        //    var properties = typeof(T).GetProperties();
        //    var results = new List<T>();
        //    while(!await reader.ReadAsync())
        //    {
        //        var t = new T();
        //        foreach(var property in properties)
        //        {
        //            property.SetValue(t, reader[property.Name]);
        //        }
        //        results.Add(t);
        //    }
        //    return results;
        //}

        public async Task<TEntity> GetAsync(TKey id)
        {
            using IDbConnection conn = new SqlConnection(ConnectionString);
            var sql = "SELECT * FROM Student WHERE id = @id";
            var entity = await conn.GetAsync<TEntity>(sql, new { id });
            if (entity == null) throw new EntityNotFoundException(typeof(TEntity), id);
            return entity;
        }
    }
}
