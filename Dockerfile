FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuickJob.Cabinet.Api/QuickJob.Cabinet.Api.csproj", "QuickJob.Cabinet.Api/"]
RUN dotnet restore "QuickJob.Cabinet.Api/QuickJob.Cabinet.Api.csproj"
COPY . .
WORKDIR "/src/QuickJob.Cabinet.Api"
RUN dotnet build "QuickJob.Cabinet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuickJob.Cabinet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickJob.Cabinet.Api.dll"]
