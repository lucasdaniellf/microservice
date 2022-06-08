# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY /PlatformApp/PlatformApp/*.sln ./PlatformApp/PlatformApp/
COPY /PlatformApp/PlatformApp/*.csproj ./PlatformApp/PlatformApp/
COPY /Contracts/Contracts/*.csproj ./Contracts/Contracts/
#
RUN dotnet restore "./Contracts/Contracts/Contracts.csproj"
RUN dotnet restore "./PlatformApp/PlatformApp/PlatformApp.csproj"

#
# copy everything else and build app
COPY /Contracts/Contracts/. ./Contracts/Contracts/
COPY /PlatformApp/PlatformApp/. ./PlatformApp/PlatformApp/

#
WORKDIR /app/PlatformApp/PlatformApp
RUN dotnet publish -c Release -o out 
WORKDIR /app/Contracts/Contracts
RUN dotnet publish -c Release -o out 


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/PlatformApp/PlatformApp/out .
ENTRYPOINT ["dotnet", "PlatformApp.dll"]
