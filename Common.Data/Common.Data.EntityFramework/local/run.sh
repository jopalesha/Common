#!/bin/sh
docker_compose_cmd="docker-compose -f ./docker-compose.yml"

${docker_compose_cmd} up -d mssql


# ${docker_compose_cmd} down
