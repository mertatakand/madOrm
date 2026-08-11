using madOrm.Library.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.Interfaces.TableNameProviders;

public interface ITableNameProvider
{
    string GetTableName<TEntity>() where TEntity : BaseEntity;
    string GetTableName(BaseEntity entity);
    string GetTableName(Type type);
}