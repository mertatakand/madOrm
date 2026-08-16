using madOrm.Library.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace madOrm.Library.Infrastructure.Configuration;

public interface IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    void Build(EntityTypeBuilder<TEntity> configuration);
}

public class EntityTypeBuilder<TEntity> where TEntity : BaseEntity
{
    public void ToColumn<TProperty>(Expression<Func<TEntity, TProperty>> expression,
        string columnName)
    {
        var memberExp = (MemberExpression)expression.Body;
        var prop = memberExp.Member as PropertyInfo;

        var type = typeof(TEntity);

        EntityTypeBuilderMapping.Instance
                                    .Create(type)
                                    .AddToType(type, prop, columnName);
    }
}
