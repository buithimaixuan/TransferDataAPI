import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  FacilityListComponent,
  FacilityEditComponent,
  FacilityCreateComponent,
} from './modules/facility';
import {
  ResidentCreateComponent,
  ResidentEditComponent,
  ResidentListComponent,
} from './modules/resident';
import {
  ProgressNoteCreateComponent,
  ProgressNoteEditComponent,
  ProgressNoteListComponent,
} from './modules/progress-note';

const routes: Routes = [
  { path: '', redirectTo: '/facility-list', pathMatch: 'full' },
  {
    path: 'facility-list',
    component: FacilityListComponent,
  },
  {
    path: 'facility-edit/:id',
    component: FacilityEditComponent,
  },
  {
    path: 'facility-create',
    component: FacilityCreateComponent,
  },
  {
    path: 'resident-list',
    component: ResidentListComponent,
  },
  {
    path: 'resident-edit/:id',
    component: ResidentEditComponent,
  },
  {
    path: 'resident-create',
    component: ResidentCreateComponent,
  },
  {
    path: 'progress-note-list',
    component: ProgressNoteListComponent,
  },
  {
    path: 'progress-note-edit/:id',
    component: ProgressNoteEditComponent,
  },
  {
    path: 'progress-note-create',
    component: ProgressNoteCreateComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
