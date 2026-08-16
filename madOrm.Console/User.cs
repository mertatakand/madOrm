using madOrm.Library.Infrastructure.Attributes;
using madOrm.Library.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Console;

[TableName("USER", "dbo")]
internal class User : BaseEntity
{
    [ColumnName("Id")]
    public int Id { get; set; }

    [ColumnName("UserName")]
    public string UserName { get; set; }
}