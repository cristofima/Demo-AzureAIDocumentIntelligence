import { Routes } from '@angular/router';
import { AnalyzeResumeComponent } from './components/analyze-resume/analyze-resume.component';

export const routes: Routes = [
  {
    path: 'resume-analysis',
    component: AnalyzeResumeComponent
  },
  {
    path: '',
    redirectTo: 'resume-analysis',
    pathMatch: 'full'
  }
];
