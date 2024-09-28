using AzureDocumentIntelligence.API.Models;

namespace AzureDocumentIntelligence.API.Interfaces
{
    public interface IAzureDocumentIntelligenceService
    {
        Task<Resume> AnalyzeResumeAsync(IFormFile file);
    }
}
