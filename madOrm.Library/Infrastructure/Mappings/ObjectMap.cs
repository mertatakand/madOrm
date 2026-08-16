using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace madOrm.Library.Infrastructure.Mappings;

internal class ObjectMap
{
    public ObjectMap(Type entity)
    {
        Entity = entity;
    }

    public Type Entity { get; set; }
    public string SelectSql { get; set; }
    public string InsertSql { get; set; }
    public string TableName { get; set; }
    private IEnumerable<string> _columnNames;

    public IEnumerable<string> ColumnNames {
        get => _columnNames ??= ColumnNamePropertyMap?.Values;
        set => _columnNames = value;
    }

    public Dictionary<PropertyInfo, string> ColumnNamePropertyMap { get; set; } = new();

    public void Add(PropertyInfo property, string columnName)
    {
        var success = ColumnNamePropertyMap.TryAdd(property, columnName);

        if (success)
            throw new InvalidOperationException($"{property.Name} is already added!");
    }

    public PropertyInfo GetProperty(string columName)
    {
        return ColumnNamePropertyMap
            .Where(i => i.Value.Equals(columName, StringComparison.Ordinal))
            .Select(i => i.Key)
            .FirstOrDefault();
    }
}
