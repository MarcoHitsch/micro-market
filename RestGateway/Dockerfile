#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestGateway/RestGateway.csproj", "RestGateway/"]
COPY ["basket-service/basket_swagger.json", "basket-service/basket_swagger.json"]
COPY ["order-service/order_swagger.json", "order-service/order_swagger.json"]
COPY ["catalog-service/catalog_swagger.json", "catalog-service/catalog_swagger.json"]
COPY ["shared/shared.csproj", "shared/"]
RUN dotnet restore "RestGateway/RestGateway.csproj"
COPY . .
WORKDIR "/src/RestGateway"
RUN dotnet build "RestGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestGateway.dll"]