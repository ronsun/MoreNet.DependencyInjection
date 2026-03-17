# MoreNet DependencyInjection

Some enhancements for dependency injection.

## Introduction

This library provides a simpler way to work with named services in dependency injection.

## Usage

### Nameable service injection

Basically, we can inject `IEnumerable<TService>` into a constructor and filter by type, but it is not simple enough.

This project provides a solution for that:
1. Inherit from `INameable`.
2. Call `AddNamedScoped<TService>()` in `Startup.cs`.
3. Finally, inject `INamedServiceContainer<TService>`.

Here's an example:
```csharp
// 1. Inherit from `INameable` and implement it.
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

## Documentation

See the [API documentation](https://ronsun.github.io/MoreNet.DependencyInjection/api) for the full API reference.
