import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {NotesService} from "../../../services/notes.service";
import {ActivatedRoute, Router} from "@angular/router";
import {INote} from "../../../models/INote";

@Component({
  selector: 'app-note-edit-page',
  standalone: true,
    imports: [
        ReactiveFormsModule
    ],
  templateUrl: './note-edit-page.component.html',
  styleUrl: './note-edit-page.component.scss'
})
export class NoteEditPageComponent {
  noteForm!: FormGroup;
  noteId!: number;

  constructor(private noteService: NotesService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.noteId = +this.activatedRoute.snapshot.paramMap.get('id')!;
    this.createNoteForm();
    this.getNote();
  }

  createNoteForm(): void {
    this.noteForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required])
    });
  }

  getNote(): void {
    this.noteService.getNote(this.noteId).subscribe({
      next: (note: INote) => {
        this.noteForm.patchValue({
          title: note.title,
          description: note.description
        });
      },
      error: (error) => console.error('There was an error!', error)
    });
  }

  onSubmit(): void {
    if (this.noteForm.invalid) {
      return;
    }

    const updatedNote = {
      ...this.noteForm.value,
      id: this.noteId
    };

    this.noteService.updateNote(updatedNote).subscribe({
      next: () => this.router.navigate(['/Note', this.noteId]),
      error: (error) => console.error('There was an error updating the note!', error)
    });
  }
}
