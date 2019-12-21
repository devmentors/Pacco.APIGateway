![Pacco](https://raw.githubusercontent.com/devmentors/Pacco/master/assets/pacco_logo.png)

**What is Pacco?**
----------------

Pacco is an open source project using microservices architecture written in .NET Core 3.1 and the domain tackles the exclusive parcels delivery which revolves around the general concept of limited resources availability. To read more about this project [click here](https://github.com/devmentors/Pacco).

**What is Pacco.APIGateway?**
----------------

Pacco.APIGateway is an API Gateway built with [Ntrada](https://github.com/snatch-dev/Ntrada) being part of [Pacco](https://github.com/devmentors/Pacco) solution.

|Branch             |Build status                                                  
|-------------------|-----------------------------------------------------
|master             |[![master branch build status](https://api.travis-ci.org/devmentors/Pacco.APIGateway.svg?branch=master)](https://travis-ci.org/devmentors/Pacco.APIGateway)
|develop            |[![develop branch build status](https://api.travis-ci.org/devmentors/Pacco.APIGateway.svg?branch=develop)](https://travis-ci.org/devmentors/Pacco.APIGateway/branches)

**How to start the application?**
----------------

Service can be started locally via `dotnet run` command (executed in the `/src/Pacco.APIGateway` directory) or by running `./scripts/start.sh` shell script in the root folder of repository.

By default, the service will be available under http://localhost:5000.

You can also start the service via Docker, either by building a local Dockerfile: 

`docker build -t pacco.apigateway .` 

or using the official one: 

`docker pull devmentors/pacco.apigateway`