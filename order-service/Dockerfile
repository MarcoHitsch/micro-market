#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["order-service/order-service.csproj", "order-service/"]
COPY ["shared/shared.csproj", "shared/"]
COPY ["catalog-service-api/catalog-service-api.csproj", "catalog-service-api/"]
RUN dotnet restore "order-service/order-service.csproj"
COPY . .
WORKDIR "/src/order-service"
RUN dotnet build "order-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "order-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "order-service.dll"]