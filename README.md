# pv179_BookHub

## Overview
This repository contains a web application which allows browing and selling books of various genres. Users can register, login, and manage their profiles. They can also add books to their cart, proceed to checkout, review their purchase history, make wishlists, and rate books.
Administrators can update book details, prices and manage user accounts.

This project was created as an assignment for the "PV179" course at FI MUNI

Team composition:

| Name              | Role   |
|-------------------|--------|
| Ján Špaček        | lead   |
| Tomáš Valent      | member |
| Matej Alexej Helc | member |

## Used Technologies
- ASP.NET Core
- C# 9.0
- Entity Framework Core
- XUnit
- SQLite
- Identity Framework for auth
- MongoDB for storing logs

## How to run

### docker
You can easily run WebAPI and WebMVC project via docker.
1. have docker installed and launched
2. clone this repository
3. In root folder of this repository, type:
Windows:
```bash
docker-compose build; docker-compose up
```
Linux:
```bash
docker-compose build && docker-compose up
```
WebAPI is running on port 5000, swagger is on /swagger/index.html
WebMVC is running on port 5170

Otherwise, follow the instructions below to run the projects locally.

1. Clone the repository (example using SSH):
```bash
git clone git@gitlab.fi.muni.cz:xspacek1/pv179_bookhub.git 
```
2. Navigate to the project directory:
```bash
cd pv179_bookhub
```
3. Build the solution using the .NET CLI:
```bash
dotnet build
```
4. To set up the database, follow the instructions in the "Database Setup" section below.

### Running WebAPI
1. navigate to the WebAPI project directory:
```bash
cd WebAPI
```
2. Run the application:
```bash
dotnet run
```
3. Open your web browser and go to `http://localhost:5000/swagger` to access the Swagger UI and explore the API endpoints.
>**Note:** Authentication is required to use the API. See the [Authentication](#Authentication) section below for details.  


### Running WebMVC
1. To run the WebMVC project, navigate to its directory:
```bash
cd WebMVC
```

2. Run the WebMVC application:
```bash
dotnet run
```
3. Open your web browser and go to `http://localhost:5170` to access the web interface.

### Database Setup

The project uses Entity Framework Core for database management. To initialize the database, follow these steps:
1. Ensure you have the .NET SDK installed on your machine.
2. Set the `DefaultConnection` string in `appsettings.json` to point to your desired database location.
3. Open a terminal in the project directory and run the following commands to apply migrations and create the database:
   ```bash
   dotnet ef database update --project DataAccessLayer --startup-project WebAPI
   ```
4. After setting up the database the seeder will populate it with initial data automatically.
   ```bash
   dotnet ef database update --project DataAccessLayer--startup-project WebMVC
   ```

#### Logging Database Setup
The project uses MongoDB database to store request logs.

**1. Download MongoDB if it isn't already:**
```powershell
    choco install mongodb
    # or
    winget install MongoDB.Server
```
**2. Start MongoDB service:**
```powershell
    net start MongoDB
```
**3. Install MongoDB Compass (GUI):**
- Download: https://www.mongodb.com/try/download/compass
- Install and open the application

**4. Connect to database:**
1. In MongoDB Compass, click **"New Connection"**
2. Enter connection string: `mongodb://localhost:27017`
3. Click **"Connect"**
4. Run the application and after making a request you'll need to click "Refresh Databases" button
4. Then you'll see `BookHubLogs` database
5. Open `BookHubLogs` → `request_logs` to view logged requests

## Project Structure
This solution consists of these projets (so far)

### DataAccessLayer
- communicates with database through Entity Framwork
- Entities directory contains classes that represent database tables
- Migrations directory contains EF migrations
- Context directory contains DbContext class that represents a session with the database and allows querying and saving data. It also contains seeder.

### Business Layer
Business layer contains:
- DTO definitions split into requests and responses
- mappers between entities and DTOs,
- services that implement business logic.


### WebAPI
- provides REST API endpoints
- Controllers directory stores endpoint definitions and logic

#### Authentication
You can authenticate yourself by clicking the 'Authorize' button with a lock icon in the top right corner. For now, there is a static token, "YourHardcodedToken", which may be replaced in the future.

#### Logging
- RequestLoggingMiddleware for logging every incoming request
- RequestTimingMiddleware for measuring request processing time, enabling monitoring of API activity and performance.

#### XML responses
The API supports XML responses in addition to JSON. To receive XML responses, set the `Accept` header in your request to `application/xml`.


### WebMVC
- available at http://localhost:5170
- provides a web interface for users to interact with the application
- uses Razor pages for rendering views
- `/account/register` - user registration
- `/account/login` - user login


## Testing
Unit tests are implemented using XUnit framework.

Tests cover business layer services.

To run the tests, navigate to the test project directory and execute:

```bash
dotnet test
```

## Data Model:
An ER diagram illustrates data model in this application:

![ERdiagram](/assets/erd.png)

## Action Model
A use case diagram that illustrates actions for different actors that use the application

![UCdiaram](/assets/usecase.png)
