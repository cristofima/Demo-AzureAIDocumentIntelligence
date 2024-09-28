import { Resume } from '@/models/resume.interface';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  public analyzeResume(file: File) {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<Resume>(`${this.baseUrl}/api/Resume`, formData);
  }
}
