
import { Component, inject } from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { HttpErrorResponse } from '@angular/common/http';

import { DiaryEntryService } from '../../../core/services/diary-entry.service';

import {
  CreateDiaryEntryRequest,
  DiaryEntryResponse
} from '../../../core/models/diary-entry.model';

@Component({
  selector: 'app-create-diary-entry',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './create-diary-entry.html',
  styleUrl: './create-diary-entry.css'
})
export class CreateDiaryEntryComponent {

  // Dependency Injection
  private fb = inject(FormBuilder);

  private diaryEntryService = inject(DiaryEntryService);

  // UI State
  isSubmitting = false;

  successMessage = '';

  errorMessage = '';

  // Diary Entry Form
  diaryForm = this.fb.nonNullable.group({

    title: [
      '',
      [
        Validators.required,
        Validators.maxLength(150)
      ]
    ],

    content: [
      '',
      [
        Validators.required,
        Validators.maxLength(5000)
      ]
    ],

    entryDate: [
      this.getToday(),
      Validators.required
    ]

  });

  // Get today's date in YYYY-MM-DD format
  getToday(): string {

    const today = new Date();

    const year = today.getFullYear();

    const month = String(today.getMonth() + 1).padStart(2, '0');

    const day = String(today.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  // Create Diary Entry
  createDiaryEntry(): void {

    console.log('1. Create Diary Entry method called');

    // Clear previous messages
    this.successMessage = '';

    this.errorMessage = '';

    // Check form validation
    if (this.diaryForm.invalid) {

      this.diaryForm.markAllAsTouched();

      console.log('Form is invalid:', this.diaryForm.errors);

      return;
    }

    // Get form values
    const request: CreateDiaryEntryRequest = {

      ...this.diaryForm.getRawValue(),

      title: this.diaryForm.controls.title.value.trim(),

      content: this.diaryForm.controls.content.value.trim()

    };

    // Prevent empty whitespace values
    if (!request.title || !request.content) {

      this.errorMessage = 'Title and content cannot be empty.';

      return;
    }

    console.log('2. Sending request:', request);

    // Show loading state
    this.isSubmitting = true;

    // Call Angular Service
    this.diaryEntryService.createDiaryEntry(request).subscribe({

      // API Success
      next: (response: DiaryEntryResponse) => {

        console.log('3. API Success:', response);

        this.successMessage =
          `Diary entry created successfully! ID: ${response.diaryEntryId}`;

        // Reset form after successful save
        this.diaryForm.reset({

          title: '',

          content: '',

          entryDate: this.getToday()

        });

        this.isSubmitting = false;

      },

      // API Error
      error: (error: HttpErrorResponse) => {

        console.error('3. API Error:', error);

        console.error('HTTP Status:', error.status);

        console.error('Error Response:', error.error);

        this.errorMessage =
          error.error?.title ||
          error.error?.detail ||
          error.message ||
          'Unable to create diary entry. Please try again.';

        this.isSubmitting = false;

      }

    });

  }

}
