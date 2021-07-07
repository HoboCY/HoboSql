using HoboSql.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HoboSql
{
    public static class DbHelperExtensions
    {
        public static Task<TEntity> GetAsync<TEntity>(this IDbConnection conn, string commandText, object param = null, int? commandTimeout = null) where TEntity : new()
        {
            if(conn.State == ConnectionState.Closed) conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            if(commandTimeout.HasValue)
                cmd.CommandTimeout = commandTimeout.Value;
            if(param != null)
            {
                foreach(var prop in param.GetType().GetProperties())
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = prop.Name;
                    parameter.Value = prop.GetValue(param);
                    cmd.Parameters.Add(parameter);
                }
            }
            var reader = cmd.ExecuteReader();
            var tObject = new TEntity();
            var tType = typeof(TEntity);
            while(reader.Read())
            {
                foreach(var prop in tType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if(reader[prop.Name] is DBNull) return null;
                    prop.SetValue(tObject, reader[prop.Name]);
                }
            }
            return Task.FromResult((TEntity)tObject);
        }

        public static Task<IQueryable<TEntity>> GetListAsync<TEntity>(this IDbConnection conn, string commandText, object param = null, int? commandTimeout = null) where TEntity : new()
        {
            if(conn.State == ConnectionState.Closed) conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            if(commandTimeout.HasValue)
                cmd.CommandTimeout = commandTimeout.Value;
            if(param != null)
            {
                foreach(var prop in param.GetType().GetProperties())
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = prop.Name;
                    parameter.Value = prop.GetValue(param);
                    cmd.Parameters.Add(parameter);
                }
            }
            var reader = cmd.ExecuteReader();
            var tType = typeof(TEntity);
            var entities = new List<TEntity>();

            while(reader.Read())
            {
                var entity = new TEntity();
                foreach(var prop in tType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    prop.SetValue(entity, reader[prop.Name]);
                }
                entities.Add(entity);
            }
            return Task.FromResult(entities.AsQueryable());
        }
    }
}
