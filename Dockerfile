# Utiliza la imagen oficial de .NET 6.0 como base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Utiliza la imagen oficial de .NET 6.0 como imagen de compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitMQ.Consumer.csproj", "."]
RUN dotnet restore "./RabbitMQ.Consumer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RabbitMQ.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMQ.Consumer.csproj" -c Release -o /app/publish

# Utiliza la imagen base para la ejecución
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQ.Consumer.dll"]
