import { DatePipe } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Facility, Resident } from 'src/app/core/models';

@Component({
  selector: 'app-resident-form',
  templateUrl: './resident-form.component.html',
})
export class ResidentFormComponent {
  @Input() title: string = '';
  @Input() submitButtonLabel: string = '';
  @Input() submitProgress: number = 0;
  @Input() resident: Resident = {} as Resident;
  @Input() minDate: Date = new Date();
  @Input() maxDate: Date = new Date();
  @Input() selectedFacilityId: number = 0;
  @Input() facilities: Facility[] = [];

  @Output() formSubmitted = new EventEmitter();

  submitForm(form: any, selectedFacilityId: number) {
    if (form.valid) {
      this.formSubmitted.emit([this.resident, selectedFacilityId]);
    }
  }
  onDateChange(event: MatDatepickerInputEvent<Date>) {
    const datePipe = new DatePipe('en-US');
    this.resident.doB = datePipe.transform(event.value, 'yyyy-MM-dd');
  }
}
