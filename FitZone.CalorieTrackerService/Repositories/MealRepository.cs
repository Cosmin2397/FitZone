namespace FitZone.CalorieTrackerService.Repositories
{
    using FitZone.CalorieTrackerService.Configurations;
    using FitZone.CalorieTrackerService.Models;
    using FitZone.CalorieTrackerService.Repositories.Interfaces;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MealRepository: IMealRepository
    {
        private readonly IMongoCollection<DailyClientMeals> _mealsCollection;

        public MealRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _mealsCollection = database.GetCollection<DailyClientMeals>(settings.Value.MealsCollection);
        }

        //  Caută mesele unui client pentru o zi specifică
        public async Task<DailyClientMeals> GetMealsByClientAndDateAsync(Guid clientId, string date)
        {
            var filter = Builders<DailyClientMeals>.Filter.And(
                Builders<DailyClientMeals>.Filter.Eq(m => m.ClientId, clientId),
                Builders<DailyClientMeals>.Filter.Eq(m => m.Date, date)
            );

            return await _mealsCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddMealToLogAsync(Guid clientId, string date, Meal newMeal)
        {
            var filter = Builders<DailyClientMeals>.Filter.And(
                Builders<DailyClientMeals>.Filter.Eq(m => m.ClientId, clientId),
                Builders<DailyClientMeals>.Filter.Eq(m => m.Date, date)
            );

            var update = Builders<DailyClientMeals>.Update.Push(m => m.Meals, newMeal);

            var options = new UpdateOptions { IsUpsert = true };

            await _mealsCollection.UpdateOneAsync(filter, update, options);
        }


        public async Task UpsertMealLogAsync(DailyClientMeals mealLog)
        {
            if (mealLog.Id == ObjectId.Empty)
            {
                mealLog.Id = ObjectId.GenerateNewId();
            }

            var filter = Builders<DailyClientMeals>.Filter.And(
                Builders<DailyClientMeals>.Filter.Eq(m => m.ClientId, mealLog.ClientId),
                Builders<DailyClientMeals>.Filter.Eq(m => m.Date, mealLog.Date)
            );

            // Folosește ReplaceOneAsync pentru a actualiza sau insera documentul
            await _mealsCollection.ReplaceOneAsync(filter, mealLog, new ReplaceOptions { IsUpsert = true });
        }


        // Șterge un meniu al unui client pentru o anumită zi
        public async Task DeleteMealLogAsync(Guid clientId, string date)
        {
            var filter = Builders<DailyClientMeals>.Filter.And(
                Builders<DailyClientMeals>.Filter.Eq(m => m.ClientId, clientId),
                Builders<DailyClientMeals>.Filter.Eq(m => m.Date, date)
            );

            await _mealsCollection.DeleteOneAsync(filter);
        }
    }

}
