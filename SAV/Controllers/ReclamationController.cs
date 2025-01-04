using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAV.Data;
using SAV.Models;
using SAV.Models.DTO;
using SAV.Services.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace SAV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamationController : ControllerBase
    {
        private readonly IReclamationService _reclamationService;

        public ReclamationController(IReclamationService reclamationService)
        {
            _reclamationService = reclamationService;
        }

        // POST: api/Reclamation
        [HttpPost]
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> CreateReclamation([FromBody] ReclamationCreateDTO reclamationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reclamation = await _reclamationService.CreateReclamationAsync(reclamationDto);
            return CreatedAtAction(nameof(GetReclamation), new { id = reclamation.Id }, reclamation);
        }

        // GET: api/Reclamation/5
        [HttpGet("{id}")]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> GetReclamation(int id)
        {
            var reclamation = await _reclamationService.GetReclamationByIdAsync(id);

            if (reclamation == null)
                return NotFound(new { message = "Reclamation not found" });

            return Ok(reclamation);
        }

        // GET: api/Reclamation/client/{clientId}
        [HttpGet("client/{clientId}")]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> GetReclamationsByClient(Guid clientId)
        {
            var reclamations = await _reclamationService.GetReclamationsByClientIdAsync(clientId);

            if (reclamations == null || !reclamations.Any())
                return NotFound(new { message = "No reclamations found for this client" });

            return Ok(reclamations);
        }

        // GET: api/Reclamation
        [HttpGet]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> GetAllReclamations()
        {
            var reclamations = await _reclamationService.GetAllReclamationsAsync();

            if (!reclamations.Any())
                return NotFound(new { message = "No reclamations found" });

            return Ok(reclamations);
        }

        // DELETE: api/Reclamation/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> DeleteReclamation(int id)
        {
            var success = await _reclamationService.DeleteReclamationAsync(id);
            if (!success)
                return NotFound(new { message = "Reclamation not found" });

            return NoContent();
        }
    }
}