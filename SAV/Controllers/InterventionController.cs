using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAV.Models.DTO;
using SAV.Services.Interface;

namespace SAV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        private readonly IInterventionService _interventionService;

        public InterventionController(IInterventionService interventionService)
        {
            _interventionService = interventionService;
        }

        [HttpPost]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> CreateIntervention([FromBody] CreateInterventionDTO interventionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var intervention = await _interventionService.CreateInterventionAsync(interventionDto);
                return CreatedAtAction(nameof(GetIntervention), new { id = intervention.Id }, intervention);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> GetIntervention(int id)
        {
            try
            {
                var intervention = await _interventionService.GetInterventionByIdAsync(id);
                return Ok(intervention);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("reclamation/{reclamationId}")]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> GetInterventionsByReclamation(int reclamationId)
        {
            var interventions = await _interventionService.GetInterventionsByReclamationIdAsync(reclamationId);
            if (interventions == null || !interventions.Any())
                return NotFound();

            return Ok(interventions);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SAV")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            try
            {
                await _interventionService.DeleteInterventionAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

