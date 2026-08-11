using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.ColumnNameProviders;
using madOrm.Library.Interfaces.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Interfaces.SqlProviders; 
public interface ISelectBuilder
{
    string Build<TEntity>(
        ITableNameProvider tableNameProvider,
        IColumnNameProvider columnNameProvider) where TEntity : BaseEntity;
}