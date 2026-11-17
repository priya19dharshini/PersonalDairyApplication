
import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterLink } from '@angular/router';

import { DiaryEntryService } from '../../../core/services/diary-entry.service';
import { DiaryEntryResponse } from '../../../core/models/diary-entry.model';

@Component({
  selector: 'app-diary-list',
  standalone: true,
  imports: [DatePipe,RouterLink],
  templateUrl: './diary-list.component.html',
  styleUrl: './diary-list.component.css'
})
export class DiaryListComponent implements OnInit {

  private diaryEntryService = inject(DiaryEntryService);

  // Reactive state using Angular Signals
  diaryEntries = signal<DiaryEntryResponse[]>([]);

  isLoading = signal(false);
  errorMessage = signal('');

  ngOnInit(): void {
    this.loadDiaryEntries();
  }

  loadDiaryEntries(): void {

    this.isLoading.set(true);
    this.errorMessage.set('');

    this.diaryEntryService.getDiaryEntries().subscribe({

      next: (response) => {

        console.log('API Response:', response);

        // Update the signal
        this.diaryEntries.set(response);

        this.isLoading.set(false);

        console.log('Updated Entries:', this.diaryEntries());

      },

      error: (error: HttpErrorResponse) => {

        console.error('API Error:', error);

        this.errorMessage.set(
          error.error?.detail ||
          error.error?.title ||
          'Unable to load diary entries.'
        );

        this.isLoading.set(false);
      }

    });
  }
}
