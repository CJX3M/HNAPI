# Establece la imagen base para la fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia los archivos de proyecto y restaura las dependencias
COPY *.sln .
COPY HackerNewsAPI/*.csproj ./HackerNewsAPI/
COPY HackerNewsAPITests/*.csproj ./HackerNewsAPITests/
RUN dotnet restore

# Copia el resto de los archivos y construye la aplicación
COPY . .
WORKDIR /app/HackerNewsAPI
RUN dotnet publish -c Release -o out

# Establece la imagen base para la fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/HackerNewsAPI/out .

# Expone el puerto en el que la aplicación escuchará
EXPOSE 80

# Define el punto de entrada para la aplicación
ENTRYPOINT ["dotnet", "HackerNewsAPI.dll"]