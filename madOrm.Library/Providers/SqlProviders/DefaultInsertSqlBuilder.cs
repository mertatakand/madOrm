using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.ColumnNameProviders;
using madOrm.Library.Interfaces.SqlProviders;
using madOrm.Library.Interfaces.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace madOrm.Library.Providers.SqlProviders;

internal class DefaultInsertSqlBuilder : IInsertSqlBuilder
{
    public string Build<TEntity>(TEntity entity, ITableNameProvider tableNameProvider, IColumnNameProvider columnNameProvider) where TEntity : BaseEntity
    {
        // INSERT INTO [TableName] ([COLUMNS]) VALUES ([VALUES])

        // INSERT INTO [User] (Id, UserName) VALUES (1, 'Atakan')
        var tableName = tableNameProvider.GetTableName<TEntity>();
        var columns = columnNameProvider.GetColumnNames<TEntity>();
        string columnString = string.Join(',', columns);


        List<string> values = new List<string>();

        foreach (var property in typeof(TEntity).GetProperties())
        {
            object val = null;

            if (property.PropertyType.IsAssignableFrom(typeof(ISignedNumber<>)))
                val = property.GetValue(entity, null);
            else
                val = $"'{property.GetValue(entity, null)}'";

            values.Add(val.ToString());
        }

        string valuesString = string.Join(',', values);

        var generatedSql = $"INSERT INTO {tableName} ({columnString}) VALUES({valuesString})";

        return generatedSql;
    }
}