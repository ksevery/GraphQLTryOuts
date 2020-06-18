#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1883
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GraphQLTryOuts.MessagingServer/GraphQLTryOuts.MessagingServer.csproj", "GraphQLTryOuts.MessagingServer/"]
RUN dotnet restore "GraphQLTryOuts.MessagingServer/GraphQLTryOuts.MessagingServer.csproj"
COPY . .
WORKDIR "/src/GraphQLTryOuts.MessagingServer"
RUN dotnet build "GraphQLTryOuts.MessagingServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQLTryOuts.MessagingServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLTryOuts.MessagingServer.dll"]