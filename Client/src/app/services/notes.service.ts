import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
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
    let params = new HttpParams();

    if(noteParams.search){
      params = params.append('search',noteParams.search);
    }
    params = params.append('sort',noteParams.sortSelected);
    params = params.append('pageIndex',noteParams.pageNumber.toString());
    params = params.append('pageSize',noteParams.pageSize.toString());


    return this.http.get<IPagination>(this.baseUrl + 'Notes',{observe: 'response',params})
      .pipe(
        map(response => {
          return response.body
        })
      );


  }

  getNote(id:number){
    return this.http.get<INote>(this.baseUrl + 'Notes/' + id);
  }
}
