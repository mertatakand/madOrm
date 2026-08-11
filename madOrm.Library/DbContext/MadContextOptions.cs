using madOrm.Library.Infrastructure.Base;
using madOrm.Library.Interfaces.DatabaseProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace madOrm.Library.DbContext;

public class MadContextOptions
{
    private List<Type> entities = new();
    public IDatabaseProvider DatabaseProvider { get; set; }

    public void AddEntity(Type type)
    {
        entities.Add(type);
    }
}

public class MadContextOptionsBuilder : IDisposable
{
    private List<Type> entities = new();
    private IDatabaseProvider databaseProvider;

    public MadContextOptionsBuilder AddEntity<TEntity>() where TEntity : BaseEntity
    {
        entities.Add(typeof(TEntity));
        return this;
    }

    public MadContextOptionsBuilder SetDatabaseProvider(IDatabaseProvider provider)
    {
        databaseProvider = provider;
        return this;
    }

    public MadContextOptions Build()
    {
        var result = new MadContextOptions();

        foreach (var entity in entities) 
        { 
            result.AddEntity(entity.GetType());
        }

        result.DatabaseProvider = databaseProvider;
        return result;
    }

    public void Dispose()
    {
        
        entities.Clear();
        entities = null;

        GC.SuppressFinalize(this);
    }
}