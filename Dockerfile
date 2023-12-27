FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["skit.API/skit.API.csproj", "skit.API/"]
COPY ["skit.Application/skit.Application.csproj", "skit.Application/"]
COPY ["skit.Core/skit.Core.csproj", "skit.Core/"]
COPY ["skit.Shared.Abstractions/skit.Shared.Abstractions.csproj", "skit.Shared.Abstractions/"]
COPY ["skit.Shared/skit.Shared.csproj", "skit.Shared/"]
COPY ["skit.Infrastructure/skit.Infrastructure.csproj", "skit.Infrastructure/"]
RUN dotnet restore "skit.API/skit.API.csproj"
COPY . .
WORKDIR "/src/skit.API"
RUN dotnet build "skit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "skit.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "skit.API.dll"]
