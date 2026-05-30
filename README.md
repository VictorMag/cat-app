# CatEncyclopedia

A full-stack cat breed explorer built with ASP.NET Core 9 and Angular 21. The backend acts as a proxy to [The Cat API](https://thecatapi.com), keeping the API key server-side and exposing a clean REST interface to the frontend.

## Architecture

```
Angular (localhost:4200)
        │
        ▼
ASP.NET Core Web API (localhost:5223)   ← proxy / Backend for Frontend
        │
        ▼
The Cat API (api.thecatapi.com/v1)
```

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org)

## Setup

Before running the API, configure the API key using .NET User Secrets so it never touches the repo:

```bash
cd CatApi
dotnet user-secrets init
dotnet user-secrets set "TheCatApi:ApiKey" "your_api_key_here"
```

## Quick Start

**1. Start the API**

```bash
cd CatApi
dotnet run
```

API available at `http://localhost:5223`

**2. Start the frontend**

```bash
cd cat-frontend
npm install
npx ng serve
```

App available at `http://localhost:4200`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/cats/breeds` | Returns all available cat breeds |
| GET | `/api/cats/images?breedId={id}&limit={n}` | Returns images filtered by breed and count |

## Project Structure

```
├── CatApi/                  # ASP.NET Core 9 Web API
│   ├── Controllers/
│   │   └── CatsController.cs
│   ├── Models/
│   │   ├── BreedDto.cs
│   │   └── CatImageDto.cs
│   ├── Services/
│   │   └── TheCatApiClient.cs
│   └── Program.cs
│
└── cat-frontend/            # Angular 21 SPA
    └── src/app/
        ├── models/
        │   ├── breed.model.ts
        │   └── cat-image.model.ts
        ├── services/
        │   └── cat.service.ts
        ├── app.ts
        ├── app.html
        └── app.scss
```

## Configuration

The Cat API key and base URL are stored in `CatApi/appsettings.json` under the `TheCatApi` section. In production, override these values with environment variables:

```
TheCatApi__BaseUrl=https://api.thecatapi.com/v1/
TheCatApi__ApiKey=your_api_key
```
