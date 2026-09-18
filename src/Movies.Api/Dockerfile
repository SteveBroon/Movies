FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5222

ENV ASPNETCORE_URLS=http://+:5222

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Movies.Api/Movies.Api.csproj", "src/Movies.Api/"]
RUN dotnet restore "src/Movies.Api/Movies.Api.csproj"
COPY . .
WORKDIR "/src/src/Movies.Api"
RUN dotnet build "Movies.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Movies.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movies.Api.dll"]
