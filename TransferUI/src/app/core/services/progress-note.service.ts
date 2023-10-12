import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProgressNote, ProgressNoteDTO } from '../models/';
import { URL_ENDPOINT } from '../../shared/constant';

@Injectable({
  providedIn: 'root',
})
export class ProgressNoteService {
  constructor(private http: HttpClient) {}

  getAllProgressNotes(): Observable<ProgressNote[]> {
    return this.http.get<ProgressNote[]>(
      URL_ENDPOINT.PROGRESS_NOTE.GET_ALL_PROGRESS_NOTE
    );
  }

  getProgressNote(id: number): Observable<ProgressNote> {
    return this.http.get<ProgressNote>(
      URL_ENDPOINT.PROGRESS_NOTE.GET_PROGRESS_NOTE_BY_ID + id
    );
  }

  addProgressNote(progressNote: ProgressNoteDTO): Observable<ProgressNoteDTO> {
    return this.http.post<ProgressNoteDTO>(
      URL_ENDPOINT.PROGRESS_NOTE.ADD_PROGRESS_NOTE,
      progressNote
    );
  }

  updateProgressNote(
    id: number,
    progressNote: ProgressNoteDTO
  ): Observable<ProgressNoteDTO> {
    return this.http.put<ProgressNoteDTO>(
      URL_ENDPOINT.PROGRESS_NOTE.UPDATE_PROGRESS_NOTE_BY_ID + id,
      progressNote
    );
  }

  deleteProgressNote(id: number): Observable<void> {
    return this.http.delete<void>(
      URL_ENDPOINT.PROGRESS_NOTE.DELETE_PROGRESS_NOTE_BY_ID + id
    );
  }
}
