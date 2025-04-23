using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quantum.Configurator;
using Quantum.Query.Pipeline;
using Quantum.Query.Semaphore;

namespace Quantum.Query.Configurator;

public static class ConfigCQRSBuilderExtenssions
{
    public static ConfigCQRSBuilder ConfigCQRS(this QuantumServiceCollection collection)
    {
        return new ConfigCQRSBuilder(collection);
    }
}

public class ConfigCQRSBuilder(QuantumServiceCollection collection)
{
    public ConfigCQRSBuilder RegisterQueryHandlersInAssemblyAsTransient(Assembly assembly)
    {

        var queryHandlers =
            assembly.GetTypes()
                .Where(t =>
                    t.GetInterfaces().Any(a => a.Name == typeof(IWantToHandleThisQuery<,>).Name));

        foreach (var queryHandler in queryHandlers)
        {
            collection.Collection.AddTransient(queryHandler.GetInterfaces()[0], queryHandler);
        }

        return this;
    }

    public ConfigCQRSBuilder RegisterQueryHandlersInAssembliesAsTransient(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            RegisterQueryHandlersInAssemblyAsTransient(assembly);
        }

        return this;
    }


    public ConfigCQRSBuilder RegisterQueryHandlerPipelineStage<T>()
        where T : IAmQueryHandlerStage
    {
        collection.Collection.AddTransient<T>();
        return this;
    }


    public ConfigCQRSBuilder RegisterQueryHandlerPipelineStageAs<T>(ServiceLifetime serviceLifetime)
        where T : IAmQueryHandlerStage
    {
        collection.Collection.Add(new ServiceDescriptor(typeof(T), typeof(T), serviceLifetime));
        return this;
    }

    public ConfigCQRSBuilder RegisterQueryHandlerPipelineStageAs<T>(Func<IServiceProvider, object> factory, ServiceLifetime serviceLifetime)
        where T : IAmQueryHandlerStage
    {
        collection.Collection.Add(new ServiceDescriptor(typeof(T), factory, serviceLifetime));
        return this;
    }

    public ConfigCQRSBuilder RegisterQueryDispatcherAsTransient<T>()
        where T : class, IQueryDispatcher
    {
        collection.Collection.AddTransient<IQueryDispatcher, T>();
        return this;
    }

    public ConfigCQRSBuilder RegisterQueryDispatcherAsTransient<T>(Func<IServiceProvider, IQueryDispatcher> factory)
        where T : class, IQueryDispatcher
    {
        collection.Collection.AddTransient<IQueryDispatcher>(factory);
        return this;
    }

    public ConfigCQRSBuilder RegisterSemaphoreAsScoped<T>()
        where T : class, ISemaphore
    {
        collection.Collection.AddScoped<ISemaphore, T>();
        return this;
    }

    public QuantumServiceCollection and()
    {
        return collection;
    }

}