import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Facility, FacilityDTO } from '../models/';
import { URL_ENDPOINT } from '../../shared/constant';

@Injectable({
  providedIn: 'root',
})
export class FacilityService {
  constructor(private http: HttpClient) {}

  getAllFacilities(): Observable<Facility[]> {
    return this.http.get<Facility[]>(URL_ENDPOINT.FACILITY.GET_ALL_FACILITY);
  }

  getFacility(id: number): Observable<Facility> {
    return this.http.get<Facility>(
      URL_ENDPOINT.FACILITY.GET_FACILITY_BY_ID + id
    );
  }

  addFacility(facility: FacilityDTO): Observable<FacilityDTO> {
    return this.http.post<FacilityDTO>(
      URL_ENDPOINT.FACILITY.ADD_FACILITY,
      facility
    );
  }

  updateFacility(id: number, facility: FacilityDTO): Observable<FacilityDTO> {
    return this.http.put<FacilityDTO>(
      URL_ENDPOINT.FACILITY.UPDATE_FACILITY_BY_ID + id,
      facility
    );
  }

  deleteFacility(id: number): Observable<void> {
    return this.http.delete<void>(
      URL_ENDPOINT.FACILITY.DELETE_FACILITY_BY_ID + id
    );
  }
}
