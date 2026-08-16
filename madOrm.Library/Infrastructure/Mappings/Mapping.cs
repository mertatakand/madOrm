using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.ColumnNameProviders;
using madOrm.Library.Interfaces.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Infrastructure.Mappings;

internal class Mapping
{
    private static Mapping _instance;
    public static Mapping Instance => _instance ??= new Mapping();

    private Mapping()
    {
        maps = new();
    }

    private Dictionary<string, ObjectMap> maps;

    public ObjectMap Get<TEntity>() where TEntity : BaseEntity
    {
        return Get(typeof(TEntity));
    }
    internal ObjectMap Get(Type type)
    {
        maps.TryGetValue(type.Name, out var map);

        return map;
    }
    internal IEnumerable<string> GetColumnNames<TEntity>() where TEntity : BaseEntity
    {
        maps.TryGetValue(typeof(TEntity).Name, out var map);

        return map?.ColumnNames;
    }
    internal void SetMapping(ObjectMap objectMap)
    {
        var entityTypeName = objectMap.Entity.Name;

        if (maps.ContainsKey(entityTypeName))
            maps[entityTypeName] = objectMap;
        else 
            maps.Add(entityTypeName, objectMap);
    }
    internal ObjectMap SetMapping<TEntity>
        (ITableNameProvider tableNameProvider,
        IColumnNameProvider columnNameProvider) where TEntity : BaseEntity
    {
        var map = new ObjectMap(typeof(TEntity))
        {
            TableName = tableNameProvider.GetTableName<TEntity>(),
            ColumnNamePropertyMap = columnNameProvider.CreateColumnMap(typeof(TEntity))
        };
        SetMapping(map);

        return map;
    }

    internal ObjectMap SetMapping
        (Type type,
        ITableNameProvider tableNameProvider,
        IColumnNameProvider columnNameProvider)
    {
        var map = new ObjectMap(type)
        {
            TableName = tableNameProvider.GetTableName(type),
            ColumnNamePropertyMap = columnNameProvider.CreateColumnMap(type)
        };
        SetMapping(map);

        return map;
    }
}