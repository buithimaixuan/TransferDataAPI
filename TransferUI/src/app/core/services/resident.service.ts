import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Resident, ResidentDTO } from '../models/';
import { URL_ENDPOINT } from '../../shared/constant';

@Injectable({
  providedIn: 'root',
})
export class ResidentService {
  constructor(private http: HttpClient) {}

  getAllResidents(): Observable<Resident[]> {
    return this.http.get<Resident[]>(URL_ENDPOINT.RESIDENT.GET_ALL_RESIDENT);
  }

  getResident(id: number): Observable<Resident> {
    return this.http.get<Resident>(
      URL_ENDPOINT.RESIDENT.GET_RESIDENT_BY_ID + id
    );
  }

  addResident(resident: ResidentDTO): Observable<ResidentDTO> {
    return this.http.post<ResidentDTO>(
      URL_ENDPOINT.RESIDENT.ADD_RESIDENT,
      resident
    );
  }

  updateResident(id: number, resident: ResidentDTO): Observable<ResidentDTO> {
    return this.http.put<ResidentDTO>(
      URL_ENDPOINT.RESIDENT.UPDATE_RESIDENT_BY_ID + id,
      resident
    );
  }

  deleteResident(id: number): Observable<void> {
    return this.http.delete<void>(
      URL_ENDPOINT.RESIDENT.DELETE_RESIDENT_BY_ID + id
    );
  }
}
