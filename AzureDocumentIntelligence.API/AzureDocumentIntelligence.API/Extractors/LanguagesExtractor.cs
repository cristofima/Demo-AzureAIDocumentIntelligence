using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;

namespace AzureDocumentIntelligence.API.Extractors
{
    public class LanguagesExtractor : IFieldExtractor
    {
        private readonly ILogger<LanguagesExtractor> logger;

        public LanguagesExtractor(ILogger<LanguagesExtractor> logger)
        {
            this.logger = logger;
        }

        public void Extract(AnalyzedDocument document, Resume resume)
        {
            if (document.Fields.TryGetValue("Languages", out var field) && field.FieldType == DocumentFieldType.List)
            {
                var languages = new List<Language>();
                foreach (DocumentField itemField in field.Value.AsList())
                {
                    if (itemField.FieldType == DocumentFieldType.Dictionary)
                    {
                        var language = new Language();
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
                                        language.Name = subFieldValue.Value.AsString();
                                        break;
                                    case "Level":
                                        language.Level = subFieldValue.Value.AsString();
                                        break;
                                    default: break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogWarning($"Error parsing Language SubField '{subFieldName}'. Error: {ex.Message}");
                            }
                        }

                        languages.Add(language);
                    }
                }

                resume.Languages.AddRange(languages);
            }
        }
    }
}
