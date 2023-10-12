import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-entity-list',
  templateUrl: './entity-list.component.html',
  styleUrls: ['./entity-list.component.css'],
})
export class EntityListComponent {
  @Input() entities: any[] = []; // The array of entities to display
  @Input() displayedColumns: string[] = []; // The columns to display
  @Input() entityName: string = ''; // The name of the entity ("Facilities", "Residents", "Progress Notes")
  @Input() entityCreateRoute: string = ''; // The route to create a new entity
  @Input() entityEditRoute: string = ''; // The route to edit an entity
  @Input() deleteProgress: number = 0;

  @Output() deleteEntityFunction: EventEmitter<any> = new EventEmitter();
  // Call the custom delete function
  deleteEntity(entityId: number): void {
    this.deleteEntityFunction.emit(entityId);
  }
}
