FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Auth/Auth.API/Auth.API.csproj", "Services/Auth/Auth.API/"]
COPY ["BuildingBlocks/Articles.Abstractions/Articles.Abstractions.csproj", "BuildingBlocks/Articles.Abstractions/"]
COPY ["BuildingBlocks/Blocks.Domain/Blocks.Domain.csproj", "BuildingBlocks/Blocks.Domain/"]
COPY ["BuildingBlocks/Articles.Grpc.Contracts/Articles.Grpc.Contracts.csproj", "BuildingBlocks/Articles.Grpc.Contracts/"]
COPY ["BuildingBlocks/Blocks.AspNetCore/Blocks.AspNetCore.csproj", "BuildingBlocks/Blocks.AspNetCore/"]
COPY ["BuildingBlocks/Blocks.Core/Blocks.Core.csproj", "BuildingBlocks/Blocks.Core/"]
COPY ["BuildingBlocks/Blocks.Exceptions/Blocks.Exceptions.csproj", "BuildingBlocks/Blocks.Exceptions/"]
COPY ["Modules/EmailService/EmailService.Smtp/EmailService.Smtp.csproj", "Modules/EmailService/EmailService.Smtp/"]
COPY ["Modules/EmailService/EmailService.Contracts/EmailService.Contracts.csproj", "Modules/EmailService/EmailService.Contracts/"]
COPY ["Services/Auth/Auth.Application/Auth.Application.csproj", "Services/Auth/Auth.Application/"]
COPY ["BuildingBlocks/Articles.Security/Articles.Security.csproj", "BuildingBlocks/Articles.Security/"]
COPY ["Services/Auth/Auth.Domain/Auth.Domain.csproj", "Services/Auth/Auth.Domain/"]
COPY ["Services/Auth/Auth.Persistence/Auth.Persistence.csproj", "Services/Auth/Auth.Persistence/"]
COPY ["BuildingBlocks/Blocks.EntityFramework/Blocks.EntityFramework.csproj", "BuildingBlocks/Blocks.EntityFramework/"]
COPY ["BuildingBlocks/Blocks.MediatR/Blocks.MediatR.csproj", "BuildingBlocks/Blocks.MediatR/"]
COPY ["BuildingBlocks/Blocks.FastEndpoints/Blocks.FastEndpoints.csproj", "BuildingBlocks/Blocks.FastEndpoints/"]
RUN dotnet restore "Services/Auth/Auth.API/Auth.API.csproj"
COPY . .
WORKDIR "/src/Services/Auth/Auth.API"
RUN dotnet build "./Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.API.dll"]
