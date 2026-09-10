FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "Complexion.Api/Complexion.Api.csproj"
RUN dotnet publish "Complexion.Api/Complexion.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN useradd --create-home appuser
USER appuser

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Complexion.Api.dll"]