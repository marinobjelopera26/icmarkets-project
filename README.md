# ICMarkets Blockchain API

A production-ready ASP.NET Core 8.0 Web API solution for ingesting blockchain data from external providers. Currently works with BlockCypher API as an external provider of blockchain data.
Built with Clean Architecture and CQRS.

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED)](https://www.docker.com/)

## 📋 Table of Contents

- [ICMarkets Blockchain API](#icmarkets-blockchain-api)
  - [📋 Table of Contents](#-table-of-contents)
  - [🚀 Overview](#-overview)
  - [✨ Features](#-features)
    - [Core Functionalities](#core-functionalities)
    - [Architecture](#architecture)
    - [Design Patterns](#design-patterns)
  - [📦 Prerequisites](#-prerequisites)
    - [For Development (without Docker)](#for-development-without-docker)
    - [For Development (with Docker)](#for-development-with-docker)
  - [🚀 Getting Started](#-getting-started)
    - [Local Development with Docker Compose (Recommended)](#local-development-with-docker-compose-recommended)
      - [1. Clone Source Code](#1-clone-source-code)
      - [2. Open the Solution in IDE](#2-open-the-solution-in-ide)
      - [3. Configure Application Settings](#3-configure-application-settings)
      - [4. Run the Docker Compose project](#4-run-the-docker-compose-project)
      - [5. Access API Documentation](#5-access-api-documentation)
  - [📃 API Documentation](#-api-documentation)
    - [Endpoint Overview](#endpoint-overview)
    - [Supported Blockchains](#supported-blockchains)
    - [Endpoints Documentation](#endpoints-documentation)
      - [Ingest Blockchain Data](#ingest-blockchain-data)
      - [Fetch Blockchain History Data](#fetch-blockchain-history-data)
      - [Fetch Latest Blockchain Snapshot](#fetch-latest-blockchain-snapshot)
  - [💻 Background Services](#-background-services)
  - [⚙️ Configuration](#️-configuration)
    - [appsettings.json](#appsettingsjson)
  - [📁 Project Structure](#-project-structure)

---

## 🚀 Overview

ICMarkets Blockchain API is a RESTful service that provides:

- Blockchain data ingestion from external providers (BlockCypher API)
- Storage of historical blockchain data using Postgres database
- Retrieval of historic data for each supported blockchain
- Support for multiple blockchains: Bitcoin (BTC), Ethereum (ETH), Dash (DASH), and Litecoin (LTC)

**Tech Stack:**

- **.NET 8.0** - .NET SDK
- **Entity Framework Core 8.0.22** - Postgres database EFCore ORM
- **MediatR 13.1.0** - CQRS and mediator pattern implementation
- **FluentValidation 12.1.1** - User-input validation
- **Serilog 8.0.3** - Structured logging provider
- **Microsoft.Extensions.Http.Resilience 8.10.0** - Resilience and transient-fault-handling
- **NUnit + NSubstitute + Testcontainers** -  Test framework
- **Docker + Docker compose** - Development environment setup
- **GitHub Actions** - Continuous Integration

---

## ✨ Features

### Core Functionalities

- ✅ **Ingest blockchain data on-demand** from BlockCypher API (BTC, ETH, LTC, DASH)
- ✅ **Ingest blockchain data in the background** via data ingestion hosted service
- ✅ **Query historical data** for each supported blockchain

### Architecture

- 🏛️ **Clean Architecture** - Domain, Application, Infrastructure, Presentation layers
- 📬 **CQRS Pattern** - Command Query Responsibility Segregation with MediatR
- ✔️ **Input Validation** - FluentValidation MediatR pipeline behaviorts
- 🐳 **Docker Ready** - Multi-stage Dockerfile and docker-compose project configuration

---

### Design Patterns

- **Repository** - Abstraction of accessing data from backing database
- **Unit of Work** - Transaction abstraction
- **CQRS** - Separation of read and write operations with commands for writing and queries for reading
- **Mediator** - Decoupled command/query handling via `MediatR`
- **Resillience** - Retry mechanisms when communicating with external services

---

## 📦 Prerequisites

### For Development (without Docker)

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- An IDE of your choice (e.g. Visual Studio 2022, Visual Studio Code, JetBrains Rider)
- [Postgres](https://www.postgresql.org/) database server if you decide not to use the Docker infrastructure for development

### For Development (with Docker)

- [Docker Desktop](https://www.docker.com/)

---

## 🚀 Getting Started

### Local Development with Docker Compose (Recommended)

#### 1. Clone Source Code

```bash
git clone https://github.com/marinobjelopera26/icmarkets-project.git
cd icmarkets-project
```

#### 2. Open the Solution in IDE

In the IDE of your choosing open the solution file `ICM.Crypto.sln`.

#### 3. Configure Application Settings

The API uses `appsettings.json` for configuration. By default, it's pre-configured and ready to run
with the Docker Compose project configuration.  

In case you would prefer to use different configuration for local development, `appsettings.(Development.)json` file might require some tweaking.

#### 4. Run the Docker Compose project

Using the IDE of your choice (or Docker CLI), run the Docker Compose configuration.
This will:

- download all the required Docker images (e.g. Postgres image)
- build the Docker image of the web API project
- spin up all the required Docker containers

You may observe the process in Docker Desktop application.

#### 5. Access API Documentation

After all the necessary Docker containers are up and running, you may access the API documentation.
Unless you haven't changed the Docker container port configuration, you can do this by accessing the Swagger UI by navigating to `http://localhost:8080/swagger` in the browser of your choice.  

Through Swagger UI, one is able to ping all the available API endpoints and try them out.

---

## 📃 API Documentation

### Endpoint Overview

| Method | Endpoint | Description |
| -------|----------|-------------|
| GET    | `/health` | Liveness check |
| GET    | `/health/ready`| Readiness check |
| POST   | `/api/v1/blockchain/ingest` | Trigger blockchain data ingestion |
| GET    | `/api/v1/blockchain/history` | Get historic data for a blockchain |
| GET    | `/api/v1/blockchain/latest` | Get latest snapshot for a blockchain |

### Supported Blockchains

| Coins | Chains | Description |
|-------|---------|-------------|
| `btc` | `main`, `test3` | Bitcoin |
| `eth` | `main` | Ethereum |
| `dash` | `main` | Dash |
| `ltc` | `main` | Litecoin |

### Endpoints Documentation

#### Ingest Blockchain Data

Endpoint that performs blockchain data ingestion for blockchains provided via the request body.
Data ingestion can be performed for the following blockchains:  

1. `BtcMain` - Bitcoin main
2. `BtcTest3` - Bitcoin test3
3. `EthMain` - Ethereum main
4. `DashMain` - Dash main
5. `LtcMain` - Litecoin main
6. `All` - All of the above

It is also possible to provide multiple blockchain identifiers, separated by a comma.
Few examples of request bodies:

```json
// Ingest data for all supported blockchains
{
    "blockchains": "all"
}

// Ingest data for one specific blockchain
{
    "blockchains": "BtcMain"
}

// Ingest data for multiple specific blockchains
{
    "blockchains": "BtcMain, EthMain, LtcMain"
}
```

Example:

```bash
curl -X POST http://localhost:8080/api/v1/blockchain/ingest -d '{ "blockchains": "all" }'
```

---

#### Fetch Blockchain History Data

Endpoint that retrieves blockchain snapshot history data for a specific blockchain ordered by the creation date, descending. Blockchain is specified using the `coin` and `chain` query parameters, as shown in the example below. `page` and `pageSize` query parameters are used to control how many records are retrieved.

Example:

```bash
curl 'http://localhost:8080/api/v1/blockchain/history?coin=btc&chain=main&page=1&pageSize=10'
```

#### Fetch Latest Blockchain Snapshot

Endpoint that retrieves the latest stored blockchain snapshot for a specific blockchain. Blockchain is specified using the `coin` and `chain` query parameters, as shown in the example below.

Example:

```bash
curl 'http://localhost:8080/api/v1/blockchain/latest?coin=btc&chain=test3'
```

## 💻 Background Services

ICMarkets Blockchain API offers a possibility of enabling a background (hosted) service that will
ingest blockchain snapshot data automatically. Data ingestion background service is disabled by default. It can be enabled by setting `DataIngestion:Enabled` value to `true` in the `appsettings.json` file. Data ingestion is then executed every X seconds, where X is configurable through `appsettings.json` by changing the value of the `DataIngestion:PollingIntervalInSeconds` option.

---

## ⚙️ Configuration

### appsettings.json

```json
{
  "AllowedHosts": "*",
  "Cors": {
    "AllowedOrigins": [],
    "AllowedMethods": [
        "GET", "POST"
    ]
  },
  "ConnectionStrings": {
    "Default": ""
  },
  "DataIngestion": {
    "Enabled": false,
    "PollingIntervalInSeconds": 120
  }
}
```

## 📁 Project Structure

```text
icmarkets-project/
├── .config/
|   ├── dotnet-tools.json
├──.github/
|   ├── workflows/
|   |   ├── ICMCrypto-CI.yml     # GitHub action for Continuous Integration
├── src/
│   ├── ICM.Crypto.Application   # Application layer (Commands, Queries, Validation, Interfaces/Ports for infrastructure integration)
|   ├── ICM.Crpyto.Domain        # Domain layer (Aggregate root + value objects and domain exception)
|   ├── ICM.Crypto.Infrastructure.BlockCypher     # Integration with BlockCypher external API
|   ├── ICM.Crpyto.Infrastructure.HostedServices  # Blockchain data ingestion hosted service impl.
|   ├── ICM.Crypto.Infrastructure.Persistence     # Integration with Postgres database using EFCore (repositories, unit of work)
|   ├── ICM.Crypto.WebApi        # Composition root and the web host (app)
├── tests/
|   ├── ICMarkets.Crypto.UnitTests            # Contains unit tests
|   ├── ICMarkets.Crypto.FunctionalTests      # Contains functional tests
|   ├── ICMarkets.Crypto.IntegrationTests     # Contains integration tests
├── .dockerignore                           # Docker ignore patterns
├── .gitignore                              # Git ignore patterns
├── compose.yaml                            # Docker compose project configuration
├── Directory.Packages.props                # Central package management setup
├── global.json                             # Pinning of used .NET SDK version 
├── ICM.Crpyto.sln                          # Solution file
├── NuGet.Config                            # NuGet configurationn
└── README.md                               # Project README file
```
