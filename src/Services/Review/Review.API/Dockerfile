FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Review/Review.API/Review.API.csproj", "Services/Review/Review.API/"]
COPY ["BuildingBlocks/Blocks.AspNetCore/Blocks.AspNetCore.csproj", "BuildingBlocks/Blocks.AspNetCore/"]
COPY ["BuildingBlocks/Blocks.Core/Blocks.Core.csproj", "BuildingBlocks/Blocks.Core/"]
COPY ["BuildingBlocks/Blocks.Exceptions/Blocks.Exceptions.csproj", "BuildingBlocks/Blocks.Exceptions/"]
COPY ["BuildingBlocks/Blocks.Domain/Blocks.Domain.csproj", "BuildingBlocks/Blocks.Domain/"]
COPY ["Modules/FileStorage/FileStorage.MongoGridFS/FileStorage.MongoGridFS.csproj", "Modules/FileStorage/FileStorage.MongoGridFS/"]
COPY ["Modules/FileStorage/FileStorage.Contracts/FileStorage.Contracts.csproj", "Modules/FileStorage/FileStorage.Contracts/"]
COPY ["Services/Review/Review.Application/Review.Application.csproj", "Services/Review/Review.Application/"]
COPY ["BuildingBlocks/Blocks.Messaging/Blocks.Messaging.csproj", "BuildingBlocks/Blocks.Messaging/"]
COPY ["BuildingBlocks/Articles.Abstractions/Articles.Abstractions.csproj", "BuildingBlocks/Articles.Abstractions/"]
COPY ["Services/Review/Review.Persistence/Review.Persistence.csproj", "Services/Review/Review.Persistence/"]
COPY ["BuildingBlocks/Blocks.EntityFramework/Blocks.EntityFramework.csproj", "BuildingBlocks/Blocks.EntityFramework/"]
COPY ["BuildingBlocks/Blocks.MediatR/Blocks.MediatR.csproj", "BuildingBlocks/Blocks.MediatR/"]
COPY ["Services/Review/Review.Domain/Review.Domain.csproj", "Services/Review/Review.Domain/"]
COPY ["BuildingBlocks/Articles.Grpc.Contracts/Articles.Grpc.Contracts.csproj", "BuildingBlocks/Articles.Grpc.Contracts/"]
COPY ["BuildingBlocks/Articles.IntegrationEvents.Contracts/Articles.IntegrationEvents.Contracts.csproj", "BuildingBlocks/Articles.IntegrationEvents.Contracts/"]
COPY ["Modules/EmailService/EmailService.Contracts/EmailService.Contracts.csproj", "Modules/EmailService/EmailService.Contracts/"]
COPY ["BuildingBlocks/Articles.Security/Articles.Security.csproj", "BuildingBlocks/Articles.Security/"]
COPY ["Modules/EmailService/EmailService.Smtp/EmailService.Smtp.csproj", "Modules/EmailService/EmailService.Smtp/"]
RUN dotnet restore "Services/Review/Review.API/Review.API.csproj"
COPY . .
WORKDIR "/src/Services/Review/Review.API"
RUN dotnet build "./Review.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Review.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Review.API.dll"]
