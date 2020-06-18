#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GraphQLTryOuts.Users/GraphQLTryOuts.Users.csproj", "GraphQLTryOuts.Users/"]
COPY ["GraphQLTryOuts.Users.Data/GraphQLTryOuts.Users.Data.csproj", "GraphQLTryOuts.Users.Data/"]
COPY ["GraphQLTryOuts.Messaging.Models/GraphQLTryOuts.Messaging.Shared.csproj", "GraphQLTryOuts.Messaging.Models/"]
RUN dotnet restore "GraphQLTryOuts.Users/GraphQLTryOuts.Users.csproj"
COPY . .
WORKDIR "/src/GraphQLTryOuts.Users"
RUN dotnet build "GraphQLTryOuts.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQLTryOuts.Users.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLTryOuts.Users.dll"]