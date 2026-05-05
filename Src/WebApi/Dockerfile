# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY . ./
WORKDIR /app/Src

RUN dotnet restore SimpleDemo.sln
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]