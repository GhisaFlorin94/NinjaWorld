
# Ninja World & War Resolver Service

## Overview

**Ninja World** and **War Resolver Service** are two interconnected services. The Ninja World service generates ninja objects and posts messages to a RabbitMQ queue. The War Resolver Service listens to the queue, simulates multiple battles in parallel, and returns the winners with a list of casualties.

## Prerequisites

- Docker or Podman installed on your machine
- Docker Compose installed on your machine

## How to Start the Application

### Using Docker Compose

1. **Clone the Repository**

    ```sh
    git clone <repository-url>
    cd <repository-directory>
    ```

2. **Build and Start the Services**

    ```sh
    docker-compose up --build
    ```

    This command will:
    - Build the Docker images for the Ninja World and War Resolver Service projects.
    - Start the PostgreSQL, RabbitMQ, Ninja World, and War Resolver Service containers.

3. **Access the Application**

    The application will be running at `http://localhost:8080`.

4. **Swagger UI**

    Access the Swagger UI to explore the API endpoints at:
    `http://localhost:8080/swagger/index.html`
    
5. **.NET HTTP File**
		At root level of NinjaWorld project, you can found a NinjaWorld.http file. You can use it to test the endpoints


## Services Configuration

### Ninja World Service

- **Dockerfile**: `./NinjaWorld/NinjaWorld.Dockerfile`
- **Exposed Port**: 8080

### War Resolver Service

- **Dockerfile**: `./WarResolverService/WarResolverService.Dockerfile`

### RabbitMQ (host hardcoded in code & using the default ports)

- **Management Port**: 15672
- **AMQP Port**: 5672

### PostgreSQL (Can be changed from appsetings.Json)

- **Port**: 5432
- **User**: `postgres`
- **Password**: `PostgresPassword`

## Additional Information

- Ensure that the `app-network` is properly configured to allow communication between services.
- The `depends_on` directive ensures that the services are started in the correct order.

## Improvements that were not implemented for time constrains

- Input Validations
- Propper Exception handling
- Probably a more clear folder structure
- A battle results aggregator logic
- RabbitMq code could have been decupled from the Busines logic(eg. create a publisher for each queue, and have a Factory/Strategy pattern that gets the proper publisher based on the message type) 