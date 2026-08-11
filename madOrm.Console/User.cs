using madOrm.Library.Infrastructure.Attributes;
using madOrm.Library.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Console;

[TableName("USER", "dbo")]
internal class User : BaseEntity
{
    [ColumnName("id")]
    public int Id { get; set; }

    [ColumnName("username")]
    public string UserName { get; set; }
}