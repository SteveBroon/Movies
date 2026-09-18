# Movies

A .NET 10 Web API for searching and retrieving movie information.

The application uses a Clean Architecture approach, with separate projects for the API, application logic, domain entities, and infrastructure.

The movie data is stored in SQL Server and accessed using Entity Framework Core.

## Features

- Search movies by title
- Filter movies by genre
- Sort movies by title or release date
- Paginate movie results
- Retrieve an individual movie by ID
- Retrieve available genres
- Associated genres included with movie results
- Structured logging using `ILogger`
- Global exception handling middleware
- Unit tests using xUnit and Moq
- Docker support

## Project Structure

```text
Movies/
├── src/
│   ├── Movies.Api/
│   ├── Movies.Application/
│   ├── Movies.Domain/
│   └── Movies.Infrastructure/
├── tests/
│   ├── Movies.UnitTests/
└── Movies.slnx
```

### Projects

**Movies.Api**

ASP.NET Core Web API containing controllers, middleware and application startup.

**Movies.Application**

Contains application services, requests, responses and repository/service interfaces.

**Movies.Domain**

Contains the core domain entities such as `Movie`, `Genre` and `MovieGenre`.

**Movies.Infrastructure**

Contains Entity Framework Core, SQL Server configuration, repositories and database configuration.

---

## Prerequisites

To run the application locally you will need:

- .NET 10 SDK
- SQL Server
- Git

For running the application using Docker:

- Docker Desktop

Check the installed .NET version:

```bash
dotnet --version
```

---

# Clone the Repository

Clone the repository:

```bash
git clone https://github.com/SteveBroon/Movies.git
```

Change into the project directory:

```bash
cd Movies
```

Restore the NuGet packages:

```bash
dotnet restore
```

---

# Build the Application

Build the entire solution:

```bash
dotnet build
```

To build in Release configuration:

```bash
dotnet build --configuration Release
```

---

# Database Setup

The application uses SQL Server with Entity Framework Core.

The connection string is configured in:

```text
src/Movies.Api/appsettings.json
```

For example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MoviesDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Update the connection string to match your SQL Server configuration.

## Create the Database

Data was extracted from https://www.kaggle.com/datasets/disham993/9000-movies-dataset/data and manipulated in an attempt to separate genre information into a separate table.

## SQL Server Database Setup

To initialise the database manually in SQL Server:

1. Open **SQL Server Management Studio (SSMS)** or another SQL Server client.
2. Create a new database, for example:

```sql
CREATE DATABASE Movies;
GO
```

3. Select the newly created **Movies** database.
4. Run the following SQL scripts from ./Setup **in order**:

   1. **Migration.sql – Create Tables**
      Creates the database tables and their relationships.

   3. **SeedData.sql – Load Movie Data**
      Populates the database with the movie and genre data.

For each script, make sure the **Movies** database is selected before executing it.


> **Note:** Run the scripts in the supplied order, as later scripts may depend on objects created by earlier scripts.
Script has duplicate data but still executes.


# Run the API Locally

From the solution root:

```bash
dotnet run --project "./src/Movies.Api/Movies.Api.csproj"
```

Alternatively, change to the API directory:

```bash
cd src/Movies.Api
dotnet run
```

The API will display the listening URL in the console.

For example:

```text
Now listening on: http://localhost:5222
```

The exact port may differ depending on the local launch configuration.

---

# Run Using Docker

The application can also be built and run as a Docker container.

From the solution root, build the image:

```bash
docker build -t movies-api -f src/Movies.Api/Dockerfile .
```

Run the container:

```bash
docker run -p 5222:5222 movies-api
```

The API will then be available at:

```text
http://localhost:5222
```

## Docker and SQL Server

When running the API in Docker, the SQL Server connection string must point to a SQL Server instance that is accessible from the container.


---

# API Endpoints

The API exposes endpoints for movies and genres.

## Get Movies

```http
GET /Movies
```

Returns a paginated list of movies.

### Example

```http
GET /Movies?page=1&pageSize=20
```

Example response:

```json
{
  "items": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "title": "The Matrix",
      "releaseDate": "1999-03-30T00:00:00",
      "genres": [
        "Action",
        "Science Fiction"
      ]
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 9000,
  "totalPages": 450
}
```

## Search by Title

```http
GET /Movies?search=spider
```

The search performs a partial title match.

For example:

```http
GET /Movies?search=spider&page=1&pageSize=10
```

## Filter by Genre

```http
GET /Movies?genre=1
```

The search can be combined with title searching:

```http
GET /Movies?search=spider&genre=1
```

## Sorting

Movies can be sorted by:

- `title`
- `releaseDate`

Ascending order:

```http
GET /Movies?sortBy=title
```

Descending order:

```http
GET /Movies?sortBy=title&descending=true
```

For release date:

```http
GET /Movies?sortBy=releaseDate&descending=true
```

## Pagination

Pagination is controlled using `page` and `pageSize`.

```http
GET /Movies?page=2&pageSize=20
```

`pageSize` is limited to a maximum of 100.

Search, filtering, sorting and pagination can be combined:

```http
GET /Movies?search=spider&genre=Action&sortBy=releaseDate&descending=true&page=1&pageSize=10
```

## Get Movie by ID

```http
GET /Movies/{id}
```

Example:

```http
GET /Movies/8f6c5e9d-7f2a-4c9a-9f8d-123456789abc
```

Returns:

- `200 OK` when the movie exists
- `404 Not Found` when the movie does not exist

## Get Genres

```http
GET /Genres
```

Returns the available movie genres.

Example:

```json
[
  {
    "id": "1",
    "name": "Action"
  },
  {
    "id": "2",
    "name": "Comedy"
  }
]
```

---

# API Documentation

When running in the Development environment, OpenAPI is enabled.

The generated OpenAPI document can be accessed from:

```text
/openapi/v1.json
```

The API can also be tested using tools such as:

- Swagger-compatible clients
- Postman
- curl
- Visual Studio / VS Code REST clients

---

# Running Tests

The solution contains unit tests for the API controllers and application services.

Run all tests from the solution root:

```bash
dotnet test
```

Run tests with detailed output:

```bash
dotnet test --logger "console;verbosity=detailed"
```

## Test Coverage

The tests cover areas including:

- Controller responses
- HTTP status code mapping
- Service behaviour
- Movie-to-response mapping
- Genre mapping
- Pagination metadata
- Repository parameter handling
- Missing movie handling

Database-specific query behaviour is kept separate from application unit tests so that service and controller tests remain fast and focused.

---

# Architecture

The application follows a Clean Architecture style.

```text
                    Movies.Api
                        |
                        v
                Movies.Application
                        |
                        v
                   Movies.Domain

                Movies.Infrastructure
                        |
                        v
                Movies.Application
                        |
                        v
                   Movies.Domain
```

The API is responsible for HTTP concerns, the Application layer contains business/application logic, the Domain contains the core entities, and Infrastructure handles persistence and external technical concerns.

Entity Framework Core is used by the Infrastructure project to query SQL Server.

---

# Logging and Error Handling

The application uses ASP.NET Core's built-in `ILogger<T>` for structured logging.

Unhandled exceptions are captured by the global exception-handling middleware in the API project.

Exceptions are logged with their details while the API returns a generic error response to the client rather than exposing internal exception information.

---

# Technology Stack

- .NET 10
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- Docker
- xUnit
- Moq
- OpenAPI
- Clean Architecture