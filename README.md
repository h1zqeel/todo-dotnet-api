# Todo .NET Web API

This .NET Web API application is designed to seamlessly integrate with the Vue.js application available at [https://github.com/h1zqeel/todo-app](https://github.com/h1zqeel/todo-app). It utilizes a MySQL database to store its data.

## Database Configuration

To configure the MySQL database, replace the connection string in the `appsettings.json` file with the appropriate credentials for your MySQL database.

Additionally, remember to update the JWT signing key, issuer, and audience as necessary for your environment.

## Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone this repository:

   ```sh
   git clone https://github.com/h1zqeel/todo-dotnet-api.git
   ```

2. Navigate to the project directory:

   ```sh
   cd todo-dotnet-api
   ```

3. Install dependencies:

   ```sh
   dotnet restore
   ```

### Running the Application

To run the .NET Web API application, use the following command:

```sh
dotnet run
```

The API will be accessible at `http://localhost:5063` by default.

## Usage

Once the application is running, the API endpoints will be available to interact with the Vue.js application.

That's it! You're ready to use the Todo .NET Web API with your Vue.js application.