import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { Resident, Facility } from 'src/app/core/models';
import { FacilityService, ResidentService } from 'src/app/core/services';

@Component({
  selector: 'app-resident-list',
  templateUrl: './resident-list.component.html',
})
export class ResidentListComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'doB',
    'facility',
    'edit',
    'delete',
  ];
  destroy$ = new Subject<void>();
  residents: Resident[] = [];
  deleteProgress: number = 0;

  constructor(
    private readonly residentService: ResidentService,
    private readonly facilityService: FacilityService,
    private _snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.residentService
      .getAllResidents()
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => {
          for (const resident of this.residents) {
            //because I don't want to change the backend here, so I just call this api in the loop. It can cause some issues about performance if the loop data is large.
            this.facilityService
              .getFacility(resident.facilityId)
              .pipe(takeUntil(this.destroy$))
              .subscribe((facility: Facility) => {
                resident.facility = facility;
              });
          }
        })
      )
      .subscribe((residents: Resident[]) => {
        this.residents = residents;
      });
  }

  deleteResident(id: number): void {
    this.residentService
      .deleteResident(id)
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
        this.residents = this.residents.filter((item) => item.id !== id);
        this.openSnackBar(
          `Delete resident has id ${id} successfully!`,
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
