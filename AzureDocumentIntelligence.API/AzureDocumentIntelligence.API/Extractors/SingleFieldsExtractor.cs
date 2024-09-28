using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Models;

namespace AzureDocumentIntelligence.API.Extractors
{
    public class SingleFieldsExtractor : IFieldExtractor
    {
        public void Extract(AnalyzedDocument document, Resume resume)
        {
            foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
            {
                var fieldName = fieldKvp.Key;
                var field = fieldKvp.Value;
                switch (fieldName)
                {
                    case "FullName":
                        resume.PersonalDetails.FullName = field.Value.AsString();
                        break;
                    case "Email":
                        resume.PersonalDetails.Email = field.Value.AsString();
                        break;
                    case "Cellphone":
                        resume.PersonalDetails.Cellphone = field.Value.AsString();
                        break;
                    case "Location":
                        resume.PersonalDetails.Location = field.Value.AsString();
                        break;
                    case "Summary":
                        resume.PersonalDetails.Summary = field.Value.AsString();
                        break;
                    case "LinkedInUrl":
                        resume.SocialNetworks.LinkedIn = field.Value.AsString();
                        break;
                    case "GitHubUrl":
                        resume.SocialNetworks.GitHub = field.Value.AsString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
