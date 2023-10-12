import { Resident } from './resident.model';

export interface ProgressNote {
  id: number;
  content: string;
  type: string;
  createdDate: string | null;
  residentId: number;
  resident?: Resident;
}
