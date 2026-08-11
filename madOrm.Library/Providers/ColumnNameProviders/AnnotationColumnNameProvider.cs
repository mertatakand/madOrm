using madOrm.Library.Infrastructure.Attributes;
using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.ColumnNameProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace madOrm.Library.Providers.ColumnNameProviders;

internal class AnnotationColumnNameProvider : IColumnNameProvider
{
    public Dictionary<PropertyInfo, string> CreateColumnMap(Type type)
    {
        var map = new Dictionary<PropertyInfo, string>();

        var properties = type.GetProperties();

        foreach (var property in properties) 
        {
            var att = property.GetCustomAttribute<ColumnNameAttribute>();
            var columnName = att?.ColumnName ?? property.Name;

            map.Add(property, columnName);
        }
        return map;
    }

    public IEnumerable<string> GetColumnNames<TEntity>() where TEntity : BaseEntity
    {
        return  GetColumnNames(typeof(TEntity));
    }

    public IEnumerable<string> GetColumnNames(Type type)
    {
        var properties = type.GetProperties();
        var colums = new List<string>();

        foreach ( var property in properties)
        {
            var att = property.GetCustomAttribute<ColumnNameAttribute>();
            var columnName = att?.ColumnName ?? property.Name;

            colums.Add(columnName);
        }

        return colums;
    }
}
