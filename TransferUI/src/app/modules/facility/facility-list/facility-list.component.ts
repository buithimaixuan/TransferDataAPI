import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { Facility } from 'src/app/core/models';
import { FacilityService } from 'src/app/core/services';

@Component({
  selector: 'app-facility-list',
  templateUrl: './facility-list.component.html',
})
export class FacilityListComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['id', 'name', 'address', 'edit', 'delete'];
  destroy$ = new Subject<void>();
  facilities: Facility[] = [];
  deleteProgress: number = 0;

  constructor(
    private readonly facilityService: FacilityService,
    private _snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.facilityService
      .getAllFacilities()
      .pipe(takeUntil(this.destroy$))
      .subscribe((facilities: Facility[]) => {
        this.facilities = facilities;
      });
  }

  deleteFacilities(id: number): void {
    this.facilityService
      .deleteFacility(id)
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
        this.facilities = this.facilities.filter((item) => item.id !== id);
        this.openSnackBar(
          `Delete facility has id ${id} successfully!`,
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
