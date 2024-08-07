import {Component, Input} from '@angular/core';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-accordion',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './accordion.component.html',
  styleUrl: './accordion.component.scss'
})
export class AccordionComponent {
  @Input() items: { question: string, answer: string, open: boolean }[] = [];

  toggle(index: number) {
    this.items[index].open = !this.items[index].open;
  }
}
