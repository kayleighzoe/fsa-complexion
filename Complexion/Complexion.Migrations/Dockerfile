FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "Complexion.Migrations/Complexion.Migrations.csproj"
RUN dotnet publish "Complexion.Migrations/Complexion.Migrations.csproj" -c Release -o /app/publish

# checkov:skip=CKV_DOCKER_2: One-shot migration job with no long-running process to health-poll
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN useradd --create-home appuser
USER appuser

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Complexion.Migrations.dll"]