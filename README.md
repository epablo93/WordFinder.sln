# WordFinder Solution

## Overview
This solution implements the WordFinder challenge using modern .NET 8 best practices, focusing on high performance, maintainability, and clean architecture. The application is structured using Onion Architecture, with clear separation of concerns between core logic, business services, validation, API, and models.

## Architecture & Patterns

### Onion Architecture
- **Core Layer**: Contains interfaces and result DTOs (e.g., `IWordFinderService`, `WordFinderResult`). This layer is independent of any infrastructure or framework code.
- **Services Layer**: Contains business logic and validation (e.g., `WordFinderService`, `MatrixInputValidator`, `WordStreamValidator`). Services depend only on abstractions from the Core layer.
- **Models Layer**: Contains request/response DTOs for the API (e.g., `WordFinderRequest`).
- **Controllers Layer**: Contains API endpoints (e.g., `WordFinderController`). Controllers depend on services via dependency injection.
- **Root**: Contains the core algorithm class (`WordFinder`), which is pure and free of dependencies.

### Patterns & Practices
- **Dependency Injection**: All services and validators are injected, promoting testability and loose coupling.
- **FluentValidation**: Used for robust, declarative input validation, ensuring that only valid data reaches the business logic.
- **DTOs**: Used for clear API contracts and separation between transport and domain models.
- **Result Pattern**: Service methods return a result object (`WordFinderResult`) containing success status, data, and errors, rather than throwing exceptions for validation issues.
- **Single Responsibility Principle**: Each class/file has a single responsibility, making the codebase easy to maintain and extend.
- **Unit Testing**: The architecture is designed for easy unit testing of all layers.

## Performance Considerations
- **Efficient Data Structures**: The core `WordFinder` uses `ConcurrentDictionary` and `HashSet` for fast lookups and thread-safe counting.
- **Parallel Processing**: The search for words in the matrix is parallelized using `AsParallel().ForAll`, leveraging multi-core CPUs for large word streams.
- **Preprocessing**: The matrix is preprocessed into vertical strings for O(1) vertical search, avoiding repeated computation.
- **Minimal Allocations**: The algorithm avoids unnecessary allocations and uses efficient LINQ queries for result selection.
- **No Redundant Validation**: Validation is performed once, before the core logic, ensuring the algorithm only works with valid data.

## Why This Is Performative
- **Parallelism**: By parallelizing the search for each unique word, the solution scales with available CPU cores, making it suitable for large word streams.
- **Optimized Search**: Preprocessing the matrix for vertical search and using efficient string operations ensures that each word is checked in O(N) time, where N is the matrix dimension.
- **Thread Safety**: Use of `ConcurrentDictionary` allows safe concurrent updates without locking.
- **Top-N Selection**: The algorithm only keeps the top 10 most frequent words, reducing memory and computation overhead.

## How to Run
1. Build and run the solution with .NET 8.
2. Use the `/WordFinder/find` API endpoint to POST a matrix and word stream.
3. The response will contain the top 10 most repeated words found in the matrix, or validation errors if the input is invalid.

### Example Request Body
To test the API right away in Swagger or with a tool like Postman, use the following JSON body:
{
  "matrix": [
    "coldw",
    "ionhi",
    "lwnoc",
    "dhsni",
    "chill"
  ],
  "wordStream": ["cold", "wind", "snow", "chill", "cold", "chill", "wind", "wind"]
}

