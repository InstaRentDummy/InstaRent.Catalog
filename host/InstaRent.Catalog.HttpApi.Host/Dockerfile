#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["host/InstaRent.Catalog.HttpApi.Host/InstaRent.Catalog.HttpApi.Host.csproj", "host/InstaRent.Catalog.HttpApi.Host/"]
COPY ["src/InstaRent.Catalog.HttpApi/InstaRent.Catalog.HttpApi.csproj", "src/InstaRent.Catalog.HttpApi/"]
COPY ["src/InstaRent.Catalog.Application.Contracts/InstaRent.Catalog.Application.Contracts.csproj", "src/InstaRent.Catalog.Application.Contracts/"]
COPY ["src/InstaRent.Catalog.Domain.Shared/InstaRent.Catalog.Domain.Shared.csproj", "src/InstaRent.Catalog.Domain.Shared/"]
COPY ["src/InstaRent.Catalog.Application/InstaRent.Catalog.Application.csproj", "src/InstaRent.Catalog.Application/"]
COPY ["src/InstaRent.Catalog.Domain/InstaRent.Catalog.Domain.csproj", "src/InstaRent.Catalog.Domain/"]
COPY ["src/InstaRent.Catalog.MongoDB/InstaRent.Catalog.MongoDB.csproj", "src/InstaRent.Catalog.MongoDB/"]
COPY ["host/InstaRent.Catalog.Host.Shared/InstaRent.Catalog.Host.Shared.csproj", "host/InstaRent.Catalog.Host.Shared/"]
RUN dotnet restore "host/InstaRent.Catalog.HttpApi.Host/InstaRent.Catalog.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/host/InstaRent.Catalog.HttpApi.Host"
RUN dotnet build "InstaRent.Catalog.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaRent.Catalog.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaRent.Catalog.HttpApi.Host.dll"]