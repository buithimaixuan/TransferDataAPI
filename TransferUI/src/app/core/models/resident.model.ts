import { Facility } from './facility.model';
export interface Resident {
  id: number;
  firstName: string;
  lastName: string;
  doB: string | null;
  facilityId: number;
  facility?: Facility;
}
