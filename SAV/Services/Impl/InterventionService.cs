using Microsoft.EntityFrameworkCore;
using SAV.Data;
using SAV.Models;
using SAV.Models.DTO;
using SAV.Services.Interface;
namespace SAV.Services.Impl
{
    public class InterventionService : IInterventionService
    {
        private readonly AppDbContext _context;

        public InterventionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Intervention> CreateInterventionAsync(CreateInterventionDTO interventionDto)
        {
            var reclamation = await _context.Reclamations.FindAsync(interventionDto.ReclamationId);
            if (reclamation == null)
                throw new KeyNotFoundException("Reclamation not found");

            var intervention = new Intervention
            {
                ReclamationId = interventionDto.ReclamationId,
                TechnicienId = interventionDto.TechnicienId,
                IsUnderWarranty = interventionDto.IsUnderWarranty,
                TotalCost = interventionDto.TotalCost,
                PiecesRechange = await _context.PiecesRechange
                    .Where(pr => interventionDto.PieceRechangeIds.Contains(pr.Id))
                    .ToListAsync()
            };

            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return intervention;
        }

        public async Task<Intervention> GetInterventionByIdAsync(int id)
        {
            var intervention = await _context.Interventions
                .Include(i => i.Reclamation)
                    .ThenInclude(r => r.Client)
                .Include(i => i.Technicien)
                .Include(i => i.PiecesRechange)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (intervention == null)
                throw new KeyNotFoundException("Intervention not found");

            return intervention;
        }

        public async Task<List<Intervention>> GetInterventionsByReclamationIdAsync(int reclamationId)
        {
            return await _context.Interventions
                .Where(i => i.ReclamationId == reclamationId)
                .Include(i => i.Technicien)
                .Include(i => i.PiecesRechange)
                .ToListAsync();
        }

        public async Task<bool> DeleteInterventionAsync(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
                throw new KeyNotFoundException("Intervention not found");

            _context.Interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
