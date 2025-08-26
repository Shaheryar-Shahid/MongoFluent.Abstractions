# MongoFluent.Abstractions

MongoFluent.Abstractions provides a .NET-friendly abstraction layer over MongoDB.Driver, enabling fluent query, projection, sorting, and repository patterns for MongoDB operations. It is designed to simplify and standardize MongoDB access in .NET 8 applications.

## Features
- Fluent query, projection, and sorting interfaces
- Repository pattern for MongoDB collections
- Tenant filtering support
- Easy integration with MongoDB.Driver

## Getting Started
Add the NuGet package to your project:
```shell
# .NET CLI
 dotnet add package MongoFluent.Abstractions
```

## Usage Example
Below is a basic example of how to use the abstractions to interact with a MongoDB collection:

```csharp
// Setup MongoDB context
var client = new MongoClient("mongodb://localhost:27017");
var context = new MongoDbContext(client, "schoolDb");

// Create repository for Student model
var studentRepo = new MongoRepository<Student>(context);

// Insert a student
await studentRepo.InsertOneAsync(new Student { Id = 1, Name = "Alice" });

// Query students by name
var students = await studentRepo.GetAsync(q => q.Eq(s => s.Name, "Alice"));

// Update a student
await studentRepo.UpdateOneAsync(q => q.Eq(s => s.Id, 1), new Dictionary<string, object> { ["Name"] = "Bob" });

// Delete a student
await studentRepo.DeleteOneAsync(q => q.Eq(s => s.Id, 1));
```
## Example Implementation
A complete example implementation is available in the dedicated `MongoFluent.Abstractions.Examples` project.  
This project includes:
- Example entity (`Model/Student.cs`)
- Repository implementation (`Repository/MongoRepository.cs`)
- MongoDB context setup (`Database/MongoDbContext.cs`)
- Repository interface (`Interfaces/Repository/IRepository.cs`)

Refer to the `MongoFluent.Abstractions.Examples` project for usage and integration details.


## License
MIT