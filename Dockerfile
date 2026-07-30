# 1. ETAPA DE EJECUCIÓN 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# 2. ETAPA DE COMPILACIÓN 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia del archivo del proyecto y restauración de paquetes
COPY ["Alumnos.API.csproj", "./"]
RUN dotnet restore "Alumnos.API.csproj"

# copia del resto del código
COPY . .

# Compilación y publicación 
RUN dotnet publish "Alumnos.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. ETAPA FINAL DE ARRANQUE
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Alumnos.API.dll"]