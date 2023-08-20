FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Trading.Api/Trading.Api.csproj", "Trading.Api/"]
RUN dotnet restore "Trading.Api/Trading.Api.csproj"
COPY . .
WORKDIR "/src/Trading.Api"
RUN dotnet build "Trading.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trading.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trading.Api.dll"]