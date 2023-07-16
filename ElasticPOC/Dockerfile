FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElasticPOC/ElasticPOC.csproj", "ElasticPOC/"]
RUN dotnet restore "ElasticPOC/ElasticPOC.csproj"
COPY . .
WORKDIR "/src/ElasticPOC"
RUN dotnet build "ElasticPOC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticPOC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElasticPOC.dll"]
