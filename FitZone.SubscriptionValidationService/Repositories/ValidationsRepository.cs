using FitZone.SubscriptionValidationService.Data;
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
    }
}
