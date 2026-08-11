using madOrm.Library.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.DbContext;

public class MadContext
{
    private readonly MadContextOptionsBuilder optionsBuilder;

    private MadContextOptions _options;

    private MadContextOptions options => _options ??= optionsBuilder.Build();

    public MadContext(MadContextOptionsBuilder builder)
    {
        this.optionsBuilder = builder;
    }

    public MadContext(Action<MadContextOptionsBuilder> builderAction)
    {
        optionsBuilder = new();
        builderAction(optionsBuilder);
    }

    public Task<IEnumerable<TEntity>> ReadList<TEntity>() where TEntity : BaseEntity
    {
        var entities = options.DatabaseProvider.ReadList<TEntity>();

        return entities;
    }
}
