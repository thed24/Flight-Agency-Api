FROM mcr.microsoft.com/dotnet/sdk:7.0.100-preview.3-bullseye-slim-amd64 as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish ./Flight-Agency.sln -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:7.0.0-preview.3-alpine3.15-amd64 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENV ASPNETCORE_ENVIRONMENT Production

ENTRYPOINT [ "dotnet", "/app/Flight-Agency.WebApi.dll" ]