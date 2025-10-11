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
- Entity Framwork Core
- NUnit?
- SQLite
- some auth framework

## How to run

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
5. navigate to the WebAPI project directory:
```bash
cd WebAPI
```
6. Run the application:
```bash
dotnet run
```
7. Open your web browser and go to `https://localhost:5000/swagger` to access the Swagger UI and explore the API endpoints.
   

### Database Setup

The project uses Entity Framework Core for database management. To initialize the database, follow these steps:
1. Ensure you have the .NET SDK installed on your machine.
2. Set the `DefaultConnection` string in `appsettings.json` to point to your desired database location.
3. Open a terminal in the project directory and run the following commands to apply migrations and create the database:
   ```bash
   dotnet ef database update --project DataAccessLayer --startup-project WebAPI
   ```
4. After setting up the databasem the seeder will populate it with initial data automatically.



## Projet Structure
This solution consists of these projets (so far)

### DataAccessLayer
- communicates with database through Entity Framwork
- Entities directory contains classes that represent database tables
- Migrations directory contains EF migrations
- Context directory contains DbContext class that represents a session with the database and allows querying and saving data. It also contains seeder.

### WebAPI
- provides REST API endpoints
- Controllers directory stores endpoint definitions and logic

todo: Business Layer

todo: Infrastructure layer

todo: frontend

todo: tests

## Data Model:
An ER diagram illustrates data model in this application:
todo: upload a final version...
![ERdiagram](/assets/erd.png)

## Action Model
A use case diagram that illustrates actions for different actors that use the application

![UCdiaram](/assets/usecase.png)