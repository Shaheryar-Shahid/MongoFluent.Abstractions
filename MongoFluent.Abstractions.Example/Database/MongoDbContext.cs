using MongoDB.Driver;

namespace AMService.Persistence.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        
        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }
        public IMongoCollection<T> GetCollection<T>()
        {
            var name = typeof(T).Name.ToLowerInvariant() + "s";
            return _database.GetCollection<T>(name);
        }
    }
}
