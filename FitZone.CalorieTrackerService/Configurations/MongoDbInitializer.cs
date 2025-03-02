using FitZone.CalorieTrackerService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FitZone.CalorieTrackerService.Configurations
{
    public class MongoDbInitializer
    {
        private readonly IMongoDatabase _database;

        public MongoDbInitializer(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public void Initialize()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            // Crează colecția dacă nu există
            if (!_database.ListCollectionNames().ToList().Contains("DailyClientMeals"))
            {
                _database.CreateCollection("DailyClientMeals");
            }

            // Adaugă index pe ClientId și Date pentru performanță
            var mealCollection = _database.GetCollection<DailyClientMeals>("DailyClientMeals");
            var indexKeys = Builders<DailyClientMeals>.IndexKeys.Ascending(m => m.ClientId).Ascending(m => m.Date);
            var indexModel = new CreateIndexModel<DailyClientMeals>(indexKeys);
            mealCollection.Indexes.CreateOne(indexModel);
        }
    }

}
