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

HEALTHCHECK --interval=30s --timeout=3s CMD bash -c 'exec 3<>/dev/tcp/localhost/8080; echo -e "GET /health HTTP/1.1\r\nHost: localhost\r\nConnection: close\r\n\r\n" >&3; grep -q "200" <&3' || exit 1