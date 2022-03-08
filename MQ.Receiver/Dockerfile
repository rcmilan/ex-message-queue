#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MQ.Receiver/MQ.Receiver.csproj", "MQ.Receiver/"]
RUN dotnet restore "MQ.Receiver/MQ.Receiver.csproj"
COPY . .
WORKDIR "/src/MQ.Receiver"
RUN dotnet build "MQ.Receiver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQ.Receiver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQ.Receiver.dll"]
