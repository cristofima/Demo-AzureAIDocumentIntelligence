export interface Resume {
  personalDetails: PersonalDetails;
  socialNetworks: SocialNetworks;
  education: Education[];
  workExperience: WorkExperience[];
  skills: string[];
  projects: Project[];
  certifications: Certification[];
  languages: Language[];
}

export interface PersonalDetails {
  fullName: string;
  email: string;
  cellphone: string;
  location: string;
  summary: string;
}

export interface SocialNetworks {
  linkedIn: string;
  gitHub: string;
}

export interface Education {
  school: string;
  degree: string;
  location: string;
  startDate?: Date;
  endDate?: Date;
}

export interface WorkExperience {
  position: string;
  company: string;
  location: string;
  startDate?: Date;
  endDate?: Date;
  activities: string[];
}

export interface Project {
  name: string;
  startDate?: Date;
  endDate?: Date;
  description: string;
  url: string;
}

export interface Certification {
  name: string;
  issueDate?: Date;
  issuingOrganization: string;
}

export interface Language {
  name: string;
  level: string;
}
