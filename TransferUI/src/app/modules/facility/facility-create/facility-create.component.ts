import { Component, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { FacilityDTO } from 'src/app/core/models';
import { FacilityService } from 'src/app/core/services';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-facility-create',
  templateUrl: './facility-create.component.html',
})
export class FacilityCreateComponent implements OnDestroy {
  destroy$ = new Subject<void>();
  facilityDTO: FacilityDTO = {} as FacilityDTO;
  submitProgress: number = 0;
  constructor(
    private readonly facilityService: FacilityService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {}

  submitForm(angForm: NgForm): void {
    if (!angForm.invalid) {
      this.facilityService
        .addFacility(this.facilityDTO)
        .pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            this.submitProgress = 100; // Set progress to 100 when complete
            setTimeout(() => {
              this.submitProgress = 0; // Reset progress to 0 after a delay
              this.router.navigate(['/facility-list']);
            }, 300); // Adjust the delay as needed
          })
        )
        .subscribe(
          (response) => {
            // Handle success, show a success message
            this.openSnackBar('Facility added successfully!', 'Close');
          },
          (error) => {
            // Handle error, show an error message
            this.openSnackBar('Error added facility!', 'Close');
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
