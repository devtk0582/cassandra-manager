FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CassandraManager/CassandraManager.csproj", "CassandraManager/"]
RUN dotnet restore "CassandraManager/CassandraManager.csproj"
COPY . .
WORKDIR "/src/CassandraManager"
RUN dotnet build "CassandraManager.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CassandraManager.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CassandraManager.dll"]