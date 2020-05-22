#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /code
COPY . /code/src/Worker
RUN dotnet restore -v minimal src/Worker \
    && dotnet publish -c Release -o "." "src/Worker/"
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_ENVIROMENT=Development
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "Catalogo.API.dll"]