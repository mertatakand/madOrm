using madOrm.Library.DbContext;
using madOrm.Library.Providers.ColumnNameProviders;
using madOrm.Library.Providers.SqlProviders;
using madOrm.Library.Providers.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Interfaces.DatabaseProviders;

public static class MadContextExtensions
{
    extension(MadContextOptionsBuilder builder)
    {
        public void UseSqlServer(string connectionString)
        {
            var provider = new SqlServerDatabaseProvider()
            {
                ConnectionString = connectionString,
                TableNameProvider = new AnnotationTableNameProvider(),
                ColumnNameProvider = new EntityTypeBuilderProvider(),
                SelectBuilder = new DefaultSelectSqlBuilder(),
                InsertSqlBuilder = new DefaultInsertSqlBuilder(),
            };

            builder.SetDatabaseProvider(provider);
        }
    }
}