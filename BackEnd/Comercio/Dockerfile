#Comience con la imagen base del SDK de .NET 7
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

#Set the working directory inside the container
WORKDIR /app

#Copiar los archivos del proyecto y restaurar las dependencias.
COPY *.sln ./
COPY ./Comercio.Api/*.csproj ./Comercio.Api/
COPY ./Comercio.Aplicacion/*.csproj ./Comercio.Aplicacion/
COPY ./Comercio.Infraestructura/*.csproj ./Comercio.Infraestructura/
COPY ./Comercio.Dominio/*.csproj ./Comercio.Dominio/
RUN dotnet restore

#Copie los archivos restantes y cree la aplicación.
COPY . ./
RUN dotnet publish -c Release -o out

#Construir la imagen en tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime-env
WORKDIR /app
COPY --from=build-env /app/out .
#
#set entry point 
ENTRYPOINT ["dotnet", "Comercio.Api.dll"]

