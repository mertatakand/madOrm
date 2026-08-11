using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.ColumnNameProviders;
using madOrm.Library.Interfaces.SqlProviders;
using madOrm.Library.Interfaces.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Interfaces.DatabaseProviders;

public interface IDatabaseProvider
{
    string ConnectionString { get; set; }
    ITableNameProvider TableNameProvider { get; set; }
    IColumnNameProvider ColumnNameProvider { get; set; }
    ISelectBuilder SelectBuilder { get; set; }

    string ToSelect<TEntity>() where TEntity : BaseEntity;

    Task<IEnumerable<TEntity>> ReadList<TEntity>() where TEntity : BaseEntity;
}
