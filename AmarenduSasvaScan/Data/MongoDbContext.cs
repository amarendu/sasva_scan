using Microsoft.Extensions.Options; 
using MongoDB.Driver; 
using System; 
namespace AmarenduSasvaScan.Data 
{ 
    public class MongoDbContext 
    { 
        private readonly IMongoDatabase _database; 
        public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings) 
        { 
            try 
            { 
                var client = new MongoClient(mongoDbSettings.Value.ConnectionString); 
                _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName); 
                Console.WriteLine("MongoDB connection established successfully."); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
                throw; 
            } 
        } 
        public IMongoCollection<T> GetCollection<T>(string name) 
        { 
            try 
            { 
                Console.WriteLine($"Retrieving collection: {name}"); 
                return _database.GetCollection<T>(name); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error retrieving collection {name}: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
                throw; 
            } 
        } 
    } 
    public class MongoDbSettings 
    { 
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; } 
    } 
} 