using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Infrastructure.SqlBuilders;

internal class Parameter
{
    public string Name { get; set; }
    public object Value { get; set; }

    public Parameter(string name, object value)
    {
        Name = name;
        Value = value;
    }
}
