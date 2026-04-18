FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Submissions/Submission.API/Submission.API.csproj", "Services/Submissions/Submission.API/"]
COPY ["BuildingBlocks/Articles.Security/Articles.Security.csproj", "BuildingBlocks/Articles.Security/"]
COPY ["BuildingBlocks/Articles.Abstractions/Articles.Abstractions.csproj", "BuildingBlocks/Articles.Abstractions/"]
COPY ["BuildingBlocks/Blocks.Domain/Blocks.Domain.csproj", "BuildingBlocks/Blocks.Domain/"]
COPY ["BuildingBlocks/Blocks.Core/Blocks.Core.csproj", "BuildingBlocks/Blocks.Core/"]
COPY ["BuildingBlocks/Blocks.Exceptions/Blocks.Exceptions.csproj", "BuildingBlocks/Blocks.Exceptions/"]
COPY ["BuildingBlocks/Blocks.AspNetCore/Blocks.AspNetCore.csproj", "BuildingBlocks/Blocks.AspNetCore/"]
COPY ["Modules/FileStorage/FileStorage.MongoGridFS/FileStorage.MongoGridFS.csproj", "Modules/FileStorage/FileStorage.MongoGridFS/"]
COPY ["Modules/FileStorage/FileStorage.Contracts/FileStorage.Contracts.csproj", "Modules/FileStorage/FileStorage.Contracts/"]
COPY ["Services/Submissions/Submission.Application/Submission.Application.csproj", "Services/Submissions/Submission.Application/"]
COPY ["BuildingBlocks/Articles.Grpc.Contracts/Articles.Grpc.Contracts.csproj", "BuildingBlocks/Articles.Grpc.Contracts/"]
COPY ["BuildingBlocks/Blocks.MediatR/Blocks.MediatR.csproj", "BuildingBlocks/Blocks.MediatR/"]
COPY ["BuildingBlocks/Blocks.Messaging/Blocks.Messaging.csproj", "BuildingBlocks/Blocks.Messaging/"]
COPY ["Services/Submissions/Submission.Persistence/Submission.Persistence.csproj", "Services/Submissions/Submission.Persistence/"]
COPY ["BuildingBlocks/Blocks.EntityFramework/Blocks.EntityFramework.csproj", "BuildingBlocks/Blocks.EntityFramework/"]
COPY ["Services/Submissions/Submission.Domain/Submission.Domain.csproj", "Services/Submissions/Submission.Domain/"]
COPY ["BuildingBlocks/Articles.IntegrationEvents.Contracts/Articles.IntegrationEvents.Contracts.csproj", "BuildingBlocks/Articles.IntegrationEvents.Contracts/"]
RUN dotnet restore "Services/Submissions/Submission.API/Submission.API.csproj"
COPY . .
WORKDIR "/src/Services/Submissions/Submission.API"
RUN dotnet build "./Submission.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Submission.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Submission.API.dll"]
