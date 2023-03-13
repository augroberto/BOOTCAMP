# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY gamecatalog/ .

RUN dotnet restore

# copy and publish app and libraries
RUN dotnet publish -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.8-bullseye-slim-amd64
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "gamecatalog.dll"]