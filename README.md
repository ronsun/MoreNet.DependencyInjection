## What's this
Some enhancement for dependency injection. 

### Nameable service injection
Basically, we can inject `IEnumerable<TService>` to constructor and filter them by type, but it is not simple enough.  

This project provide a solution to resolve it.
1. Inherit from `INamebale`.
2. Call `AddNamedScoped<TService>()` in `Startup.cs`
3. Fianlly just inject `INamedServiceDictionary<TService>`.

Here's an example:
``` csharp
// 1. Inherit from `INamebale` and implementation.
public interface IFoo : INameable
{
}

internal class Foo1 : IFoo
{
    public string Name => "N1";
}

internal class Foo2 : IFoo
{
    public string Name => "N2";
}

// 2. Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    // Configure as usual
    services.AddScoped<IFoo, Foo1>();
    services.AddScoped<IFoo, Foo2>();

    // Just one more thing
    services.AddNamedScoped<IFoo>();
}

// 3. Inject and use simply
public DemoController(INamedServiceContainer<IFoo> foos)
{
    var foo1 = foos.GetService("N1");
}
```