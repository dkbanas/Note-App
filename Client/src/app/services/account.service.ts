import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BehaviorSubject, map, Observable, ReplaySubject} from "rxjs";
import {IUser} from "../models/IUser";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:7055/"

  private currentUserSource = new ReplaySubject<IUser | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,private router: Router) {}

  loadCurrentUser(token: string | null): Observable<void> {

    if(token === null){
      this.currentUserSource.next(null);
      return new Observable<void>(observer => {
        observer.complete();
      });
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);

    return this.http.get<IUser>(this.baseUrl + 'Account',{headers}).pipe(
      map((user:IUser) => {
        if(user){
          localStorage.setItem('token',user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }
  login(values: any){
    return this.http.post<IUser>(this.baseUrl + 'Account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values:any){
    return this.http.post<IUser>(this.baseUrl + 'Account/register',values).pipe(
      map((user: IUser) => {
        if(user) {
          localStorage.setItem('token',user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email:string){
    return this.http.get(this.baseUrl + 'Account/emailexists?email=' + email);
  }

  updateUser(values: any) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.put(this.baseUrl + 'Account/updateUsername', values, { headers });
  }

  updatePassword(values: any) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.put(this.baseUrl + 'Account/updatePassword', values, { headers });
  }

  deleteAccount() {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.delete(this.baseUrl + 'Account/delete', { headers });
  }

}
