
import { Routes } from '@angular/router';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'create-diary',
    pathMatch: 'full'
  },

  {
    path: 'create-diary',
    loadComponent: () =>
      import('./features/diary-entries/create-diary-entry/create-diary-entry')
        .then(m => m.CreateDiaryEntryComponent)
  },

  {
    path: 'diary-list',
    loadComponent: () =>
      import('./features/diary-entries/diary-list/diary-list.component')
        .then(m => m.DiaryListComponent)
  },


  {
    path: '**',
    redirectTo: 'create-diary'
  }

];
