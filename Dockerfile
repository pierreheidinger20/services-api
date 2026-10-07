FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Services.slnx ./
COPY src/Services.Api/Services.Api.csproj src/Services.Api/
COPY src/Services.Application/Services.Application.csproj src/Services.Application/
COPY src/Services.Domain/Services.Domain.csproj src/Services.Domain/
COPY src/Services.Infrastructure/Services.Infrastructure.csproj src/Services.Infrastructure/
RUN dotnet restore src/Services.Api/Services.Api.csproj

COPY src/ src/
RUN dotnet publish src/Services.Api/Services.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Services.Api.dll"]
