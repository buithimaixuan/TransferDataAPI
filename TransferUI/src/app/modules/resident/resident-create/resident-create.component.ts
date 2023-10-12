import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { Facility, Resident, ResidentDTO } from 'src/app/core/models';
import { FacilityService, ResidentService } from 'src/app/core/services';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-resident-create',
  templateUrl: './resident-create.component.html',
  providers: [DatePipe],
})
export class ResidentCreateComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<void>();
  resident: Resident = {} as Resident;
  facilities: Facility[] = [];
  submitProgress: number = 0;
  selectedFacilityId: number = 0;
  minDate: Date = new Date();
  maxDate: Date = new Date();

  constructor(
    private readonly facilityService: FacilityService,
    private readonly residentService: ResidentService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    const currentYear = new Date().getFullYear();
    const currentMonth = new Date().getMonth();
    const currentDay = new Date().getDate();
    this.minDate = new Date(currentYear - 70, 0, 1);
    this.maxDate = new Date(currentYear, currentMonth, currentDay);
  }

  ngOnInit(): void {
    this.facilityService
      .getAllFacilities()
      .pipe(takeUntil(this.destroy$))
      .subscribe((facilities: Facility[]) => {
        this.facilities = facilities;
        this.selectedFacilityId = facilities[0].id;
      });
  }

  submitForm(angForm: NgForm, selectedFacilityId: number): void {
    const residentDTO: ResidentDTO = {
      firstName: this.resident.firstName,
      lastName: this.resident.lastName,
      doB: this.resident.doB,
      facilityId: selectedFacilityId,
    };

    if (!angForm.invalid) {
      this.residentService
        .addResident(residentDTO)
        .pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            this.submitProgress = 100; // Set progress to 100 when complete
            setTimeout(() => {
              this.submitProgress = 0; // Reset progress to 0 after a delay
              this.router.navigate(['/resident-list']);
            }, 300); // Adjust the delay as needed
          })
        )
        .subscribe(
          (response) => {
            // Handle success, show a success message
            this.openSnackBar('Resident added successfully!', 'Close');
          },
          (error) => {
            // Handle error, show an error message
            this.openSnackBar('Error added resident!', 'Close');
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
