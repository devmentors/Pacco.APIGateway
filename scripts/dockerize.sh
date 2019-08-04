#!/bin/bash
TAG=''

case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_TAG=dev
    ;;    
esac

IMAGE=$DOCKER_USERNAME/pacco.apigateway:$TAG

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -t $IMAGE .
docker push $IMAGE