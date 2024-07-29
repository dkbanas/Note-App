import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IPagination} from "../models/IPagination";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  baseUrl = "https://localhost:7055/"
  constructor(public http: HttpClient) {}

  getNotes() {
    return this.http.get<IPagination>(this.baseUrl + 'Notes')
  }
}
