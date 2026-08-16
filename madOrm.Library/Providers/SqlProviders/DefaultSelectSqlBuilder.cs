using madOrm.Library.Interfaces.ColumnNameProviders;
using madOrm.Library.Interfaces.SqlProviders;
using madOrm.Library.Interfaces.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Providers.SqlProviders;

public class DefaultSelectSqlBuilder : ISelectBuilder
{
    string ISelectBuilder.Build<TEntity>(ITableNameProvider tableNameProvider, IColumnNameProvider columnNameProvider)
    {
        // SELECT * FROM USER
        var tableName = tableNameProvider.GetTableName<TEntity>();
        var columns = columnNameProvider.GetColumnNames<TEntity>();
        string columnString = "*";

        if (columns is not null && columns.Any())
            columnString = string.Join(", ", columns.Select(i => $"[{i}]"));

        var sql = $"SELECT {columnString} FROM {tableName}";

        return sql;
    }
}