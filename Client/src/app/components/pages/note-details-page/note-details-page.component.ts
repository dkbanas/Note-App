import { Component } from '@angular/core';
import {INote} from "../../../models/INote";
import {NotesService} from "../../../services/notes.service";
import {CommonModule} from "@angular/common";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-note-details-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './note-details-page.component.html',
  styleUrl: './note-details-page.component.scss'
})
export class NoteDetailsPageComponent {
  note!: INote;
  constructor(private noteService: NotesService,private activatedRoute: ActivatedRoute,private router: Router) {}

  ngOnInit(){
  this.getNote();
}
  getNote() {
    this.noteService.getNote(+this.activatedRoute.snapshot.paramMap.get('id')!).subscribe({
      next: (note) => { this.note = note },
      error: (error) => console.error('There was an error!', error)
    });
  }

  deleteNote() {
    const noteId = +this.activatedRoute.snapshot.paramMap.get('id')!;
    console.log('Deleting note with ID:', noteId);
    if (isNaN(noteId)) {
      console.error('Invalid note ID:', noteId);
      return;
    }
    this.noteService.deleteNote(noteId).subscribe({
      next: () => {
        console.log('Note deleted successfully');
        this.router.navigate(['/Note']);
      },
      error: (error) => console.error('There was an error deleting the note!', error)
    });
  }
}
