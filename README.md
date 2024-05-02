# Project Overview: TaskTrac
TaskTrac is a task management system designed to streamline task organization and collaboration. 

It enables users to create tasks and add subtasks to effectively manage their projects.

For ease of use, users can create a task and also add subtasks to the newly created task without leaving the page. These subtasks can also be edited.

To ensure data privacy, users can only view and manage their own tasks. Additionally, to enhance security, users are automatically logged out after a period of inactivity.

TaskTrac follows the N-tier architecture, which is

•	**Secure:** You can secure each tier separately using different methods.

•	**Easy to manage:** You can manage each tier separately, adding or modifying each tier without affecting the other tiers.

•	**Scalable:** If you need to add more resources, you can do it per tier, without affecting the other tiers.

•	**Flexible:** Apart from isolated scalability, you can also expand each tier in any manner that your requirements dictate.

### Setup:

To set up TaskTrac:
If **(LocalDb)\MSSQLLocalDB** is installed, run the following commands in the Package Manager Console:
   
•	**add-migration Initial**

•	**update-database**

Alternatively, 

you can manually change the connection string in the **appsettings.json** file in the TaskTrac.API project and the **AppDbContext.cs** file in the TaskTrac.DAL project (in the Data folder).
Then, run the same commands in the Package Manager Console.

These commands will create and update the necessary migrations to set up the database, including tables for tasks, subtasks, and ASP.NET Identity.

## Architecture:
TaskTrac follows the N-tier architecture, which is divided into different layers:

### Data Access Layer (DAL):

•	**Models:** Define the structure of data entities, including tasks and subtasks.

•	**Repositories:** Handle data access operations and interact with the database.

•	**Interfaces:** Define contracts for interacting with data repositories.

•	**Data:** Contains migrations and database configuration using the Code First approach.

### Business Logic Layer (BLL):

•	**Interfaces:** Define rules and operations for tasks and subtasks.

•	**Services:** Implement business logic operations, such as task creation, updating, and retrieval.

### API Layer:
•	**Controllers:** Provide endpoints for client-server communication.

•	**DTO (Data Transfer Objects):** Define data structures for transferring data between layers and clients.

•	**Interfaces (IJWTService):** Define contracts for generating JWT tokens.

•	**Services:** Implement the JWT token generation service for authentication.

### Testing Layer:
•	**TasksTests.cs:** Contains tests for both positive and negative scenarios of task-related methods.

•	**SubTasksTests.cs:** Includes tests for both positive and negative scenarios of subtask-related methods.

### Presentation Layer:
•	**Controllers:** Handle user requests and responses.

•	**Views:** Render HTML pages for user interaction.

•	**Login:** Pages for authentication.

•	**Login.cshtml:** Login form.

•	**Register.cshtml:** Registration form.

•	**Tasks:** Pages for managing tasks.

•	**Tasks.cshtml:** List of tasks.

•	**CreateTask.cshtml:** Form for creating tasks.

•	**UpdateTask.cshtml:** Form for updating tasks.

### Authentication and Authorization:

TaskTrac utilizes JWT tokens and Microsoft ASP.NET Core Identity for secure authentication. This ensures that users can only access their own tasks and helps maintain data privacy.

