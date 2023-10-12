import { Component, Input } from '@angular/core';

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

  // Define a method to handle entity deletion
  deleteEntity(entityId: number) {}
}
