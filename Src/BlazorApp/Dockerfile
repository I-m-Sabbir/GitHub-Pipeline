# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
WORKDIR /app/Src

RUN dotnet restore SimpleDemo.sln
RUN dotnet publish BlazorApp/BlazorApp.csproj -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "BlazorApp.dll"]
