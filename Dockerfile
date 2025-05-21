# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY ["TaskManagerApi.csproj", "./"]
RUN dotnet restore "./TaskManagerApi.csproj"

# Copy everything and build
COPY . .
RUN dotnet publish "./TaskManagerApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "TaskManagerApi.dll"]
