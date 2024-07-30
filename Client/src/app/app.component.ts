import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import {NavBarComponent} from "./components/partials/nav-bar/nav-bar.component";
import {HttpClient} from "@angular/common/http";
import {CommonModule} from "@angular/common";
import {LoadingComponent} from "./components/partials/loading/loading.component";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TooltipModule, NavBarComponent, CommonModule, LoadingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Note';
  constructor(public http: HttpClient) {}

}

