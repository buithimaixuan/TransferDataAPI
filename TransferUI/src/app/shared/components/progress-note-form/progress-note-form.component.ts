import { DatePipe } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { ProgressNote, Resident } from 'src/app/core/models';

@Component({
  selector: 'app-progress-note-form',
  templateUrl: './progress-note-form.component.html',
})
export class ProgressNoteFormComponent {
  @Input() title: string = '';
  @Input() submitButtonLabel: string = '';
  @Input() submitProgress: number = 0;
  @Input() progressNote: ProgressNote = {} as ProgressNote;
  @Input() maxDate: Date = new Date();
  @Input() selectedResidentId: number = 0;
  @Input() residents: Resident[] = [];

  @Output() formSubmitted = new EventEmitter();

  submitForm(form: any, selectedResidentId: number) {
    if (form.valid) {
      this.formSubmitted.emit([this.progressNote, selectedResidentId]);
    }
  }
  onDateChange(event: MatDatepickerInputEvent<Date>) {
    const datePipe = new DatePipe('en-US');
    this.progressNote.createdDate = datePipe.transform(
      event.value,
      'yyyy-MM-dd'
    );
  }
}
