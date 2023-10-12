import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {
  EntityListComponent,
  FooterComponent,
  HeaderComponent,
} from './shared/components';
import {
  FacilityListComponent,
  FacilityCreateComponent,
  FacilityEditComponent,
} from './modules/facility';
import {
  ResidentListComponent,
  ResidentEditComponent,
  ResidentCreateComponent,
} from './modules/resident';
import {
  ProgressNoteListComponent,
  ProgressNoteCreateComponent,
  ProgressNoteEditComponent,
} from './modules/progress-note';
import { CustomDatePipe } from './shared/pipes';

@NgModule({
  declarations: [
    AppComponent,
    CustomDatePipe,
    FooterComponent,
    HeaderComponent,
    FacilityListComponent,
    FacilityCreateComponent,
    FacilityEditComponent,
    ResidentListComponent,
    ResidentEditComponent,
    ResidentCreateComponent,
    ProgressNoteListComponent,
    ProgressNoteEditComponent,
    ProgressNoteCreateComponent,
    EntityListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatTableModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatRippleModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatProgressBarModule,
    FormsModule,
    MatSelectModule,
    MatNativeDateModule,
    MatDatepickerModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
