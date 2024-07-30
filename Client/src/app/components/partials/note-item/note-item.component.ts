import {Component, Input} from '@angular/core';
import {INote} from "../../../models/INote";
import {NotesService} from "../../../services/notes.service";
import {RouterModule} from "@angular/router";

@Component({
  selector: 'app-note-item',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './note-item.component.html',
  styleUrl: './note-item.component.scss'
})
export class NoteItemComponent {
  @Input() note!:INote;
  constructor() {}



  timeAgo(dateString:string): string{
    const now = new Date();
    const pastDate = new Date(dateString);
    const diffInMs = now.getTime() - pastDate.getTime();
    const diffInMinutes = Math.floor(diffInMs / (1000 * 60));

    if (diffInMinutes < 60) {
      return `${diffInMinutes} min${diffInMinutes !== 1 ? 's' : ''} ago`;
    }

    const diffInHours = Math.floor(diffInMinutes / 60);
    if (diffInHours < 24) {
      return `${diffInHours} hour${diffInHours !== 1 ? 's' : ''} ago`;
    }

    const diffInDays = Math.floor(diffInHours / 24);
    if (diffInDays < 31) {
      return `${diffInDays} day${diffInDays !== 1 ? 's' : ''} ago`;
    }

    const diffInMonths = Math.floor(diffInDays / 30);
    if (diffInMonths < 12) {
      return `${diffInMonths} month${diffInMonths !== 1 ? 's' : ''} ago`;
    }

    const diffInYears = Math.floor(diffInMonths / 12);
    return `${diffInYears} year${diffInYears !== 1 ? 's' : ''} ago`;
  }

}
