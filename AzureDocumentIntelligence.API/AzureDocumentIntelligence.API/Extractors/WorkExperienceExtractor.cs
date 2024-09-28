using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;
using AzureDocumentIntelligence.API.Utils;

namespace AzureDocumentIntelligence.API.Extractors
{
    public class WorkExperienceExtractor : IFieldExtractor
    {
        private readonly ILogger<WorkExperienceExtractor> logger;

        public WorkExperienceExtractor(ILogger<WorkExperienceExtractor> logger)
        {
            this.logger = logger;
        }

        public void Extract(AnalyzedDocument document, Resume resume)
        {
            if (document.Fields.TryGetValue("WorkExperience", out var field) && field.FieldType == DocumentFieldType.List)
            {
                var workExperienceList = new List<WorkExperience>();
                foreach (DocumentField itemField in field.Value.AsList())
                {
                    if (itemField.FieldType == DocumentFieldType.Dictionary)
                    {
                        var workExperience = new WorkExperience();
                        var itemFields = itemField.Value.AsDictionary();
                        foreach (KeyValuePair<string, DocumentField> subField in itemFields)
                        {
                            var subFieldName = subField.Key;
                            var subFieldValue = subField.Value;

                            try
                            {
                                switch (subFieldName)
                                {
                                    case "Position":
                                        workExperience.Position = subFieldValue.Value.AsString();
                                        break;
                                    case "Company":
                                        workExperience.Company = subFieldValue.Value.AsString();
                                        break;
                                    case "Location":
                                        workExperience.Location = subFieldValue.Value.AsString();
                                        break;
                                    case "Description":
                                        workExperience.Description = subFieldValue.Value.AsString();
                                        break;
                                    case "StartDate":
                                        workExperience.StartDate = DateUtil.ParseDate(subFieldValue);
                                        break;
                                    case "EndDate":
                                        workExperience.EndDate = DateUtil.ParseDate(subFieldValue);
                                        break;
                                    default: break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogWarning($"Error parsing WorkExperience SubField '{subFieldName}'. Error: {ex.Message}");
                            }
                        }

                        if (string.IsNullOrEmpty(workExperience.Position) || workExperience.StartDate == null)
                        {
                            continue;
                        }

                        workExperienceList.Add(workExperience);
                    }
                }

                resume.WorkExperience.AddRange(workExperienceList);
            }
        }
    }
}
