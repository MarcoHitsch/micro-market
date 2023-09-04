#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["catalog-service/catalog-service.csproj", "catalog-service/"]
COPY ["catalog-service-api/catalog-service-api.csproj", "catalog-service-api/"]
COPY ["shared/shared.csproj", "shared/"]
RUN dotnet restore "catalog-service/catalog-service.csproj"
COPY . .
WORKDIR "/src/catalog-service"
RUN dotnet build "catalog-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "catalog-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "catalog-service.dll"]