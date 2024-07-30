import { Component } from '@angular/core';
import {BusyService} from "../../../services/busy.service";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss'
})
export class LoadingComponent {
  isLoading!: boolean;
  constructor(busyService: BusyService) {
    busyService.isLoading.subscribe((isLoading) => {
      this.isLoading = isLoading;
    });

  }
}
