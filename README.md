# Hacker News Best Stories API

A RESTful API built with ASP.NET Core to efficiently retrieve the top `n` stories from Hacker News based on their score. The API integrates with the [Hacker News API](https://github.com/HackerNews/API) and includes caching to minimize external API calls.

---

## Features

- **Top Stories**: Retrieve the top `n` stories sorted by score in descending order.
- **Caching**: In-memory caching to reduce latency and avoid overloading the Hacker News API.
- **Efficient Serialization**: Uses .NET 6+ source-generated JSON serialization for performance.
- **Swagger Support**: Built-in API documentation via Swagger UI.
- **Unit Tests**: Test coverage for core service logic.

---

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- IDE (e.g., [Visual Studio 2022](https://visualstudio.microsoft.com/), [VS Code](https://code.visualstudio.com/))
- [Docker](https://www.docker.com/) (optional, for containerization)

---

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/hacker-news-api.git
   cd hacker-news-api

2. **Restore packages**:
   ```bash
   dotnet restore

3. **Run the application**:
```bash
	dotnet run
```

4. Access Swagger UI:
   - Open a browser and navigate to `https://localhost:5001/swagger/`.

API Endpoints:
- `GET /api/stories/{n}`: Retrieve the top `n` stories from Hacker News.

Example Request:
GET /api/stories/5 HTTP/1.1
Host: localhost:5001

Example Response:
```json
[
  {
	"id": 123,
	"title": "Example Story",
	"score": 100,
	"url": "https://example.com"
  },
  ...
]
```

---

Caching Strategy:

- The API caches the top stories for 5 minutes by default.
- The cache expiration time can be configured in `appsettings.json`.
- Sorted Stories: Pre-sroted list cached for 5 minutes.

Testing:

- Run unit tests using the following command:
```bash
dotnet test
```

Example Test Output:
Tet verify that:
* The service correctly fecthes and catches story ID's
* Story detalils are correctly fetched and serialized
* The API returns stories sorted by score

Deployment

Docker

1. Build the Docker image:
```bash	
docker build -t hacker-news-api .
```
2. Run the Docker container:
```bash
docker run -d -p 5000:80 hacker-news-api
```
 
Technologies Used:
* ASP.NET Core 8
* Scrutor: Service decoration
* xUnit: Unit testing
* Swagger: API documentation

Improvements:
* Retrieve the best stories in parallel batches for improved performance.
* Pagination support for large result sets.
* Rate limiting to prevent abuse.
* Background service to refresh the cache periodically.
* Optimized serialization for large data sets.
* Load testing to evaluate performance under heavy load.