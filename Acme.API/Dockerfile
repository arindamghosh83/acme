#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8090

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Acme.API/Acme.API.csproj", "Acme.API/"]
RUN dotnet restore "Acme.API/Acme.API.csproj"
COPY . .
WORKDIR "/src/Acme.API"
RUN dotnet build "Acme.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Acme.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:8090
ENTRYPOINT ["dotnet", "Acme.API.dll"]