# Todo Application

A Todo application built with React, TypeScript, and .NET 9. This application allows users to manage their tasks with features like task creation, completion tracking, due dates, and notes.

## Features

- Create, edit, and delete tasks (CRUD)
- Mark tasks as complete/incomplete
- Add notes to tasks
- Real-time updates
- Unit tests for backend services

## Tech Stack

### Frontend
- React 18
- TypeScript
- Material-UI (MUI)
- Axios for API calls
- React Router for navigation

### Backend
- .NET 9
- Entity Framework Core
- SQLite Database
- Swagger for API documentation
- RESTful API design
- xUnit for testing

## Project Structure

```
todo-app/
├── todo-frontend/          # React frontend application
│   ├── src/
│   │   ├── components/     # React components
│   │   ├── services/       # API service layer
│   │   └── App.tsx         # Main application component
│   └── package.json        # Frontend dependencies
│
└── TodoApi/                # .NET backend API
    ├── Controllers/        # API endpoints
    ├── Data/              # Database context and models
    ├── Services/          # Business logic
    └── Program.cs         # Application entry point
```

## Getting Started

### Prerequisites

- Node.js (v14 or higher)
- .NET 9 SDK
- Visual Studio 2022 or VS Code

### Backend Setup

1. Navigate to the API directory:
   ```bash
   cd TodoApi
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the API:
   ```bash
   dotnet run
   ```

4. Access Swagger documentation:
   - HTTP: http://localhost:5066/swagger

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd todo-frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

4. Open your browser and navigate to:
   ```
   http://localhost:3000
   ```

### Running Tests

1. First, create a solution file if it doesn't exist:
   ```bash
   dotnet new sln
   dotnet sln add TodoApi/TodoApi.csproj
   dotnet sln add TodoApi.Tests/TodoApi.Tests.csproj
   ```

2. Run all tests:
   ```bash
   dotnet test
   ```

3. Run tests with coverage:
   ```bash
   dotnet test /p:CollectCoverage=true
   ```

4. Run specific test project:
   ```bash
   dotnet test TodoApi.Tests/TodoApi.Tests.csproj
   ```

## API Endpoints

The API provides the following endpoints:

- `GET /api/todoitems` - Get all todo items
- `GET /api/todoitems/{id}` - Get a specific todo item
- `POST /api/todoitems` - Create a new todo item
- `PUT /api/todoitems/{id}` - Update an existing todo item
- `DELETE /api/todoitems/{id}` - Delete a todo item

## Screenshot
![image](https://github.com/user-attachments/assets/190b6553-5adc-41dd-929f-e24e577a5fd1)
