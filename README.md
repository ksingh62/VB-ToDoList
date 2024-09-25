# To-Do List Application

## Overview

This is a simple To-Do List application developed in VB.NET with a Microsoft Access database for storing tasks. The application allows users to add, update, delete, and mark tasks as done. It demonstrates basic CRUD operations and interaction with an Access database.

## Features

- **Add New Tasks**: Users can add new tasks to the list.
- **Update Tasks**: Tasks can be updated with new descriptions.
- **Delete Tasks**: Users can remove tasks from the list.
- **Mark Tasks as Done**: Tasks can be marked as completed with a checkbox.
- **Data Persistence**: Tasks and their completion status are stored in an MS Access database.

## Technologies Used

- **VB.NET**: Programming language used to build the application.
- **Microsoft Access**: Database used for storing tasks.
- **OleDb**: Data provider used for interacting with the Access database.

## Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/todolist.git
   
2. Open the Project

Open the `ToDoList.sln` file in Visual Studio.

3. Set Up the Database

Ensure you have Microsoft Access installed. The database file `ToDoList.accdb` should be placed in the `bin\Debug` directory of the project. You can adjust the file path in the connection string if needed.

4. Build and Run

Build the project in Visual Studio. Run the application to start managing your tasks.

## Usage

- **Add a Task**: Enter the task description in the input box and click "Add Task".
- **Update a Task**: Click on a task to select it. Modify the task description in the input box and click "Update Task" to save changes.
- **Delete a Task**: Select the task you want to delete and click "Delete Task" to remove it.
- **Mark as Done**: Check the checkbox next to a task to mark it as done.
- **Clear All Tasks**: Click "Clear All Tasks" to remove all tasks from the list.

## Connection String

Ensure the connection string in `Form1.vb` correctly points to the `ToDoList.accdb` file:

```vb
Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=|DataDirectory|\ToDoList.accdb;Persist Security Info=False;"
