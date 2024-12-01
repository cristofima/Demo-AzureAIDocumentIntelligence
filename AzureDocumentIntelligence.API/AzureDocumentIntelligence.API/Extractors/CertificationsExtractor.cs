using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;
using AzureDocumentIntelligence.API.Utils;

namespace AzureDocumentIntelligence.API.Extractors
{
    public class CertificationsExtractor : IFieldExtractor
    {
        private readonly ILogger<CertificationsExtractor> logger;

        public CertificationsExtractor(ILogger<CertificationsExtractor> logger)
        {
            this.logger = logger;
        }

        public void Extract(AnalyzedDocument document, Resume resume)
        {
            if (document.Fields.TryGetValue("Certifications", out var field) && field.FieldType == DocumentFieldType.List)
            {
                var certifications = new List<Certification>();
                foreach (DocumentField itemField in field.Value.AsList())
                {
                    if (itemField.FieldType == DocumentFieldType.Dictionary)
                    {
                        var certification = new Certification();
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
                                        var list = StringUtil.SplitText(subFieldValue.Value.AsString(), ",");
                                        certification.Name = list[0];
                                        if (list.Count > 1)
                                        {
                                            for (int i = 1; i < list.Count; i++)
                                            {
                                                certifications.Add(new() { Name = list[i] });
                                            }
                                        }
                                        break;

                                    case "IssuingOrganization":
                                        certification.IssuingOrganization = subFieldValue.Value.AsString();
                                        break;

                                    case "IssueDate":
                                        certification.IssueDate = DateUtil.ParseDate(subFieldValue);
                                        break;

                                    default: break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogWarning($"Error parsing Certifications SubField '{subFieldName}'. Error: {ex.Message}");
                            }
                        }

                        certifications.Add(certification);
                    }
                }

                resume.Certifications.AddRange(certifications);
            }
        }
    }
}