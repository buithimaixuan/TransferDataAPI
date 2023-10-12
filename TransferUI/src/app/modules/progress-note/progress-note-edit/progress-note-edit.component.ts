import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { ProgressNote, ProgressNoteDTO, Resident } from 'src/app/core/models';
import { ProgressNoteService, ResidentService } from 'src/app/core/services';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-progress-note-edit',
  templateUrl: './progress-note-edit.component.html',
  providers: [DatePipe],
})
export class ProgressNoteEditComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<void>();
  progressNote: ProgressNote = {} as ProgressNote;
  residents: Resident[] = [];
  submitProgress: number = 0;
  progressNoteId: number = 0;
  selectedResidentId: number = 0;
  maxDate: Date = new Date();

  constructor(
    private readonly residentService: ResidentService,
    private readonly progressNoteService: ProgressNoteService,
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const currentYear = new Date().getFullYear();
    const currentMonth = new Date().getMonth();
    const currentDay = new Date().getDate();
    this.maxDate = new Date(currentYear, currentMonth, currentDay);
  }

  ngOnInit(): void {
    //Get the resident id from the current route.
    const routeParams = this.route.snapshot.paramMap;
    this.progressNoteId = Number(routeParams.get('id'));

    this.progressNoteService
      .getProgressNote(this.progressNoteId)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => {
          this.selectedResidentId = this.progressNote.residentId;
        })
      )
      .subscribe((progressNote: ProgressNote) => {
        this.progressNote = progressNote;
      });

    this.residentService
      .getAllResidents()
      .pipe(takeUntil(this.destroy$))
      .subscribe((residents: Resident[]) => {
        this.residents = residents;
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
        .updateProgressNote(this.progressNoteId, progressNoteDTO)
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
            this.openSnackBar('ProgressNote updated successfully!', 'Close');
          },
          (error) => {
            // Handle error, show an error message
            this.openSnackBar('Error updated ProgressNote!', 'Close');
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
