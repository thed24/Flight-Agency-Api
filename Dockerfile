FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS installer-env

COPY . /src/dotnet-function-app
RUN cd /src/dotnet-function-app/ && \
    mkdir -p /home/site/wwwroot && \
    dotnet publish *.sln --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true \
    ASPNETCORE_URLS=http://+:8080

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
