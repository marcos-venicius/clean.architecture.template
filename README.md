# Clean Architecture Template

This code is a template to start a new application using .NET CORE 7 with Mediator, CQRS, Clean Architecture, Unit Test and more.

This have a simple `Todo-App` implementation **just for demonstration** of how it works.

Create your projects based on this template.

**some codes is missing tests because it is only an example of usage**

![image](https://github.com/marcos-venicius/clean.architecture.template/assets/94018427/2035f12a-5ce3-4fd4-84d4-19576c174b26)

click on "Use this template" to start

## Create migrations

```shell
dotnet-ef migrations add <migration_name> -o Infra/Data/Migrations --startup-project Core/Core.csproj
```

## Run project

```shell
cd src/Core

dotnet run
```
