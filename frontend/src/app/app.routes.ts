
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
    path: '**',
    redirectTo: 'create-diary'
  }

];
