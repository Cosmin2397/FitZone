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
            try
            {
                // Verificăm dacă ID-ul este gol și generează unul nou dacă este necesar
                if (mealLog.Id == ObjectId.Empty)
                {
                    mealLog.Id = ObjectId.GenerateNewId();
                    Console.WriteLine($"S-a generat un nou ID: {mealLog.Id}");
                }
                else
                {
                    Console.WriteLine($"Se folosește ID-ul existent: {mealLog.Id}");
                }

                // Verifică dacă documentul există deja
                var filter = Builders<DailyClientMeals>.Filter.And(
                    Builders<DailyClientMeals>.Filter.Eq(m => m.ClientId, mealLog.ClientId),
                    Builders<DailyClientMeals>.Filter.Eq(m => m.Date, mealLog.Date)
                );

                var existingDocument = await _mealsCollection.Find(filter).FirstOrDefaultAsync();
                if (existingDocument != null)
                {
                    Console.WriteLine($"Document existent găsit cu ID: {existingDocument.Id}");

                    // Dacă documentul există, păstrează ID-ul original pentru a evita probleme
                    mealLog.Id = existingDocument.Id;

                    // Folosește un filtru bazat pe ID pentru a actualiza documentul existent
                    var idFilter = Builders<DailyClientMeals>.Filter.Eq(m => m.Id, existingDocument.Id);
                    var updateResult = await _mealsCollection.ReplaceOneAsync(idFilter, mealLog);

                    Console.WriteLine($"Document actualizat: {updateResult.IsAcknowledged}, ModifiedCount: {updateResult.ModifiedCount}");
                }
                else
                {
                    Console.WriteLine($"Nu s-a găsit document existent pentru ClientId: {mealLog.ClientId}, Date: {mealLog.Date}");

                    // Inserează un document nou
                    await _mealsCollection.InsertOneAsync(mealLog);

                    Console.WriteLine($"Document nou inserat cu ID: {mealLog.Id}");
                }

                // Verifică din nou dacă documentul a fost salvat corect
                var verifyDoc = await _mealsCollection.Find(filter).FirstOrDefaultAsync();
                if (verifyDoc != null)
                {
                    Console.WriteLine($"Verificare după upsert: Document găsit cu ID: {verifyDoc.Id}");
                }
                else
                {
                    Console.WriteLine("EROARE: Documentul nu a fost găsit după operațiunea de upsert!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în UpsertMealLogAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-aruncă excepția pentru a putea fi tratată în controller
            }
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
