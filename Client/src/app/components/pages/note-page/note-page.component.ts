import {Component, ElementRef, ViewChild} from '@angular/core';
import {INote} from "../../../models/INote";
import {NotesService} from "../../../services/notes.service";
import {CommonModule, NgForOf} from "@angular/common";
import {NoteItemComponent} from "../../partials/note-item/note-item.component";
import {noteParams} from "../../../models/noteParams";
import {PaginationModule} from "ngx-bootstrap/pagination";

@Component({
  selector: 'app-note-page',
  standalone: true,
  imports: [
    NgForOf,
    NoteItemComponent,
    PaginationModule,CommonModule,
  ],
  templateUrl: './note-page.component.html',
  styleUrl: './note-page.component.scss'
})
export class NotePageComponent {
  @ViewChild('search',{static:false}) searchTerm!: ElementRef;
  notes!:INote[];
  noteParams = new noteParams();
  totalCount!: number;
  sortOptions = [
    {name:'Newest', value: 'date'},
    {name:'Oldest', value: 'date-reverse'},
    {name:'A-Z',value:'alphabetically'},
    {name:'Z-A',value:'alphabetically-reverse'}
  ];

  constructor(private notesService: NotesService ) {}

  ngOnInit() {
    this.getNotes();
  }

  getNotes(){
    this.notesService.getNotes(this.noteParams).subscribe({
        next: (response) => {
          this.notes = response!.data;
          this.noteParams.pageNumber = response!.pageIndex;
          this.noteParams.pageSize = response!.pageSize;
          this.totalCount = response!.count;

          },
        error: (error) => console.error('There was an error!', error)
      }
    )
  }

  onSortSelected(sort:string){
    this.noteParams.sortSelected = sort;
    this.getNotes();
  }

  onPageChange(event: any){
    if(this.noteParams.pageNumber !== event){
      this.noteParams.pageNumber = event.page;
      this.getNotes();
    }
  }

  onSearch(){
    this.noteParams.search = this.searchTerm.nativeElement.value;
    this.getNotes();
  }

  onReset(){
    this.noteParams = new noteParams();
    this.getNotes();
  }


}
