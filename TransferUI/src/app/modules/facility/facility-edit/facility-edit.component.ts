import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { Facility, FacilityDTO } from 'src/app/core/models';
import { FacilityService } from 'src/app/core/services';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-facility-edit',
  templateUrl: './facility-edit.component.html',
})
export class FacilityEditComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<void>();
  facility: Facility = {} as Facility;
  submitProgress: number = 0;
  facilityId: number = 0;
  constructor(
    private readonly facilityService: FacilityService,
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    //Get the facility id from the current route.
    const routeParams = this.route.snapshot.paramMap;
    this.facilityId = Number(routeParams.get('id'));

    this.facilityService
      .getFacility(this.facilityId)
      .pipe(takeUntil(this.destroy$))
      .subscribe((facility: Facility) => {
        this.facility = facility;
      });
  }

  submitForm(angForm: NgForm): void {
    const facilityDTO: FacilityDTO = {
      name: this.facility.name,
      address: this.facility.address,
    };

    if (!angForm.invalid) {
      this.facilityService
        .updateFacility(this.facilityId, facilityDTO)
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
            this.openSnackBar('Facility updated successfully!', 'Close');
          },
          (error) => {
            // Handle error, show an error message
            this.openSnackBar('Error updated facility!', 'Close');
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
