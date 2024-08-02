import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {IPagination} from "../models/IPagination";
import {map, Observable} from "rxjs";
import {noteParams} from "../models/noteParams";
import {INote} from "../models/INote";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  baseUrl = "https://localhost:7055/"
  constructor(public http: HttpClient) {}

  getNotes(noteParams: noteParams){

    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('token')}` // Adjust according to your token storage method
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
      Authorization: `Bearer ${localStorage.getItem('token')}` // Adjust according to your token storage method
    });

    return this.http.get<INote>(this.baseUrl + 'Notes/' + id,{ headers });
  }
}
