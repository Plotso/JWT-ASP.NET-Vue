#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JWTShowcase.Identity/JWTShowcase.Identity.csproj", "JWTShowcase.Identity/"]
COPY ["JWTShowcase/JWTShowcase.csproj", "JWTShowcase/"]
RUN dotnet restore "JWTShowcase.Identity/JWTShowcase.Identity.csproj"
COPY . .
WORKDIR "/src/JWTShowcase.Identity"
RUN dotnet build "JWTShowcase.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWTShowcase.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTShowcase.Identity.dll"]