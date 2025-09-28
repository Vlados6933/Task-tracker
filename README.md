# Task Tracker CLI

A simple **command-line task tracker** written in C# that allows you to add, update, delete, and list tasks. Tasks are stored in a JSON file and include metadata such as ID, description, status, creation date, and last update date.

---

## Features

* Add, update, and delete tasks
* Mark tasks as `todo`, `in-progress`, or `done`
* List all tasks or filter by status
* Persistent storage in `user.json`
* Basic input validation

---

## Installation & Run

```bash
# Clone repository
git clone https://github.com/Vlados6933/Task-tracker.git
cd Task-tracker

# Run project
dotnet run --project Task-tracker
```

---

## Commands

All commands must start with `task-cli`.

```bash
# Add a new task
task-cli add "Buy groceries"

# Update a task
task-cli update 1 "Buy groceries and cook dinner"

# Delete a task
task-cli delete 1

# Mark tasks
task-cli mark-in-progress 1
task-cli mark-done 1

# List all tasks
task-cli list

# List tasks by status
task-cli list done
task-cli list todo
task-cli list in-progress
```

---

## Data Format (JSON Example)

```json
{
  "Id": 1,
  "Description": "Buy groceries",
  "Status": "todo",
  "CreatedAt": "2025-09-26T18:35:22",
  "UpdatedAt": "2025-09-26T18:35:22"
}
```

---

## Code Overview

* **Models/TodoModel.cs** – Task structure with ID, description, status, timestamps
* **Models/Status.cs** – Enum (`todo`, `inProgress`, `done`)
* **Services/FileIOService.cs** – Save/load tasks from JSON file
* **Services/StringParser.cs** – Parses CLI input, validates commands, extracts arguments
* **Services/CommandHandler.cs** – Executes task operations based on parsed commands

---

## Contribution

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

## License

MIT License
