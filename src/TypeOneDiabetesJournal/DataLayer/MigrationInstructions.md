Add Migration

dotnet ef migrations add InitialCreate --project Migrations

dotnet ef database update --project Migrations --connection 'connectionString'