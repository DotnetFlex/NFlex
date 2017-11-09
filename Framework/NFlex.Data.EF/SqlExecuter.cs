using NFlex.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;

namespace NFlex.Data.EF
{
    /// <summary>
    /// 数据库操作执行类
    /// <para>（此类执行的所有功能都不受工作单元管理，将直接作用于数据库）</para>
    /// </summary>
    public class SqlExecuter: ISqlExecuter
    {
        protected IDbContext _dbContext { get; private set; }

        public SqlExecuter(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 执行SQL查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> ExecuteQuery<T>(string sql)
        {
            return _dbContext.Database.SqlQuery<T>(sql).ToList();
        }

        /// <summary>
        /// 执行非查询SQL命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteCommand(string sql)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 执行只返回一个值的SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql)
        {
            var obj = ExecuteQuery<T>(sql);
            if (obj.Any())
                return (T)obj.First();
            else
                return default(T);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public void BulkInsert<T>(IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(_dbContext.Database.Connection.ConnectionString))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = typeof(T).Name;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))
                    .Cast<PropertyDescriptor>()
                    .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                    .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IUpdateContext<T> Update<T>(Expression<Func<T, bool>> predicate) where T:class
        {
            var queryable = _dbContext.Set<T>().Where(predicate);
            return new UpdateContext<T>(queryable);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().Where(predicate).Delete();
        }
    }
}
