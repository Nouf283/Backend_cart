# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["ShoppingCart.csproj", "./"]
RUN dotnet restore "ShoppingCart.csproj"

# Copy all source files
COPY . .
RUN dotnet build "ShoppingCart.csproj" -c Release -o /app/build
RUN dotnet publish "ShoppingCart.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app

# Copy publish output from build stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ShoppingCart.dll"]
