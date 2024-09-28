using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;
using AzureDocumentIntelligence.API.Utils;

namespace AzureDocumentIntelligence.API.Extractors
{
    public class ProjectsExtractor : IFieldExtractor
    {
        private readonly ILogger<ProjectsExtractor> logger;

        public ProjectsExtractor(ILogger<ProjectsExtractor> logger)
        {
            this.logger = logger;
        }

        public void Extract(AnalyzedDocument document, Resume resume)
        {
            if (document.Fields.TryGetValue("Projects", out var field) && field.FieldType == DocumentFieldType.List)
            {
                var projects = new List<Project>();
                foreach (DocumentField itemField in field.Value.AsList())
                {
                    if (itemField.FieldType == DocumentFieldType.Dictionary)
                    {
                        var project = new Project();
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
                                        project.Name = subFieldValue.Value.AsString();
                                        break;
                                    case "Description":
                                        project.Description = subFieldValue.Value.AsString();
                                        break;
                                    case "Url":
                                        project.Url = subFieldValue.Value.AsString();
                                        break;
                                    case "StartDate":
                                        project.StartDate = DateUtil.ParseDate(subFieldValue);
                                        break;
                                    case "EndDate":
                                        project.EndDate = DateUtil.ParseDate(subFieldValue);
                                        break;
                                    default: break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogWarning($"Error parsing Project SubField '{subFieldName}'. Error: {ex.Message}");
                            }
                        }

                        projects.Add(project);
                    }
                }


                resume.Projects.AddRange(projects);
            }
        }
    }
}
