#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Empresa.Produto.Web/Empresa.Produto.Web.csproj", "src/Empresa.Produto.Web/"]
RUN dotnet restore "src/Empresa.Produto.Web/Empresa.Produto.Web.csproj"
COPY . .
WORKDIR "/src/src/Empresa.Produto.Web"
RUN dotnet build "Empresa.Produto.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Empresa.Produto.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Empresa.Produto.Web.dll"]