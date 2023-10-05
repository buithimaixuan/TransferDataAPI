# TransferData

## At ServerA:

- dotnet build
- dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=ServerA;Persist Security Info=True;User ID=sa;Password=123456Sa;"

## At ServerB:

- dotnet build
- dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=ServerB;Persist Security Info=True;User ID=sa;Password=123456Sa;"

Then run the solution

# WorkerService

After run TransferData project successfully then cd into WorkerService and run:

- node worker.js
