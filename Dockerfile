FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/Pacco.APIGateway -c release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENV NTRADA_CONFIG ntrada.docker
ENTRYPOINT dotnet Pacco.APIGateway.dll