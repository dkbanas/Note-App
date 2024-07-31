import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, map} from "rxjs";
import {IUser} from "../models/IUser";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:7055/"

  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,private router: Router) {}

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

}
