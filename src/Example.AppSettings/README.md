# Setting Environment Variables

src/Example.AppSettings >

1. Set SQL Connection String.
    ```shell
    dotnet user-secrets set "SqlServerOptions:ConnectionString" CONNECTIONSTRINGS_DEFAULTCONNECTION
    ```