#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GraphQLTryOuts.Identity/GraphQLTryOuts.Identity.csproj", "GraphQLTryOuts.Identity/"]
COPY ["GraphQLTryOuts.Messaging.Models/GraphQLTryOuts.Messaging.Shared.csproj", "GraphQLTryOuts.Messaging.Models/"]
COPY ["GraphQLTryOuts.Identity.Data/GraphQLTryOuts.Identity.Data.csproj", "GraphQLTryOuts.Identity.Data/"]
RUN dotnet restore "GraphQLTryOuts.Identity/GraphQLTryOuts.Identity.csproj"
COPY . .
WORKDIR "/src/GraphQLTryOuts.Identity"
RUN dotnet build "GraphQLTryOuts.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQLTryOuts.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLTryOuts.Identity.dll"]