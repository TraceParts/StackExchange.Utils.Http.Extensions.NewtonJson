# StackExchange.Utils.Http.Extensions.NewtonJson

Currently StackExchange.Utils.Http has dependency on Jil for Json serialization and deserialization.

This package allows to use [NewtonSoft Json.NET](https://www.newtonsoft.com/json) instead of [JIL](https://github.com/kevin-montrose/Jil)

[![StackExchange.Utils.Http.Extensions.NewtonJson](https://img.shields.io/badge/nuget-v0.0.2-green)](https://www.nuget.org/packages/StackExchange.Utils.Http.Extensions.NewtonJson/0.0.2)

### How to use it

```c#
var result = await Http.Request("https://example.com")  
                       .ExpectNewtonJson<MyType>(MyJsonSerializerSettings)
                       .GetAsync()
```

*If serializerSettings is null, JsonSerializer will use default settings from DefaultSettings.*

Of course, you can use all other features from StackExchange, like this:
```c#
var result = await Http.Request("https://example.com")
                       .IgnoredResponseStatuses(HttpStatusCode.NotFound)
                       .WithTimeout(TimeSpan.FromSeconds(20))
                       .ExpectNewtonJson<MyType>()
                       .GetAsync();
```

###### Todo

Implement SendNewtonJson