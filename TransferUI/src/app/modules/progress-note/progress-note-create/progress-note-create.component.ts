import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { ProgressNote, ProgressNoteDTO, Resident } from 'src/app/core/models';
import { ProgressNoteService, ResidentService } from 'src/app/core/services';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-progress-note-create',
  templateUrl: './progress-note-create.component.html',
  providers: [DatePipe],
})
export class ProgressNoteCreateComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<void>();
  progressNote: ProgressNote = {} as ProgressNote;
  residents: Resident[] = [];
  submitProgress: number = 0;
  selectedResidentId: number = 0;
  maxDate: Date = new Date();

  constructor(
    private readonly residentService: ResidentService,
    private readonly progressNoteService: ProgressNoteService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    const currentYear = new Date().getFullYear();
    const currentMonth = new Date().getMonth();
    const currentDay = new Date().getDate();
    this.maxDate = new Date(currentYear, currentMonth, currentDay);
  }

  ngOnInit(): void {
    this.residentService
      .getAllResidents()
      .pipe(takeUntil(this.destroy$))
      .subscribe((residents: Resident[]) => {
        this.residents = residents;
        this.selectedResidentId = residents[0].id;
      });
  }

  onDateChange(event: MatDatepickerInputEvent<Date>) {
    const datePipe = new DatePipe('en-US');
    this.progressNote.createdDate = datePipe.transform(
      event.value,
      'yyyy-MM-dd'
    );
  }

  submitForm(angForm: NgForm): void {
    const progressNoteDTO: ProgressNoteDTO = {
      content: this.progressNote.content,
      type: this.progressNote.type,
      createdDate: this.progressNote.createdDate,
      residentId: this.selectedResidentId,
    };

    if (!angForm.invalid) {
      this.progressNoteService
        .addProgressNote(progressNoteDTO)
        .pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            this.submitProgress = 100; // Set progress to 100 when complete
            setTimeout(() => {
              this.submitProgress = 0; // Reset progress to 0 after a delay
              this.router.navigate(['/progress-note-list']);
            }, 300); // Adjust the delay as needed
          })
        )
        .subscribe(
          (response) => {
            // Handle success, show a success message
            this.openSnackBar('ProgressNote added successfully!', 'Close');
          },
          (error) => {
            // Handle error, show an error message
            this.openSnackBar('Error added ProgressNote!', 'Close');
          }
        );
    }
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 3000,
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
