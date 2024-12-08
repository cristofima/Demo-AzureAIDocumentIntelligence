import { Resume } from '@/models/resume.interface';
import { ApiService } from '@/services/api.service';
import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import html2pdf from 'html2pdf.js';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { FileUploadModule } from 'primeng/fileupload';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-analyze-resume',
  standalone: true,
  imports: [CommonModule, FileUploadModule, ButtonModule],
  templateUrl: './analyze-resume.component.html',
  styleUrl: './analyze-resume.component.scss',
})
export class AnalyzeResumeComponent {
  resume?: Resume;
  isLoading = false;
  selectedFile: File | null = null;
  @ViewChild('resumeSection', { static: false }) resumeSection!: ElementRef;

  constructor(
    private apiService: ApiService,
    private messageService: MessageService,
  ) {}

  onFileSelected(event: any) {
    this.selectedFile = event.files[0];
  }

  onRemoveFile() {
    this.selectedFile = null;
  }

  async onSubmit() {
    if (!this.selectedFile) return;

    this.isLoading = true;

    try {
      this.resume = await lastValueFrom(
        this.apiService.analyzeResume(this.selectedFile),
      );
      this.messageService.add({
        severity: 'success',
        summary: 'Success',
        detail: 'Resume processed',
      });
    } catch {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Error processing resume',
        sticky: true,
      });
    } finally {
      this.isLoading = false;
    }
  }

  exportToPDF() {
    const printContents = this.resumeSection.nativeElement.innerHTML;

    const options = {
      margin: 1,
      filename: 'resume.pdf',
      html2canvas: { scale: 2 },
      jsPDF: { unit: 'in', format: 'a4', orientation: 'portrait' },
    };

    html2pdf().from(printContents).set(options).save();
  }
}
