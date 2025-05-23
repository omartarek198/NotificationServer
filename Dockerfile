# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["NotificationServer.csproj", "./"]
RUN dotnet restore "./NotificationServer.csproj"

# Copy everything else and publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
# Expose ports for HTTP + WebSockets
EXPOSE 80
EXPOSE 443

# Copy published files from build
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "NotificationServer.dll"]
