using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;

namespace AzureDocumentIntelligence.API.Extractors;

public class SkillsExtractor : IFieldExtractor
{
    private readonly ILogger<SkillsExtractor> logger;

    public SkillsExtractor(ILogger<SkillsExtractor> logger)
    {
        this.logger = logger;
    }

    public void Extract(AnalyzedDocument document, Resume resume)
    {
        if (document.Fields.TryGetValue("Skills", out var field) && field.FieldType == DocumentFieldType.List)
        {
            foreach (DocumentField itemField in field.Value.AsList())
            {
                if (itemField.FieldType == DocumentFieldType.Dictionary)
                {
                    var itemFields = itemField.Value.AsDictionary();
                    foreach (KeyValuePair<string, DocumentField> subField in itemFields)
                    {
                        var subFieldName = subField.Key;
                        var subFieldValue = subField.Value;

                        try
                        {
                            switch (subFieldName)
                            {
                                case "Name":
                                    var skills = subFieldValue.Value.AsString()
                                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => s.Trim()).ToList()
                                        .Where(skill => !string.IsNullOrWhiteSpace(skill))
                                        .ToList();

                                    resume.Skills.AddRange(skills);
                                    break;

                                default: break;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogWarning($"Error parsing Skill SubField '{subFieldName}'. Error: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}