FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY AutoSpaPro/*.csproj ./AutoSpaPro/
RUN dotnet restore ./AutoSpaPro/AutoSpaPro.csproj
COPY AutoSpaPro/. ./AutoSpaPro/
RUN dotnet publish ./AutoSpaPro/AutoSpaPro.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["/bin/sh", "-c", "dotnet AutoSpaPro.dll --urls http://+:${PORT:-8080}"]
