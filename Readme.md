# TodoList – Technical Test for Beyond

This repository contains the solution to the technical test for a senior developer position at Beyond Hospitality Group.

## Contents

- Console application (.NET 8)
- Domain logic with validation
- REST API (Web API)
- Unit tests using MSTest
- Interactive documentation with Swagger
- Postman test collection.

## Project structure

- `TodoList.Domain`: models and business logic
- `TodoList.Runner`: console application
- `TodoList.Server`: Web API
- `TodoList.Test`: unit tests

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or later

## Frontend

This project includes a basic front-end built with HTML, CSS, and vanilla JavaScript located in the frontend/ folder.

- Communicates with the Web API asynchronously (no page reloads).
- Includes forms to add items and register progress.
- Displays todo items with visual progress bars, using different colors for each category (Work, Personal, Shopping, Urgent).
- The progress percentage is shown inside each bar.

To use it:

Start the API (TodoList.Server) at http://localhost:5100.

Open frontend/index.html in your browser.