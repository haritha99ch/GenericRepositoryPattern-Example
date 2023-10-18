# Entity Framework: Advance Generic Repository Pattern .NET (Specification, Selector)

## Introduction

This is an example of implementing an Enhanced Generic Repository pattern in .NET. The project includes implementation of Entity Framework with generic repository including specification pattern an selector pattern.

## Selector pattern

Selector pattern is a novel approach for Type-Safe Data Selection in generic repository. [Read more](https://gist.github.com/haritha99ch/f7f3e90562395c5f95f3898e0570f041)

## Setup

### Prerequisite

1. .NET 8 installed on your computer. You can download .NET 8 from the official website (<https://dotnet.microsoft.com/download/dotnet/8.0>).
2. An integrated development environment (IDE) to write your code
3. Git installed on your computer.

### Initial setup

1. Clone the project.

   ```shell
   git clone https://github.com/haritha99ch/GenericRepositoryPattern-Example.git
   ```

2. [Set Environment variables](./src/Example.AppSettings/README.md#setting-environment-variables).

3. Install all the dependencies.

   ```shell
   dotnet restore
   ```

4. Build the project.

   ```shell
   dotnet build
   ```

5. Run the project.

   ```shell
   dotnet run --project ./src/Example.Presentation/
   ```
