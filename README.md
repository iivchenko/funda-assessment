# Funda Assessment
Funda Assessment is an implementation of .NET Software Engineer Technical Assignment task. [pdf](docs/assessment.pdf)

## High level design 
![High level design](docs/high-level-design.png)

## Prerequisites
* docker 
* docker-compose
* .NET Core SDK 3.1
* PowerShell

## How to run application without Visual Studio
Just run **_start.bat** script

## Releases
 * [v1.0](https://github.com/iivchenko/funda-assessment/releases/v1.0)
 * [v1.1](https://github.com/iivchenko/funda-assessment/releases/v1.1)
 * [v1.2](https://github.com/iivchenko/funda-assessment/releases/v1.2)
 * [v1.3](https://github.com/iivchenko/funda-assessment/releases/v1.3)
 
## Tech Stack
 * [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
 * [Docker](https://www.docker.com/)
 * [Cake Build](https://cakebuild.net/)
 * [Refit](https://github.com/reactiveui/refit) 
 * [NUnit](https://nunit.org/)
 * [MediatR](https://github.com/jbogard/MediatR)
 * [AutoMapper](https://automapper.org/)
 * [Redis](https://redis.io/)
 * [StackExchange Redis](https://github.com/StackExchange/StackExchange.Redis)
 * [Newtonsoft Json](https://www.newtonsoft.com/json)

## Principles:
 * SOLID
 * Clean Arhitecture
 * CQRS
 * Feature Folders
 * TDD

## Ideas
 * Extract service from MVC web
 * Add swagger to the service
 * Provide message queue and SignalR to update clients statistics 
 * Replace Statistics Seeder with UI to create custom statistics

# License

Funda Assessment is open source software, licensed under the terms of MIT license. 
See [LICENSE](LICENSE) for details.
