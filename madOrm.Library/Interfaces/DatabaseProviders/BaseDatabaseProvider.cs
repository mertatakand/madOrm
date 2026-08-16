using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Infrastructure.Mapping;
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
        var mapping = Mapping.Instance.Get<TEntity>();

        if (mapping?.SelectSql is not null)
            return mapping.SelectSql;

        mapping ??= Mapping.Instance.SetMapping<TEntity>(TableNameProvider, ColumnNameProvider);
        mapping.SelectSql = SelectBuilder.Build<TEntity>(TableNameProvider, ColumnNameProvider);

        return mapping.SelectSql;
    }

    protected virtual async Task<DbDataReader> GetReader(DbConnection conn, string sql)
    {
        ArgumentNullException.ThrowIfNull(conn);

        if (conn.State != ConnectionState.Open)
            await conn.OpenAsync(); // requires open conn

        var command = conn.CreateCommand();

        command.CommandText = sql;

        await Console.Out.WriteLineAsync($"SQL: \n {sql}");

        var reader = await command.ExecuteReaderAsync();

        return reader;
    }

    protected void SetObject<TEntity>(DbDataReader reader, out IList<TEntity> entities) where TEntity : BaseEntity
    {
        var objectMap = Mapping.Instance.Get<TEntity>();

        if (!objectMap.ColumnNamePropertyMap.Any())
        {
            Mapping.Instance.SetMapping<TEntity>(TableNameProvider, ColumnNameProvider);
        }

        entities = new List<TEntity>();

        while (reader.Read())
        {
            var ent = Activator.CreateInstance<TEntity>();
            entities.Add(ent);

            for (int i = 0; i < objectMap.ColumnNamePropertyMap.Count; i++)
            {
                var columnName = reader.GetName(i);
                var data = reader.GetValue(i);
                var columnType = reader.GetFieldType(i);

                var prop = objectMap.GetProperty(columnName);

                prop.SetValue(ent, Convert.ChangeType(data, columnType));
            }            
        }
    }
}