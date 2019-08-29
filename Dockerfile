FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/Pacco.APIGateway -c release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build /app/src/Pacco.APIGateway/out .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENV NTRADA_CONFIG ntrada.docker
ENTRYPOINT dotnet Pacco.APIGateway.dll