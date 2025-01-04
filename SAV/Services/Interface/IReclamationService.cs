using SAV.Models.DTO;
using SAV.Models;

namespace SAV.Services.Interface
{
    public interface IReclamationService
    {
        Task<Reclamation> CreateReclamationAsync(ReclamationCreateDTO reclamationDto);
        Task<Reclamation> GetReclamationByIdAsync(int id);
        Task<IEnumerable<Reclamation>> GetReclamationsByClientIdAsync(Guid clientId);
        Task<IEnumerable<Reclamation>> GetAllReclamationsAsync();
        Task<bool> DeleteReclamationAsync(int id);
    }
}
