#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Comercio.Api.csproj", "Comercio.Api/"]
COPY ["../Comercio.Aplicacion/Comercio.Aplicacion.csproj", "Comercio.Aplicacion/"]
COPY ["../Comercio.Dominio/Comercio.Dominio.csproj", "Comercio.Dominio/"]
COPY ["../Comercio.Infraestructura/Comercio.Infraestructura.csproj", "Comercio.Infraestructura/"]
RUN dotnet restore "./Comercio.Api/./Comercio.Api.csproj"
COPY . .
WORKDIR "/src/Comercio.Api"
RUN dotnet build "./Comercio.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Comercio.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comercio.Api.dll"]