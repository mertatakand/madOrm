using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using System.Text;

namespace madOrm.Library.Interfaces.DatabaseProviders;

internal class SqlServerDatabaseProvider : BaseDatabaseProvider
{
    public override async Task<IEnumerable<TEntity>> ReadList<TEntity>()
    {

        using var conn = new SqlConnection(ConnectionString);

        var sql = ToSelect<TEntity>();

        DbDataReader reader = await GetReader(conn, sql);

        SetObject<TEntity>(reader, out var result);

        return result;
    }
}