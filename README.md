# TaskManagerAPI

TaskManagerAPI is a .NET 8 web API for managing tasks. It provides endpoints to create, update, delete, and list tasks, as well as retrieve tasks by their ID.

## Features

- Create a new task
- Update an existing task
- Delete a task
- List tasks with optional filters (status, expiration date)
- Retrieve a task by its ID

## Technologies

- .NET 8
- C# 12.0
- Entity Framework Core & Enitity Framework Core In-Memory Database
- xUnit (for testing)
- Swagger (for API documentation)

## Getting Started

### Prerequisites

- .NET 8 SDK

### Installation

1. Clone the repository:
```git clone https://github.com/yourusername/TaskManagerAPI.git```

2. Navigate to the project directory:
```cd TaskManagerAPI```

3. Restore the dependencies:
```dotnet restore```


### Running the Application

1. Build the project:
```dotnet build```

2. Run the project:
```dotnet run``` or press RUN into the Visual Studio IDE

The API will be available at `https://localhost:7050` or  `http://localhost:5014`.
A swagger page in https://localhost:7050/swagger/index.html should open automatically.

### Running Tests

To run the tests, use the following command:
```dotnet test``` or go to the **Test Explorer** and RUN the tests


## API Endpoints

### Create a Task

- **URL:** `POST /api/tasks`
- **Request Body:**
```
{
    "title": "Task Title",
    "description": "Task Description",
    "expiresIn": "2025-02-28T00:00:00Z",
    "status": "Pending"
}
```

- **Response:**
```
{
    "id": "task-id",
    "title": "Task Title",
    "description": "Task Description",
    "expiresIn": "2025-02-28T00:00:00Z",
    "status": "Pending"
}
```

### Update a Task

- **URL:** `PUT /api/tasks/{id}`
- **Request Body:**
```
{
    "title": "Updated Title",
    "description": "Updated Description",
    "expiresIn": "2025-03-01T00:00:00Z",
    "status": "InProgress"
}
```

### Delete a Task

- **URL:** `DELETE /api/tasks/{id}`

  
### List Tasks

- **URL:** `GET /api/tasks`
- **Query Parameters:**
    - `status` (optional): Filter by task status
    - `expiresIn` (optional): Filter by expiration date
- **Response:**
```
[
  {
    "id": "task-id",
    "title": "Task Title",
    "description": "Task Description",
    "expiresIn": "2025-02-28T00:00:00Z",
    "status": "Pending"
  }
]
```

### Get Task by ID

- **URL:** `GET /api/tasks/{id}`
- **Response:**
```
{
    "id": "task-id",
    "title": "Task Title",
    "description": "Task Description",
    "expiresIn": "2025-02-28T00:00:00Z",
    "status": "Pending"
}
```
    
