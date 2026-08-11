using madOrm.Library.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace madOrm.Library.Interfaces.ColumnNameProviders;

public interface IColumnNameProvider
{
    IEnumerable<string> GetColumnNames<TEntity>() where TEntity : BaseEntity;
    IEnumerable<string> GetColumnNames(Type type);

    Dictionary<PropertyInfo, string> CreateColumnMap(Type type);
}