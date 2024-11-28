# Use the official .NET 6 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY ["backend/WorkHive.Api/WorkHive.Api.csproj", "backend/WorkHive.Api/"]
COPY ["backend/WorkHive.Domain/WorkHive.Domain.csproj", "backend/WorkHive.Domain/"]
COPY ["backend/WorkHive.Application/WorkHive.Application.csproj", "backend/WorkHive.Application/"]
COPY ["backend/WorkHive.Infrastructure/WorkHive.Infrastructure.csproj", "backend/WorkHive.Infrastructure/"]

RUN dotnet restore "backend/WorkHive.Api/WorkHive.Api.csproj"

# Copy the rest of the solution files
COPY . .

# Build the application
RUN dotnet publish "backend/WorkHive.Api/WorkHive.Api.csproj" -c Release -o /app/out

# Use the official .NET 6 runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out .

# Expose the port on which the app will run
EXPOSE 8080
EXPOSE 443

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "WorkHive.Api.dll"]