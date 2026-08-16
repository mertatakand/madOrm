using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Infrastructure.Configuration;
using madOrm.Library.Interfaces.ColumnNameProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace madOrm.Library.Providers.ColumnNameProviders;

public class EntityTypeBuilderProvider : IColumnNameProvider
{
    public EntityTypeBuilderProvider()
    {
    }

    public Dictionary<PropertyInfo, string> CreateColumnMap(Type type)
    {
        return EntityTypeBuilderMapping.Instance.Map[type];
    }

    public IEnumerable<string> GetColumnNames<TEntity>() where TEntity : BaseEntity
    {
        return GetColumnNames(typeof(TEntity));
    }

    public IEnumerable<string> GetColumnNames(Type type)
    {
        return EntityTypeBuilderMapping.Instance.Map[type].Values;
    }
}