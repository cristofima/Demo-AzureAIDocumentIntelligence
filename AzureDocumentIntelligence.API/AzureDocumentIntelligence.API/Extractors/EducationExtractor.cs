using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;
using AzureDocumentIntelligence.API.Utils;

namespace AzureDocumentIntelligence.API.Extractors
{
    public class EducationExtractor : IFieldExtractor
    {
        private readonly ILogger<EducationExtractor> logger;

        public EducationExtractor(ILogger<EducationExtractor> logger)
        {
            this.logger = logger;
        }

        public void Extract(AnalyzedDocument document, Resume resume)
        {
            if (document.Fields.TryGetValue("Education", out var field) && field.FieldType == DocumentFieldType.List)
            {
                var educationList = new List<Education>();
                foreach (DocumentField itemField in field.Value.AsList())
                {
                    if (itemField.FieldType == DocumentFieldType.Dictionary)
                    {
                        var education = new Education();
                        var itemFields = itemField.Value.AsDictionary();
                        foreach (KeyValuePair<string, DocumentField> subField in itemFields)
                        {
                            var subFieldName = subField.Key;
                            var subFieldValue = subField.Value;

                            try
                            {
                                switch (subFieldName)
                                {
                                    case "School":
                                        education.School = subFieldValue.Value.AsString();
                                        break;
                                    case "Degree":
                                        education.Degree = subFieldValue.Value.AsString();
                                        break;
                                    case "Location":
                                        education.Location = subFieldValue.Value.AsString();
                                        break;
                                    case "StartDate":
                                        education.StartDate = DateUtil.ParseDate(subFieldValue);
                                        break;
                                    case "EndDate":
                                        education.EndDate = DateUtil.ParseDate(subFieldValue);
                                        break;
                                    default: break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogWarning($"Error parsing Education SubField '{subFieldName}'. Error: {ex.Message}");
                            }
                        }

                        if (string.IsNullOrEmpty(education.Degree))
                        {
                            continue;
                        }

                        educationList.Add(education);
                    }
                }


                resume.Education.AddRange(educationList);
            }
        }
    }
}
