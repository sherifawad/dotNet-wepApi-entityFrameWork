This is a webApi project using .Net8 and EntityFramework.

## Getting Started

First, Setup Database:

- Run docker Compose
- Add service and connection string

For MSSQl-SERVER

```bash
docker compose -f "docker-compose.yml" up -d --build

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost; Database=employee;User Id=SA;Password=msSQL@123;MultipleActiveResultSets=true;TrustServerCertificate=true;"
},

```

For POSTGRES

```bash
docker compose -f "postgres-docker-compose.yml" up -d --build

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

	"ConnectionStrings": {
		"DefaultConnection": "Server=localhost; Database=employee;User Id=SA;Password=msSQL@123;Port=5432"
	},
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

Publish Project

```bash
dotnet publish --property:PublishDir=publish
```

## Features

- Api versioning
- Easy data View and sort
- Can be used either by MYSQl or Postgres after adding date time conversion
- Implementing Open Data protocol
- Use generic expressions: doesn't work with swagger, try with some other options like postman

```bash

http://localhost:5005/api/Employee/GetAllFilters?filter={"filters":[{"field":"Name","operator":"contains","value":"sherif"},{"operator":"eq","value":"1","field":"code"}],"logic":"or"}
```
