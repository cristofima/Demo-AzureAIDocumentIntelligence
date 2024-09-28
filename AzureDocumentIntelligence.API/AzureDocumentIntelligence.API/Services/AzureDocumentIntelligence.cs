using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;
using AzureDocumentIntelligence.API.Utils;

namespace AzureDocumentIntelligence.API.Services;

public class AzureDocumentIntelligenceService : IAzureDocumentIntelligenceService
{
    private readonly DocumentAnalysisClient documentAnalysisClient;
    private readonly string modelId;
    private readonly IEnumerable<IFieldExtractor> extractors;
    private readonly ILogger<AzureDocumentIntelligenceService> logger;

    public AzureDocumentIntelligenceService(IConfiguration configuration, IEnumerable<IFieldExtractor> extractors, ILogger<AzureDocumentIntelligenceService> logger)
    {
        var endpoint = configuration.GetValue<string>("AzureDocumentIntelligence:endpoint");
        var apiKey = configuration.GetValue<string>("AzureDocumentIntelligence:apiKey");
        this.modelId = configuration.GetValue<string>("AzureDocumentIntelligence:modelId");

        var credential = new AzureKeyCredential(apiKey);
        this.documentAnalysisClient = new DocumentAnalysisClient(new Uri(endpoint), credential);
        this.extractors = extractors;
        this.logger = logger;
    }

    public async Task<Resume> AnalyzeResumeAsync(IFormFile file)
    {
        var memoryStream = await StreamUtil.ToMemoryStreamAsync(file.OpenReadStream());
        var operation = await this.documentAnalysisClient.AnalyzeDocumentAsync(WaitUntil.Completed, this.modelId, memoryStream);
        var result = operation.Value;
        var document = result.Documents.FirstOrDefault();

        if (document == null)
        {
            this.logger.LogWarning("No documents found in the analysis result.");
            return null;
        }

        var resume = new Resume();

        foreach (var extractor in this.extractors)
        {
            try
            {
                extractor.Extract(document, resume);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Extractor '{extractor.GetType()}'. Error: {ex.Message}");
            }
        }

        return resume;
    }
}