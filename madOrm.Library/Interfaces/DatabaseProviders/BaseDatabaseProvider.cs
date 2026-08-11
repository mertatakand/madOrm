using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.ColumnNameProviders;
using madOrm.Library.Interfaces.SqlProviders;
using madOrm.Library.Interfaces.TableNameProviders;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace madOrm.Library.Interfaces.DatabaseProviders;

internal abstract class BaseDatabaseProvider : IDatabaseProvider
{
    public string ConnectionString { get; set; }
    public ITableNameProvider TableNameProvider { get; set; }
    public IColumnNameProvider ColumnNameProvider { get; set; }
    public ISelectBuilder SelectBuilder { get; set; }

    public abstract Task<IEnumerable<TEntity>> ReadList<TEntity>() where TEntity : BaseEntity;

    public virtual string ToSelect<TEntity>() where TEntity : BaseEntity
    {
        // SELECT * FROM [USER]
        var sql = SelectBuilder.Build<TEntity>(TableNameProvider, ColumnNameProvider);
        return sql;
    }

    protected virtual async Task<DbDataReader> GetReader(DbConnection conn, string sql)
    {
        ArgumentNullException.ThrowIfNull(conn);

        if (conn.State != ConnectionState.Open)
            await conn.OpenAsync(); // requires open conn

        var command = conn.CreateCommand();

        command.CommandText = sql;

        await Console.Out.WriteLineAsync($"SQL: \n {sql}");

        return await command.ExecuteReaderAsync();
    }

    protected void SetObject<TEntity>(DbDataReader reader, out IList<TEntity> entities) where TEntity : BaseEntity
    {
        entities = new List<TEntity>();

        while (reader.Read())
        {
            var ent = Activator.CreateInstance<TEntity>();
            entities.Add(ent);

            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var data = reader.GetValue(i);
                var columnType = reader.GetFieldType(i);

                var prop = ent.GetType().GetProperty(columnName);

                prop.SetValue(ent, Convert.ChangeType(data, columnType));
            }

            reader.NextResult();
        }
    }
}