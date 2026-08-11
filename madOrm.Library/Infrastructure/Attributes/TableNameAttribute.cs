using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TableNameAttribute : Attribute
{
    public string TableName { get; private set; }

    public string? SchemaName { get; private set; }
    
    public TableNameAttribute(string tableName)
    {
        TableName = tableName;
    }

    public TableNameAttribute(string tableName, string schemaName) : this(tableName)
    {
        SchemaName = schemaName;
    }
}