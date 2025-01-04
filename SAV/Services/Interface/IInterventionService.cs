using SAV.Models.DTO;
using SAV.Models;

namespace SAV.Services.Interface
{
    public interface IInterventionService
    {
        Task<Intervention> CreateInterventionAsync(CreateInterventionDTO interventionDto);
        Task<Intervention> GetInterventionByIdAsync(int id);
        Task<List<Intervention>> GetInterventionsByReclamationIdAsync(int reclamationId);
        Task<bool> DeleteInterventionAsync(int id);
    }
}
