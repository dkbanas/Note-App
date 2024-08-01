import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {Observable} from "rxjs";
import {IUser} from "../../../models/IUser";
import {AccountService} from "../../../services/account.service";
import {AsyncPipe, CommonModule} from "@angular/common";

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    RouterLink,
    AsyncPipe,
    CommonModule,
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  currentUser$!: Observable<IUser | null>;
  constructor(private accountService: AccountService) {
  }

  ngOnInit(){
    this.currentUser$ = this.accountService.currentUser$;
  }

  logout(){
    this.accountService.logout();
  }
}
