# pv179_BookHub

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
- Postgres or SQLite?
- some auth framework

## How to run

TODO: 
- write a manual how initialize a db using entity framework
- other steps?
- how to start the application

## Projet Structure
This solution consists of these projets (so far)

### DataAccessLayer
- communicates with database through Entity Framwork
- Models directory contains classes that represent database tables
- todo: add seeder
- todo: add context

### WebAPI
- provides REST API endpoints for frontend
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