using AzureDocumentIntelligence.API.DTO;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureDocumentIntelligence.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IAzureDocumentIntelligenceService azureDocumentIntelligenceService;

        public ResumeController(IAzureDocumentIntelligenceService azureDocumentIntelligenceService) {
            this.azureDocumentIntelligenceService = azureDocumentIntelligenceService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Resume))]
        public async Task<IActionResult> ExtractText([FromForm] ResumeRequest request)
        {
            try
            {
                var result = await this.azureDocumentIntelligenceService.AnalyzeResumeAsync(request.File);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
