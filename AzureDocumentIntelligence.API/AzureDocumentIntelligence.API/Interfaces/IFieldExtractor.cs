using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Models;

namespace AzureDocumentIntelligence.API.Interfaces
{
    public interface IFieldExtractor
    {
        void Extract(AnalyzedDocument document, Resume resume);
    }
}
