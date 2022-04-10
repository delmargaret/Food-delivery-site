FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EzhaBy.Api/EzhaBy.Api.csproj", "EzhaBy.Api/"]
COPY ["EzhaBy.Business/EzhaBy.Business.csproj", "EzhaBy.Business/"]
COPY ["EzhaBy.Entities/EzhaBy.Entities.csproj", "EzhaBy.Entities/"]
COPY ["EzhaBy.Infrastructure/EzhaBy.Infrastructure.csproj", "EzhaBy.Infrastructure/"]
RUN dotnet restore "EzhaBy.Api/EzhaBy.Api.csproj"
COPY . .
WORKDIR "/src/EzhaBy.Api"
RUN dotnet build "EzhaBy.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "EzhaBy.Api.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EzhaBy.Api.dll"]
