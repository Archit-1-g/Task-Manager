# Task Management System – Oritso Assignment

## 2.1. Overview

A full-stack task management system built using:
- **Backend**: .NET Core Web API
- **Frontend**: Angular 10.2.4
- **Database**: PostgreSQL

This application allows users to sign up, log in, and manage their tasks through a clean UI, supporting creation, editing, searching, and status updates of tasks.

---

## 2.1.2. Features

-  (Login/Signup)
- Task CRUD Operations (Create, Read, Update, Delete)
- Search Tasks and Get Task By ID
- Responsive, mobile-friendly UI
- Task status tracking and remarks
- Timestamp tracking for creation and last updates

---

## 2.1.3. Explanation of DB Design

### 2.1.3.2.1. ER Diagram

Task-Manager
│
├── Task-Manger-Web
│   └── src
│       └── assets
│           └── Er-diagram.png



### 2.1.3.2.2. Data Dictionary

| Field           | Type      | Description                         |
|----------------|-----------|-------------------------------------|
| Id             | int       | Primary Key                         |
| Title          | string    | Task title                          |
| Description    | string    | Task description                    |
| DueDate        | datetime  | Task due date                       |
| Status         | string    | Task status (Pending, Completed...) |
| Remarks        | string    | Additional task notes               |
| CreatedOn      | datetime  | Task creation timestamp (UTC)       |
| LastUpdatedOn  | datetime  | Last updated timestamp (UTC)        |
| CreatedById    | int       | Creator's user ID                   |
| CreatedByName  | string    | Creator's name                      |
| UpdatedById    | int       | Last editor's user ID               |
| UpdatedByName  | string    | Last editor's name                  |

### 2.1.3.2.3. Indexes Used

- Primary Key on `Id`
- Indexes on `Status`, `CreatedById` for performance on filters

### 2.1.3.2.4. Code First or DB First?

**Approach**: Code First

**Reason**: Code-first makes it easier to evolve the schema directly from the model classes and keep migrations consistent with the application.

---

## 2.1.3.3. Structure of the Application

### 2.1.3.3.1. SPA or MVC?

**SPA (Single Page Application)** using Angular 10.2.4 was chosen. Angular interacts with the Web API to create a seamless experience without full page reloads.

### 2.1.3.3.2. Frontend Structure

```
/frontend
  /src
    /app
      /auth
        - login.component.ts
        - signup.component.ts
      /task
        - task-list.component.ts
        - task-create.component.ts
        - task-edit.component.ts
        - task-details.component.ts
      /shared
        - header.component.ts
        - footer.component.ts
    /assets
      - styles
      - icons
  /environments
    - environment.ts
```

---

## 2.1.3.4. Frontend Details

### 2.1.3.4.1. Frontend Choice

**Chosen**: Angular Web SPA

**Reason**:
- Rich component-based architecture
- Efficient API consumption using HttpClient
- Easy routing and form validation

### 2.1.3.4.2. Web or Mobile

**Web Frontend** was selected for this assignment as it is easily accessible from any browser and suitable for task-based operations.

---

## 2.1.3.5. Build and Install Instructions

### 2.1.3.5.1. Environment & Dependencies

**Backend:**
- .NET 6 SDK
- Entity Framework Core
- Npgsql PostgreSQL provider

**Frontend:**
- Angular CLI 10.2.4
- Bootstrap 4
- RxJS, Angular Router, HttpClient

**Database:**
- PostgreSQL 14+

### 2.1.3.5.2. Build Instructions

**Backend:**
```bash
cd backend
 dotnet build
```

**Frontend:**
```bash
cd frontend
npm install
ng build
```

### 2.1.3.5.3. Run Instructions

**Backend:**
```bash
cd backend
dotnet ef database update
dotnet run
```
App will run at: `https://localhost:7100`

**Frontend:**
```bash
cd frontend
ng serve
```
Frontend runs at: `http://localhost:4200`

---


