using Microsoft.EntityFrameworkCore;
using SAV.Data;
using SAV.Models;
using SAV.Models.DTO;
using SAV.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SAV.Services.Impl
{
    public class ReclamationService : IReclamationService
    {

        private readonly AppDbContext _context;

        public ReclamationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reclamation> CreateReclamationAsync(ReclamationCreateDTO reclamationDto)
        {
            var reclamation = new Reclamation
            {
                Description = reclamationDto.Description,
                DateCreation = reclamationDto.DateCreation,
                ClientId = reclamationDto.ClientId
            };

            await _context.Reclamations.AddAsync(reclamation);
            await _context.SaveChangesAsync();

            return reclamation;
        }

        public async Task<Reclamation> GetReclamationByIdAsync(int id)
        {
            return await _context.Reclamations
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reclamation>> GetReclamationsByClientIdAsync(Guid clientId)
        {
            return await _context.Reclamations
                .Where(r => r.ClientId == clientId)
                .Include(r => r.Client)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reclamation>> GetAllReclamationsAsync()
        {
            try
            {
                return await _context.Reclamations
                    .Include(r => r.Client)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An error occurred while fetching reclamations.", ex);
            }
        }


        public async Task<bool> DeleteReclamationAsync(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null)
                return false;

            _context.Reclamations.Remove(reclamation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
