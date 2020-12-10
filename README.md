## Introduction

### The main goals of this project
- Demonstrating:

1. How to divide a system into Microservices, Api Gateways
2. How to structure projects in Visual Studio (division into components)
3. Implementation of Clean Architecture idea with CQRS (Reservation and Scheduling).
According to Clean Architecture book written by Robert C. Martin - "The purpose of a good architecture is to defer decisions, delay decisions. The job of an architect is not to make decisions, the job of an architect is to build a structure that allows decisions to be delayed as long as possible"
3. Implementation of Restful API according to
https://martinfowler.com/articles/richardsonMaturityModel.html.
5. Using Messaging in order to integrate applications asynchronously (RabbitMQ and MassTransit)
4. Strategic and tactical DDD according to Implementing Domain-Driven Design book written by Vaughn Vernon (Reservation and Scheduling).
5. SOLID and Clean Code rules
6. Example of tests:
- unit tests - Reservation and Catalog
- integration tests - Reservation and Catalog

### Short Description
This system is the example of a small reservation system which allows users to book a service.

### Used Technologies and Approaches:
- .NET CORE 3.1, EF CORE
- Restful Api
- OOP, SOLID, DI
- Microservices, Api Gateways
- CQRS, DDD
- MediatR
- RabbitMq, Masstransit
- Refit Client
- Outbox Pattern
- Xunit

### Business context

#### Subdomains:

Four subdomains were defined for this project. 
In the case of project for enterprise, many more sudomains would be defined such as Payment, Customer service, Communication.
Role of actor was simplified to "user".
In project for enterprise this role should be divided into at least two actors: user who can make an appointment and a user who can manage services and its schedule;

Implemented:

1. Catalog - a sphere of knowledge (definition of domain) refers to managing services in a catalog.
Users can manage services.
Users search for services in the service catalog.
Users can see details and offer of a service. 

2. Reservation - a sphere of knowledge refers to making and canceling service reservation.
Users search for reservations.
Users are able to cancel a reservation.

3. Scheduling - a sphere of knowledge refers to creating a daily schedule which contains of scheduled dates.
Users can manage a daily schedule.

Initialized, but not implemented:
4. Identity - allows users to authenticate and authorize.