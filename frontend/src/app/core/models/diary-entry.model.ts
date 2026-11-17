
export interface CreateDiaryEntryRequest {
  title: string;
  content: string;
  entryDate: string;
}

export interface DiaryEntryResponse {
  diaryEntryId: number;
  title: string;
  content: string;
  entryDate: string;
}


export interface UpdateDiaryEntryRequest {
  title: string;
  content: string;
  entryDate: string;
}
