FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrdersWorker/OrdersWorker.csproj", "OrdersWorker/"]
RUN dotnet restore "OrdersWorker/OrdersWorker.csproj"
COPY . .
WORKDIR "/src/OrdersWorker"
RUN dotnet build "OrdersWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersWorker.dll"]
