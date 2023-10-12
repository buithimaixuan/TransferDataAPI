import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-facility-form',
  templateUrl: './facility-form.component.html',
})
export class FacilityFormComponent {
  @Input() title: string = '';
  @Input() formData: any;
  @Input() submitButtonLabel: string = '';
  @Input() submitProgress: number = 0;

  @Output() formSubmitted = new EventEmitter();

  submitForm(form: any) {
    if (form.valid) {
      this.formSubmitted.emit(this.formData);
    }
  }
}
