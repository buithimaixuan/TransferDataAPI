# Requirement

![abec9232dd0f8635728ea3c491e261993f6abdd846778f54930736711bddc49a](https://github.com/lenhatquynh/TransferDataAPI/assets/84279345/06c0ce41-88cc-476f-b34c-fb50e101ef10)

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
