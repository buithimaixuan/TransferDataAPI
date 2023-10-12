import { Facility } from './facility.model';
import { Resident } from './resident.model';
import { ProgressNote } from './progress-note.model';

export type FacilityDTO = Omit<Facility, 'id'>;
export type ResidentDTO = Omit<Resident, 'id'>;
export type ProgressNoteDTO = Omit<ProgressNote, 'id'>;
