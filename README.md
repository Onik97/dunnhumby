# Dunnhumby Product Management Dashboard

## Overview

This project is a simple product management dashboard that includes a backend written in ASP.NET Core and a frontend built with React.

## Setup Instructions

1. Clone the repository to your local machine.
2. Ensure Docker and Docker Compose are installed.
3. Navigate to the project directory

### Docker Compose

The quickest way is running one docker compose command, this will build the image and run it.

```
docker compose up -d
```

Access the application at `http://localhost:5101`.

### Dockerfile

1. Build the image using the following command:

```
docker build -t dunnhumby-api .
```

2. Run the image (in this example exposing port 5101):

```
docker run -p 5101:5101 dunnhumby-api
```

## API Endpoints

- **GET /api/products**: Retrieve a list of all products.
- **POST /api/products**: Adds a new product (JSON format):

  ```
  {
      "category": "string",
      "name": "string",
      "productCode": "string",
      "price": "decimal",
      "stockQuantity": "int",
      "dateAdded": "DateTime"
  }
  ```

- **GET /api/products/stockcategory**: Get total stock quantity per category.

## Database

- The project uses SQLite as an embedded database. The database file (`dunnhumby.db`) is copied into the container
  during the build process.

## Frontend

- The frontend is built using React and uses a table to display product details and a graph to represent stock quantity per
  category.

## Graphs and Features

- **Graphs**: Displays total stock quantity per category.
- **Table**: Displays sortable and filterable columns for product data.

## Testing

- The application includes unit tests for backend functionality.
- Run tests with the following command inside the container:

```
dotnet test
```

## Dockerization

The application is fully containerized with the following Dockerfile structure:

- **Frontend Build**: Uses Bun to build the React frontend.
- **Backend Build**: Builds and publishes the ASP.NET Core API.
- **Runtime**: Runs the published API with the built frontend.

## Technologies Used

- ASP.NET Core 8.0
- React
- SQLite
- Docker
