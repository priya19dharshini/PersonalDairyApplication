
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { 
  CreateDiaryEntryRequest,
  DiaryEntryResponse
} from '../models/diary-entry.model';

@Injectable({
  providedIn: 'root'
})
export class DiaryEntryService {

  private http = inject(HttpClient);

  private apiUrl = '/api/DiaryEntries';

  createDiaryEntry(
    request: CreateDiaryEntryRequest
  ): Observable<DiaryEntryResponse> {

    return this.http.post<DiaryEntryResponse>(
      this.apiUrl,
      request
    );
  }

  getDiaryEntries(): Observable<DiaryEntryResponse[]> {

    return this.http.get<DiaryEntryResponse[]>(
      this.apiUrl
    );
  }

  // Get one diary entry by ID
  getDiaryEntryById(
    id: number
  ): Observable<DiaryEntryResponse> {

    return this.http.get<DiaryEntryResponse>(
      `${this.apiUrl}/${id}`
    );
  }
}
