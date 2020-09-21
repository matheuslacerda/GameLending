#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/GameLending.HttpApi.Host/GameLending.HttpApi.Host.csproj", "src/GameLending.HttpApi.Host/"]
COPY ["src/GameLending.EntityFrameworkCore.DbMigrations/GameLending.EntityFrameworkCore.DbMigrations.csproj", "src/GameLending.EntityFrameworkCore.DbMigrations/"]
COPY ["src/GameLending.EntityFrameworkCore/GameLending.EntityFrameworkCore.csproj", "src/GameLending.EntityFrameworkCore/"]
COPY ["src/GameLending.Domain/GameLending.Domain.csproj", "src/GameLending.Domain/"]
COPY ["src/GameLending.Domain.Shared/GameLending.Domain.Shared.csproj", "src/GameLending.Domain.Shared/"]
COPY ["src/GameLending.Application/GameLending.Application.csproj", "src/GameLending.Application/"]
COPY ["src/GameLending.Application.Contracts/GameLending.Application.Contracts.csproj", "src/GameLending.Application.Contracts/"]
COPY ["src/GameLending.HttpApi/GameLending.HttpApi.csproj", "src/GameLending.HttpApi/"]
RUN dotnet restore "src/GameLending.HttpApi.Host/GameLending.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/GameLending.HttpApi.Host"
RUN dotnet build "GameLending.HttpApi.Host.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "src/GameLending.DbMigrator/GameLending.DbMigrator.csproj"
COPY . .
WORKDIR "/src/src/GameLending.DbMigrator"
RUN dotnet build "GameLending.DbMigrator.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "GameLending.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameLending.HttpApi.Host.dll"]