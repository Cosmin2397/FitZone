using FitZone.GymsManagement.Data;
using FitZone.GymsManagement.Entities;
using FitZone.GymsManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitZone.GymsManagement.Repositories
{
    public class GymRepository : IGymRepository
    {
        public readonly AppDbContext _context;
        public GymRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Gym> AddGym(Gym gym)
        {
            if (gym == null)
            {
                return null;
            }
            else
            {
                try
                {
                    await _context.Gyms.AddAsync(gym);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Eroare la salvarea datelor în baza de date: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare neașteptată: {ex.Message}");
                    throw;
                }
            }
            return gym;
        }

        public async Task<Gym> GetGymById(Guid id)
        {
            try
            {
                return await _context.Gyms.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în timpul extragerii datelor: {ex.Message}");
                throw;
            }
        }


        public async Task<List<Gym>> GetGyms()
        {
            try
            {
                return await _context.Gyms.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în timpul extragerii datelor: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> RemoveGym(Guid id)
        {
            var gym = await _context.Gyms.FirstOrDefaultAsync(a => a.Id == id);
            if (gym == null)
            {
                return false; // Nu s-a găsit sala de sport
            }

            try
            {
                _context.Gyms.Remove(gym);
                await _context.SaveChangesAsync();
                return true; // Ștergere reușită
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare neașteptată: {ex.Message}");
                return false; // Eșec la ștergere
            }
        }

        public async Task<Gym> UpdateGym(Gym gym, Guid id)
        {
            var dbGym = await _context.Gyms.FirstOrDefaultAsync(i => i.Id == id);
            if (dbGym != null && gym != null)
            {
                dbGym.Name = gym.Name;
                dbGym.Address = gym.Address;
                dbGym.City = gym.City;
                dbGym.Description = gym.Description;
                dbGym.PhoneNumber = gym.PhoneNumber;
                dbGym.ManagerId = gym.ManagerId;
                dbGym.Status = gym.Status;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymExists(id))
                {
                    throw;
                }
            }

            return gym;
        }

        private bool GymExists(Guid id)
        {
            return _context.Gyms.Any(e => e.Id == id);
        }
    }
}
