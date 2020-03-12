FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /index
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Commande.csproj", ""]
RUN dotnet restore "./Commande.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Commande.csproj" -c Release -o /index

FROM build AS publish
RUN dotnet publish "Commande.csproj" -c Release -o /index

FROM base AS final
WORKDIR /index
COPY --from=publish /index .
ENTRYPOINT ["dotnet", "Commande.dll"]