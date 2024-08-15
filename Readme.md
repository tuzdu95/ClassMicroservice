Docker
Dockerfile (file name must be Dockerfile): A file about how to create image
- Can have multiple state to make use of cache on each state. State start with FROM [base] AS build.base here can be an image or another state
- Different OS will have different Dockerfile in some config (like Linux will need to specify the user, env should start with $ instead of % like in Windows)

docker build
docker run with arguments : -d :detached mode; -f: file
docker stop
docker-compose up: dockercompose.yaml: how to run the image : run images by config inside; -f for yaml file
docker-compose down: shut down all the images related, delete network
- network will be automatically created
- using volumes to save data from container in host machine => avoid lost data
- when using multiple -f yaml file; the files will be merge and replace => use for multiple env config like production, develop,...
- docker-compose is used in case multiple container in same host => in distributed host need use docker swarm or kubernetes