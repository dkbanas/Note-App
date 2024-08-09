import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
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
    RouterLinkActive,
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  currentUser$!: Observable<IUser | null>;
  navbarOpen = false;
  constructor(private accountService: AccountService) {
  }

  ngOnInit(){
    this.currentUser$ = this.accountService.currentUser$;
  }



  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }

  logout(){
    this.accountService.logout();
  }
}
