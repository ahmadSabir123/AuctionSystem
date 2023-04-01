FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/AuctionSystem.Web.Host/AuctionSystem.Web.Host.csproj", "src/AuctionSystem.Web.Host/"]
COPY ["src/AuctionSystem.Web.Core/AuctionSystem.Web.Core.csproj", "src/AuctionSystem.Web.Core/"]
COPY ["src/AuctionSystem.Application/AuctionSystem.Application.csproj", "src/AuctionSystem.Application/"]
COPY ["src/AuctionSystem.Core/AuctionSystem.Core.csproj", "src/AuctionSystem.Core/"]
COPY ["src/AuctionSystem.EntityFrameworkCore/AuctionSystem.EntityFrameworkCore.csproj", "src/AuctionSystem.EntityFrameworkCore/"]
WORKDIR "/src/src/AuctionSystem.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/AuctionSystem.Web.Host", "src/AuctionSystem.Web.Host"]
COPY ["src/AuctionSystem.Web.Core", "src/AuctionSystem.Web.Core"]
COPY ["src/AuctionSystem.Application", "src/AuctionSystem.Application"]
COPY ["src/AuctionSystem.Core", "src/AuctionSystem.Core"]
COPY ["src/AuctionSystem.EntityFrameworkCore", "src/AuctionSystem.EntityFrameworkCore"]
WORKDIR "/src/src/AuctionSystem.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AuctionSystem.Web.Host.dll"]
