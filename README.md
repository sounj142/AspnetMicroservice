Navigate to /src folder, open cmd and run one of the commands bellow:
- Build and run:
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build
- Only run
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
- Stop and remove all docker containers
docker-compose -f docker-compose.yml -f docker-compose.override.yml down
