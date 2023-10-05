# Run Docker to host SQL Server

```sh
docker compose build
```

```sh
docker compose up
```

# TransferData

## At ServerA:

```sh
dotnet build
```

```sh
dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=ServerA;Persist Security Info=True;User ID=sa;Password=123456Sa;"
```

## At ServerB:

```sh
dotnet build
```

```sh
dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=ServerB;Persist Security Info=True;User ID=sa;Password=123456Sa;"
```

_Then run the solution_

# WorkerService

After run TransferData project successfully then cd into WorkerService and run:

```sh
node worker.js
```
