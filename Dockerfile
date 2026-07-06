# 1. IMAGEN BASE
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 2. COMPILACION
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Alumnos.API.csproj", "."]
RUN dotnet restore "./Alumnos.API.csproj"

COPY . . 
RUN dotnet build "Alumnos.API.csproj" -c Release -o /app/build

# 3. PUBLICACION
FROM build AS publish 
RUN dotnet publish "Alumnos.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. ARRANQUE
FROM base AS final 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alumnos.API.dll"]