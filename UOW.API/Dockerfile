FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY UOW.API/UOW.API.csproj UOW.API/
COPY UOW.Entities/UOW.Entities.csproj UOW.Entities/
COPY UOW.Repositories/UOW.Repositories.csproj UOW.Repositories/
RUN dotnet restore "UOW.API/UOW.API.csproj"
COPY . .
WORKDIR "/src/UOW.API"
RUN dotnet build "UOW.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UOW.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UOW.API.dll"]
