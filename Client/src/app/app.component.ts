import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import {NavBarComponent} from "./components/partials/nav-bar/nav-bar.component";
import {HttpClient} from "@angular/common/http";
import {CommonModule} from "@angular/common";
import {LoadingComponent} from "./components/partials/loading/loading.component";
import {AccountService} from "./services/account.service";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TooltipModule, NavBarComponent, CommonModule, LoadingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Note';
  constructor(public http: HttpClient,private accountService: AccountService) { }

  ngOnInit(){
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe({
      next: () => {
        console.log('Loaded user');
      },
      error: err => {
        console.log('Error loading user:', err);
      }
    });
  }
}

