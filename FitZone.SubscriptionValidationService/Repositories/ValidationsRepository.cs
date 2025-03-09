using FitZone.SubscriptionValidationService.Data;
using FitZone.SubscriptionValidationService.DTOs;
using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionValidationService.Repositories
{
    public class ValidationsRepository : IValidationsRepository
    {
        private readonly AppDbContext _context;

        public ValidationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAccess(ClientsAccess access)
        {
            if (access == null)
            {
                return false;
            }
            else
            {
                try
                {
                    await _context.ClientsAccesses.AddAsync(access);
                    var result = await _context.SaveChangesAsync();

                    return result > 0;

                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Eroare la salvarea datelor în baza de date: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare neașteptată: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<List<ClientsAccess>> GetClientsAccesses(Guid gymId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accesses = await _context.ClientsAccesses.Where(g => g.GymId == gymId && g.DataValidare >= startDate && g.DataValidare <= endDate && g.Role == Role.Client).ToListAsync();
                if(accesses != null)
                {
                    return accesses;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<ClientsAccess>> GetEmployeesAccesses(Guid gymId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accesses = await _context.ClientsAccesses.Where(g => g.GymId == gymId && g.DataValidare >= startDate && g.DataValidare <= endDate && g.Role != Role.Client).ToListAsync();
                if (accesses != null)
                {
                    return accesses;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<ValidationStatDto>> GetEntriesAndExitsAsync(DateTime startDate, DateTime endDate, Guid gymId)
        {
            try
            {
                var filtered = await _context.ClientsAccesses.Where(log => log.DataValidare >= startDate && log.DataValidare <= endDate && log.GymId == gymId).ToListAsync();

                var grouped = filtered
                    .GroupBy(log => log.DataValidare.ToString("yyyy-MM-dd HH:00")) // Grupare pe ore
                    .OrderBy(g => g.Key)
                    .Select(g => new ValidationStatDto
                    {
                        TimePeriod = g.Key, // Format: "YYYY-MM-DD HH:00"
                        Entries = g.Count(l => l.ValidationType == ValidationType.Entry),
                        Exits = g.Count(l => l.ValidationType != ValidationType.Entry)
                    }).ToList();

                return grouped;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<ValidationByPeriodDto> GetEntriesAndExitsByPeriodAsync(DateTime startDate, DateTime endDate, Guid gymId, string role)
        {
            try
            {
                var validations = await _context.ClientsAccesses.Where(log => log.DataValidare >= startDate && log.DataValidare <= endDate && log.GymId == gymId && log.Role == Enum.Parse<Role>(role)).ToListAsync();

                var validationsByPeriod = new ValidationByPeriodDto();
                validationsByPeriod.Entries = validations.Where(e => e.ValidationType == ValidationType.Entry).Count();
                validationsByPeriod.Exits = validations.Where(e => e.ValidationType == ValidationType.Exit).Count();
                return validationsByPeriod;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
