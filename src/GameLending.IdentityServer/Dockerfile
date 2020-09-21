#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/GameLending.IdentityServer/GameLending.IdentityServer.csproj", "src/GameLending.IdentityServer/"]
COPY ["src/GameLending.EntityFrameworkCore.DbMigrations/GameLending.EntityFrameworkCore.DbMigrations.csproj", "src/GameLending.EntityFrameworkCore.DbMigrations/"]
COPY ["src/GameLending.EntityFrameworkCore/GameLending.EntityFrameworkCore.csproj", "src/GameLending.EntityFrameworkCore/"]
COPY ["src/GameLending.Domain/GameLending.Domain.csproj", "src/GameLending.Domain/"]
COPY ["src/GameLending.Domain.Shared/GameLending.Domain.Shared.csproj", "src/GameLending.Domain.Shared/"]
RUN dotnet restore "src/GameLending.IdentityServer/GameLending.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/GameLending.IdentityServer"
RUN dotnet build "GameLending.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameLending.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameLending.IdentityServer.dll"]