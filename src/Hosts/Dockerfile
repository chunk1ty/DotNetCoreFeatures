#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Hosts/DotNetCoreFeatures.Hosts.csproj", "src/Hosts/"]
RUN dotnet restore "src/Hosts/DotNetCoreFeatures.Hosts.csproj"
COPY . .
WORKDIR "/src/src/Hosts"
RUN dotnet build "DotNetCoreFeatures.Hosts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetCoreFeatures.Hosts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetCoreFeatures.Hosts.dll"]