using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ColumnNameAttribute : Attribute
{
    public string ColumnName { get; set; }

    public ColumnNameAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}
