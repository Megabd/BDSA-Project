# BDSA-Project

``docker run -d -p 1433:1433 -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Str0ngPa$$w0rd' mcr.microsoft.com/mssql/server:2022-latest``

Set github access token: ``dotnet user-secrets set "GithubAPI" <TOKEN STRING> --project Project.MinimalAPI/TodoAPI``