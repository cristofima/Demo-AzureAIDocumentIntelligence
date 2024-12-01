namespace AzureDocumentIntelligence.API.Models
{
    public class Resume
    {
        public PersonalDetails PersonalDetails { get; set; } = new();
        public SocialNetworks SocialNetworks { get; set; } = new();
        public List<Education> Education { get; set; } = [];
        public List<WorkExperience> WorkExperience { get; set; } = [];
        public List<string> Skills { get; set; } = [];
        public List<Project> Projects { get; set; } = [];
        public List<Certification> Certifications { get; set; } = [];
        public List<Language> Languages { get; set; } = [];
    }

    public class PersonalDetails
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
    }

    public class Education
    {
        public string School { get; set; }
        public string Degree { get; set; }
        public string Location { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

    public class WorkExperience
    {
        public string Position { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public List<string> Activities { get; set; }
    }

    public class SocialNetworks
    {
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
    }

    public class Language
    {
        public string Name { get; set; }
        public string Level { get; set; }
    }

    public class Project
    {
        public string Name { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public class Certification
    {
        public string Name { get; set; }
        public DateOnly? IssueDate { get; set; }
        public string IssuingOrganization { get; set; }
    }
}
