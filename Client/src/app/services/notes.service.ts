import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {IPagination} from "../models/IPagination";
import {map, Observable} from "rxjs";
import {noteParams} from "../models/noteParams";
import {INote} from "../models/INote";
import {INoteCreation} from "../models/INoteCreation";
import {IUser} from "../models/IUser";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  baseUrl = "https://localhost:7055/"
  constructor(public http: HttpClient) {}

  getNotes(noteParams: noteParams){

    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('token')}`
    });

    let params = new HttpParams();

    if(noteParams.search){
      params = params.append('search',noteParams.search);
    }
    params = params.append('sort',noteParams.sortSelected);
    params = params.append('pageIndex',noteParams.pageNumber.toString());
    params = params.append('pageSize',noteParams.pageSize.toString());


    return this.http.get<IPagination>(this.baseUrl + 'Notes',{ observe: 'response', params, headers })
      .pipe(
        map(response => {
          return response.body
        })
      );


  }

  getNote(id:number){

    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('token')}`
    });

    return this.http.get<INote>(this.baseUrl + 'Notes/' + id,{ headers });
  }

  createNote(values:any){
    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('token')}`
    });

    return this.http.post<INoteCreation>(this.baseUrl + 'Notes',values,{ headers });
  }

  deleteNote(id: number): Observable<void> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('token')}`
    });

    return this.http.delete<void>(`${this.baseUrl}Notes/${id}`, { headers });
  }

  updateNote(note: INote): Observable<INote> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.put<INote>(`${this.baseUrl}Notes/${note.id}`, note, { headers });
  }



}
