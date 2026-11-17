
import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { DiaryEntryService } from '../../../core/services/diary-entry.service';
import { DiaryEntryResponse } from '../../../core/models/diary-entry.model';

@Component({
  selector: 'app-diary-details',
  standalone: true,
  imports: [DatePipe, RouterLink],
  templateUrl: './diary-details.component.html',
  styleUrl: './diary-details.component.css'
})
export class DiaryDetailsComponent implements OnInit {

  private route = inject(ActivatedRoute);

  private diaryEntryService = inject(DiaryEntryService);

  // Stores the selected diary entry
  diaryEntry = signal<DiaryEntryResponse | null>(null);

  isLoading = signal(false);

  errorMessage = signal('');

  ngOnInit(): void {

    this.loadDiaryEntry();

  }

  loadDiaryEntry(): void {

    // Get ID from the URL
    const id = Number(
      this.route.snapshot.paramMap.get('id')
    );

    if (!Number.isInteger(id) || id <= 0) {

      this.errorMessage.set('Invalid diary entry ID.');

      return;
    }

    this.isLoading.set(true);

    this.errorMessage.set('');

    // Call backend GET by ID API
    this.diaryEntryService.getDiaryEntryById(id).subscribe({

      next: (response) => {

        console.log('Diary Details Response:', response);

        this.diaryEntry.set(response);

        this.isLoading.set(false);

      },

      error: (error: HttpErrorResponse) => {

        console.error('Diary Details Error:', error);

        this.errorMessage.set(
          error.error?.message ||
          error.error?.detail ||
          error.error?.title ||
          'Unable to load diary entry.'
        );

        this.isLoading.set(false);

      }

    });

  }

}
