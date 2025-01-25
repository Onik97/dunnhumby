FROM oven/bun:latest AS build-frontend
WORKDIR /app
COPY . .
WORKDIR /app/Dunnhumby.Web
RUN bun install
RUN bun run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend
WORKDIR /app
COPY Dunnhumby.API/Dunnhumby.API.csproj .
RUN dotnet restore
COPY Dunnhumby.API .
RUN dotnet build "Dunnhumby.API.csproj" -c Release -o /app/build
FROM build-backend AS publish
RUN dotnet publish "Dunnhumby.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 5101
COPY --from=publish /app/publish .
COPY --from=build-frontend /app/Dunnhumby.API/wwwroot ./wwwroot
COPY --from=build-frontend /app/Dunnhumby.API/dunnhumby.db /app/dunnhumby.db 
ENTRYPOINT ["dotnet", "Dunnhumby.API.dll"]