# Alicunde technical test for candidates

## Description
This project was created to apply to the Alicunde vacancy as .NET backend developer.

## Add migration
```
dotnet ef migrations add --project src/Alicunde.System.Exam.EntityFrameworkCore/Alicunde.System.Exam.EntityFrameworkCore.csproj --startup-project src/Alicunde.System.Exam.Api/Alicunde.System.Exam.Api.csproj --context Alicunde.System.Exam.EntityFrameworkCore.DbContext.AlicundeSystemExamDbContext --configuration Debug Init --output-dir Migrations
```
## Remove last migration
```
dotnet ef migrations remove --project src/Alicunde.System.Exam.EntityFrameworkCore/Alicunde.System.Exam.EntityFrameworkCore.csproj --startup-project src/Alicunde.System.Exam.Api/Alicunde.System.Exam.Api.csproj --context Alicunde.System.Exam.EntityFrameworkCore.DbContext.AlicundeSystemExamDbContext --configuration Debug --force
```
## Database update
```
dotnet ef database update --project src/Alicunde.System.Exam.EntityFrameworkCore/Alicunde.System.Exam.EntityFrameworkCore.csproj --startup-project src/Alicunde.System.Exam.Api/Alicunde.System.Exam.Api.csproj --context Alicunde.System.Exam.EntityFrameworkCore.DbContext.AlicundeSystemExamDbContext --configuration Debug
```

## Authors
Alvaro Luis Padilla Moya