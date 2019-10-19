#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
export NTRADA_CONFIG=ntrada-async.yml
cd src/Pacco.APIGateway
dotnet run
