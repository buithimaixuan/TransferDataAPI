import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { Resident, Facility, ProgressNote } from 'src/app/core/models';
import { ProgressNoteService, ResidentService } from 'src/app/core/services';

@Component({
  selector: 'app-progress-note-list',
  templateUrl: './progress-note-list.component.html',
  styleUrls: ['./progress-note-list.component.css'],
})
export class ProgressNoteListComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = [
    'id',
    'content',
    'type',
    'createdDate',
    'resident',
    'edit',
    'delete',
  ];
  destroy$ = new Subject<void>();
  progressNotes: ProgressNote[] = [];
  deleteProgress: number = 0;

  constructor(
    private readonly residentService: ResidentService,
    private readonly progressNoteService: ProgressNoteService,
    private _snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.progressNoteService
      .getAllProgressNotes()
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => {
          for (const progressNote of this.progressNotes) {
            //because I don't want to change the backend here, so I just call this api in the loop. It can cause some issues about performance if the loop data is large.
            this.residentService
              .getResident(progressNote.residentId)
              .pipe(takeUntil(this.destroy$))
              .subscribe((resident: Resident) => {
                progressNote.resident = resident;
              });
          }
        })
      )
      .subscribe((progressNotes: ProgressNote[]) => {
        this.progressNotes = progressNotes;
      });
  }

  deleteProgressNote(id: number): void {
    this.progressNoteService
      .deleteProgressNote(id)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => {
          this.deleteProgress = 100; // Set progress to 100 when complete
          setTimeout(() => {
            this.deleteProgress = 0; // Reset progress to 0 after a delay
          }, 300); // Adjust the delay as needed
        })
      )
      .subscribe(() => {
        this.progressNotes = this.progressNotes.filter(
          (item) => item.id !== id
        );
        this.openSnackBar(
          `Delete ProgressNote has id ${id} successfully!`,
          'Close'
        );
      });
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
