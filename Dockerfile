FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SimbirHomeworkClean.Api/SimbirHomeworkClean.Api.csproj", "SimbirHomeworkClean.Api/"]
RUN dotnet restore "SimbirHomeworkClean.Api/SimbirHomeworkClean.Api.csproj"
COPY . .
WORKDIR "/src/SimbirHomeworkClean.Api"
RUN dotnet build "SimbirHomeworkClean.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimbirHomeworkClean.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimbirHomeworkClean.Api.dll"]
