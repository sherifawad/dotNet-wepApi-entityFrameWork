This is a webApi project using .Net8 and EntityFramework.

## Getting Started

First, run the docker-compose:

```bash
docker compose -f "docker-compose.yml" up -d --build
```

Second, Create DataBase

```bash
dotnet ef database update
```

Third, only Run it

```bash
dotnet run
```

Finally, Access using Swagger

```bash
http://localhost:5005/swagger/index.html
```
