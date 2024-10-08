# 📝 Note App
> *This application is my project made in the process of learning .NET and Angular*

## ℹ️ Overview

Note App is a simple CRUD (Create-Read-Update-Delete) web application designed to help you keep your notes organized. Built with Angular 18.1.2, .NET 8, and PostgreSQL, it offers a seamless and secure experience for managing personal notes.

## 🌟 Key features

- User Authentication: Secure login and registration using JWT Bearer authentication.
- Personalized Notes: Each user has a private space to manage their notes.
- Sorting and Searching: Easily sort notes by date or title, and use the search function to find specific notes.
- CRUD Operations: Create, read, update, and delete notes effortlessly.
- The backend was tested using integration tests

## 🏗️ Patterns
 - Clean Architecture - way to structure your code and to separate the concerns of the application into layers. The main idea is to separate the business logic from the infrastructure and presentation layers. This way, the core domain business logic and rules are at the core layer of the application, external services are implemented at the infrastructure layer, and the presentation layer is the entry point of the application.
- Repository - in C# mediates between the domain and the data mapping layers using a collection-like interface for accessing the domain objects. Repository Design Pattern separates the data access logic and maps it to the entities in the business logic. It works with the domain entities and performs data access logic. In the Repository pattern, the domain entities, the data access logic, and the business logic talk to each other using interfaces. It hides the details of data access from the business logic.
- CQRS - CQRS stands for Command and Query Responsibility Segregation, a pattern that separates read and update operations for a data store. Implementing CQRS in your application can maximize its performance, scalability, and security. The flexibility created by migrating to CQRS allows a system to better evolve over time and prevents update commands from causing merge conflicts at the domain level.
  
## 🧰 Used NuGet Addons:
- Microsoft.EntityFrameworkCore v8.0.7: Database context management
- Npgsql.EntityFrameworkCore.PostgreSQL v8.0.4: PostgreSQL provider
- Microsoft.EntityFrameworkCore.Design v8.0.7: Migration support
- Microsoft.AspNetCore.Identity.EntityFrameworkCore v8.0.7: Identity management
- Microsoft.IdentityModel.Tokens v8.0.1: Token handling
- Microsoft.IdentityModel.JsonWebTokens v8.0.1: JWT support
- Microsoft.AspNetCore.Authentication.JwtBearer v8.0.7: JWT Bearer authentication
- Microsoft.Extensions.Identity.Stores v8.0.7: Identity stores
- xunit
- MediatR
- Microsoft.AspNetCore.Mvc.Testing
  
## 🖼️ Screenshots
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Login.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Register.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Home.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Notes.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Edit.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Details.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/Account.png)
![image alt](https://github.com/dkbanas/Note-App/blob/086a14d01b877de3d926b98fa24782ae2f44d851/Screenshots/About.png)

## 🛠️ How to run
- 1.Clone repository
- 2.Create NoteDB and Note_DB database in pgadmin4 and import sql files from Infrastructure folder.
- 3.Open appsettings.Development.json file inside API folder and fill username and password for database
- 4.Click run in your IDE to run backend.
- 5.Right click in the Client folder, open in terminal and write
```CMD
>>>PS D:\Note\Client> ng serve
```


