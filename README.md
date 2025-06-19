# Task Manager

This project can be considered the "Hello World" of the web development world: the Task Manager project. It’s a web API where you can create a user, log in, and start creating tasks associated with that user. It uses a username and password for user authentication and generates a JWT token to authorize access to protected routes.

### 🔧 Technologies 

- ASP.NET Core
- PostgreSQL
- Json Web Tokens
- Entity Framework


### 🏗️ Architecture

I followed a layered architecture with three layers: Controller layer, Application layer, and Persistence layer.

The Controller layer contains controllers mapped to specific routes. Each controller handles its own requests and performs proper request validation (when needed). After validation, the controller delegates the actual work to the Application layer.

The Application layer contains services and domain exceptions to handle all application logic. It applies the business rules for tasks like registering a user, creating a task, or updating it. It also interacts with the Persistence layer to perform data persistence tasks, such as saving a user or a newly created task.

The Persistence layer contains entities and repositories. All entities are Entity Framework entities, each representing a table in the database. The Application layer uses the repositories to interact with the data storage.

### 🚛 How to run it?

> Before cloning the repository, make sure you have docker and the docker compose plugin installed.

You just need to run `docker-compose up`.