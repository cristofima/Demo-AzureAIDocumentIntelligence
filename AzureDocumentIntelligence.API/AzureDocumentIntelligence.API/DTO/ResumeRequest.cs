using System.ComponentModel.DataAnnotations;

namespace AzureDocumentIntelligence.API.DTO
{
    public class ResumeRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
