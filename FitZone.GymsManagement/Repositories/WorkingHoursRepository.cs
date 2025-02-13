using FitZone.GymsManagement.Data;
using FitZone.GymsManagement.Entities;
using FitZone.GymsManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitZone.GymsManagement.Repositories
{
    public class WorkingHoursRepository :  IWorkingHoursRepository
    {
        public readonly AppDbContext _context;
        public WorkingHoursRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BusinessHours> AddWorkingHours(BusinessHours businessHours)
        {
            if (businessHours == null)
            {
                return null;
            }
            else
            {
                try
                {
                    await _context.GymBusinessHours.AddAsync(businessHours);
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
            return businessHours;
        }

        public async Task<BusinessHours> GetWorkingHoursByDay(DayOfWeek day)
        {
            try
            {
                return await _context.GymBusinessHours.FirstOrDefaultAsync(c => c.DayOfWeek == day);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în timpul extragerii datelor: {ex.Message}");
                throw;
            }
        }


        public async Task<List<BusinessHours>> GetGymWorkingHours(Guid gymId)
        {
            try
            {
                return await _context.GymBusinessHours.Where(z => z.GymId == gymId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în timpul extragerii datelor: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveWorkingHours(Guid id)
        {
            var weekBusinessHours = await _context.GymBusinessHours.FirstOrDefaultAsync(a => a.GymId == id);
            try
            {
                if (weekBusinessHours != null)
                {
                    _context.GymBusinessHours.RemoveRange(weekBusinessHours);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare neașteptată: {ex.Message}");
                return false;
            }
        }

        public async Task<BusinessHours> UpdateWorkingHours(BusinessHours businessHours, Guid id)
        {
            var dbBusinessHours = await _context.GymBusinessHours.FirstOrDefaultAsync(i => i.Id == id);
            if (dbBusinessHours != null && businessHours != null)
            {
                dbBusinessHours.DayOfWeek = businessHours.DayOfWeek;
                dbBusinessHours.OpeningHour = businessHours.OpeningHour;
                dbBusinessHours.ClosingTime = businessHours.ClosingTime;
                dbBusinessHours.Comment = businessHours.Comment;
                dbBusinessHours.GymId = businessHours.GymId;
                dbBusinessHours.IsClosed = businessHours.IsClosed;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessHoursExists(id))
                {
                    throw;
                }
            }

            return businessHours;
        }

        private bool BusinessHoursExists(Guid id)
        {
            return _context.GymBusinessHours.Any(e => e.Id == id);
        }
    }
}
