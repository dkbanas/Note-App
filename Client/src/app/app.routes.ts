import { Routes } from '@angular/router';
import {HomePageComponent} from "./components/pages/home-page/home-page.component";
import {NotePageComponent} from "./components/pages/note-page/note-page.component";

export const routes: Routes = [
  {path:'', component:HomePageComponent},
  {path:'Note',component:NotePageComponent},
];
