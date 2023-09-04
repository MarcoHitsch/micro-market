#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["basket-service/basket-service.csproj", "basket-service/"]
COPY ["basket-service-api/basket-service-api.csproj", "basket-service-api/"]
COPY ["catalog-service-api/catalog-service-api.csproj", "catalog-service-api/"]
RUN dotnet restore "basket-service/basket-service.csproj"
COPY . .
WORKDIR "/src/basket-service"
RUN dotnet build "basket-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "basket-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "basket-service.dll"]