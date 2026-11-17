
import { Component, OnInit, inject, signal } from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { ActivatedRoute, RouterLink } from '@angular/router';

import { HttpErrorResponse } from '@angular/common/http';

import { DiaryEntryService } from '../../../core/services/diary-entry.service';

import { UpdateDiaryEntryRequest } from '../../../core/models/diary-entry.model';

@Component({
  selector: 'app-diary-edit',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './diary-edit.component.html',
  styleUrl: './diary-edit.component.css'
})
export class DiaryEditComponent implements OnInit {

  private fb = inject(FormBuilder);

  private route = inject(ActivatedRoute);

  private diaryEntryService = inject(DiaryEntryService);

  diaryEntryId = 0;

  isLoading = signal(false);

  isSubmitting = signal(false);

  successMessage = signal('');

  errorMessage = signal('');

  // Reactive form
  diaryForm = this.fb.nonNullable.group({

    title: ['', [
      Validators.required,
      Validators.maxLength(150)
    ]],

    content: ['', [
      Validators.required,
      Validators.maxLength(5000)
    ]],

    entryDate: ['', Validators.required]

  });

  // Component initialization
  ngOnInit(): void {

    this.loadDiaryEntry();

  }

  // GET existing diary entry
  loadDiaryEntry(): void {

    this.diaryEntryId = Number(
      this.route.snapshot.paramMap.get('id')
    );

    if (!Number.isInteger(this.diaryEntryId) ||
        this.diaryEntryId <= 0) {

      this.errorMessage.set('Invalid diary entry ID.');

      return;
    }

    this.isLoading.set(true);

    this.diaryEntryService
      .getDiaryEntryById(this.diaryEntryId)
      .subscribe({

        next: (response) => {

          console.log('Existing Diary Entry:', response);

          // Load existing values into the form
          this.diaryForm.patchValue({

            title: response.title,

            content: response.content,

            entryDate: response.entryDate.substring(0, 10)

          });

          this.isLoading.set(false);

        },

        error: (error: HttpErrorResponse) => {

          console.error('Load Error:', error);

          this.errorMessage.set(
            error.error?.message ||
            error.error?.detail ||
            'Unable to load diary entry.'
          );

          this.isLoading.set(false);

        }

      });

  }

  // UPDATE diary entry
  updateDiaryEntry(): void {

    this.successMessage.set('');

    this.errorMessage.set('');

    if (this.diaryForm.invalid) {

      this.diaryForm.markAllAsTouched();

      return;

    }

    const request: UpdateDiaryEntryRequest = {

      ...this.diaryForm.getRawValue(),

      title: this.diaryForm.controls.title.value.trim(),

      content: this.diaryForm.controls.content.value.trim()

    };

    if (!request.title || !request.content) {

      this.errorMessage.set(
        'Title and content cannot be empty.'
      );

      return;

    }

    this.isSubmitting.set(true);

    this.diaryEntryService.updateDiaryEntry(
      this.diaryEntryId,
      request
    ).subscribe({

      next: (response) => {

        console.log('Updated Diary Entry:', response);

        this.successMessage.set(
          'Diary entry updated successfully!'
        );

        this.isSubmitting.set(false);

        // Display the latest saved values
        this.diaryForm.patchValue({

          title: response.title,

          content: response.content,

          entryDate: response.entryDate.substring(0, 10)

        });

      },

      error: (error: HttpErrorResponse) => {

        console.error('Update Error:', error);

        this.errorMessage.set(
          error.error?.message ||
          error.error?.detail ||
          error.error?.title ||
          'Unable to update diary entry.'
        );

        this.isSubmitting.set(false);

      }

    });

  }

}
