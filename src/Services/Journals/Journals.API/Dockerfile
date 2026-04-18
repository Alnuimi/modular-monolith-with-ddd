FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Journals/Journals.API/Journals.API.csproj", "Services/Journals/Journals.API/"]
COPY ["BuildingBlocks/Articles.Abstractions/Articles.Abstractions.csproj", "BuildingBlocks/Articles.Abstractions/"]
COPY ["BuildingBlocks/Blocks.Domain/Blocks.Domain.csproj", "BuildingBlocks/Blocks.Domain/"]
COPY ["BuildingBlocks/Articles.Grpc.Contracts/Articles.Grpc.Contracts.csproj", "BuildingBlocks/Articles.Grpc.Contracts/"]
COPY ["BuildingBlocks/Articles.Security/Articles.Security.csproj", "BuildingBlocks/Articles.Security/"]
COPY ["BuildingBlocks/Blocks.Core/Blocks.Core.csproj", "BuildingBlocks/Blocks.Core/"]
COPY ["BuildingBlocks/Blocks.Exceptions/Blocks.Exceptions.csproj", "BuildingBlocks/Blocks.Exceptions/"]
COPY ["BuildingBlocks/Blocks.AspNetCore/Blocks.AspNetCore.csproj", "BuildingBlocks/Blocks.AspNetCore/"]
COPY ["Services/Journals/Journals.Persistence/Journals.Persistence.csproj", "Services/Journals/Journals.Persistence/"]
COPY ["Services/Journals/Journals.Domain/Journals.Domain.csproj", "Services/Journals/Journals.Domain/"]
COPY ["BuildingBlocks/Blocks.Redis/Blocks.Redis.csproj", "BuildingBlocks/Blocks.Redis/"]
COPY ["BuildingBlocks/Blocks.FastEndpoints/Blocks.FastEndpoints.csproj", "BuildingBlocks/Blocks.FastEndpoints/"]
COPY ["BuildingBlocks/Blocks.Messaging/Blocks.Messaging.csproj", "BuildingBlocks/Blocks.Messaging/"]
RUN dotnet restore "Services/Journals/Journals.API/Journals.API.csproj"
COPY . .
WORKDIR "/src/Services/Journals/Journals.API"
RUN dotnet build "./Journals.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Journals.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Journals.API.dll"]
