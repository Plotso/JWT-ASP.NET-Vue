FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JWTShowcase.Posts/JWTShowcase.Posts.csproj", "JWTShowcase.Posts/"]
COPY ["JWTShowcase/JWTShowcase.csproj", "JWTShowcase/"]
RUN dotnet restore "JWTShowcase.Posts/JWTShowcase.Posts.csproj"
COPY . .
WORKDIR "/src/JWTShowcase.Posts"
RUN dotnet build "JWTShowcase.Posts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWTShowcase.Posts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTShowcase.Posts.dll"]
