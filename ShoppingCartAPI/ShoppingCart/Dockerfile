# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["ShoppingCartAPI/ShoppingCart/ShoppingCart.csproj", "ShoppingCart/"]
RUN dotnet restore "ShoppingCart/ShoppingCart.csproj"

# Copy all source files
COPY ShoppingCartAPI/ShoppingCart/ ShoppingCart/
WORKDIR /src/ShoppingCart

# Build and publish
RUN dotnet build "ShoppingCart.csproj" -c Release -o /app/build
RUN dotnet publish "ShoppingCart.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app

# Copy publish output from build stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ShoppingCart.dll"]
