# 📝 Note App
> *This application is my project made in the process of learning .NET and Angular*

## ℹ️ Overview

Note App is a simple CRUD (Create-Read-Update-Delete) web application designed to help you keep your notes organized. Built with Angular 18.1.2, .NET 8, and PostgreSQL, it offers a seamless and secure experience for managing personal notes.

## 🌟 Key features

- User Authentication: Secure login and registration using JWT Bearer authentication.
- Personalized Notes: Each user has a private space to manage their notes.
- Sorting and Searching: Easily sort notes by date or title, and use the search function to find specific notes.
- CRUD Operations: Create, read, update, and delete notes effortlessly.

## 🏗️ Patterns
 - Clean Architecture - way to structure your code and to separate the concerns of the application into layers. The main idea is to separate the business logic from the infrastructure and presentation layers. This way, the core domain business logic and rules are at the core layer of the application, external services are implemented at the infrastructure layer, and the presentation layer is the entry point of the application.
- Repository - in C# mediates between the domain and the data mapping layers using a collection-like interface for accessing the domain objects. Repository Design Pattern separates the data access logic and maps it to the entities in the business logic. It works with the domain entities and performs data access logic. In the Repository pattern, the domain entities, the data access logic, and the business logic talk to each other using interfaces. It hides the details of data access from the business logic.

## 🧰 Used NuGet Addons:
- Microsoft.EntityFrameworkCore v8.0.7: Database context management
- Npgsql.EntityFrameworkCore.PostgreSQL v8.0.4: PostgreSQL provider
- Microsoft.EntityFrameworkCore.Design v8.0.7: Migration support
- Microsoft.AspNetCore.Identity.EntityFrameworkCore v8.0.7: Identity management
- Microsoft.IdentityModel.Tokens v8.0.1: Token handling
- Microsoft.IdentityModel.JsonWebTokens v8.0.1: JWT support
- Microsoft.AspNetCore.Authentication.JwtBearer v8.0.7: JWT Bearer authentication
- Microsoft.Extensions.Identity.Stores v8.0.7: Identity stores


## 🛠️ How to run
- 1.Clone repository
- 2.Create NoteDB and Note_DB database in pgadmin4 and import sql files from Infrastructure folder.
- 3.Click run in your IDE to run backend.
- 4.Right click in the Client folder, open in terminal and write
```CMD
>>>PS D:\Note\Client> ng serve
```
