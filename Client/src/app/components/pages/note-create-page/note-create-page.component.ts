import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {NotesService} from "../../../services/notes.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-note-create-page',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './note-create-page.component.html',
  styleUrl: './note-create-page.component.scss'
})
export class NoteCreatePageComponent {
  noteForm!:FormGroup;

  constructor(private noteService: NotesService,private router: Router) {}

  ngOnInit(){
    this.createNoteForm();
  }

  createNoteForm(){
    this.noteForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required])
    })
  }

  onSubmit() {
    this.noteService.createNote(this.noteForm.value).subscribe({
      next: () => { this.router.navigateByUrl('/Note'); },
      error: (err) => { console.log(err); }
    });
  }
}
