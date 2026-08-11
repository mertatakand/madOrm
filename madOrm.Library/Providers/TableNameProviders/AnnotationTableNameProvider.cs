using madOrm.Library.Infrastructure.Attributes;
using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.TableNameProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace madOrm.Library.Providers.TableNameProviders;

internal class AnnotationTableNameProvider : ITableNameProvider
{
    public string GetTableName<TEntity>() where TEntity : BaseEntity
    {
        return GetTableName(typeof(TEntity));
    }

    public string GetTableName(BaseEntity entity)
    {
        return GetTableName(entity.GetType());
    }

    public string GetTableName(Type type)
    {
        var att = type.GetCustomAttribute<TableNameAttribute>();

        if (att is null)
            return type.Name;

        if (string.IsNullOrWhiteSpace(att.SchemaName))
            return att.TableName;

        return $"[{att.SchemaName}].[{att.TableName}]";
    }
}