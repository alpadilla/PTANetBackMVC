FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Alicunde.System.Exam.Api/Alicunde.System.Exam.Api.csproj", "src/Alicunde.System.Exam.Api/"]
COPY ["src/Alicunde.System.Exam.Api.Client/Alicunde.System.Exam.Api.Client.csproj", "src/Alicunde.System.Exam.Api.Client/"]
COPY ["src/Alicunde.System.Exam.Contracts/Alicunde.System.Exam.Contracts.csproj", "src/Alicunde.System.Exam.Contracts/"]
COPY ["src/Alicunde.System.Exam.Domain.Shared/Alicunde.System.Exam.Domain.Shared.csproj", "src/Alicunde.System.Exam.Domain.Shared/"]
COPY ["src/Alicunde.System.Exam.Domain/Alicunde.System.Exam.Domain.csproj", "src/Alicunde.System.Exam.Domain/"]
COPY ["src/Alicunde.System.Exam.EntityFrameworkCore/Alicunde.System.Exam.EntityFrameworkCore.csproj", "src/Alicunde.System.Exam.EntityFrameworkCore/"]
COPY ["src/Alicunde.System.Exam.Services/Alicunde.System.Exam.Services.csproj", "src/Alicunde.System.Exam.Services/"]
COPY ["src/Alicunde.System.Exam.Aplication/Alicunde.System.Exam.Aplication.csproj", "src/Alicunde.System.Exam.Aplication/"]
RUN dotnet restore "src/Alicunde.System.Exam.Api/Alicunde.System.Exam.Api.csproj"
COPY . .
WORKDIR "/src/src/Alicunde.System.Exam.Api"
RUN dotnet build "Alicunde.System.Exam.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Alicunde.System.Exam.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alicunde.System.Exam.Api.dll"]
