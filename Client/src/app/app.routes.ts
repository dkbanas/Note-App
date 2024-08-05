import { Routes } from '@angular/router';
import {HomePageComponent} from "./components/pages/home-page/home-page.component";
import {NotePageComponent} from "./components/pages/note-page/note-page.component";
import {NoteDetailsPageComponent} from "./components/pages/note-details-page/note-details-page.component";
import {LoginPageComponent} from "./components/pages/login-page/login-page.component";
import {RegisterPageComponent} from "./components/pages/register-page/register-page.component";
import {authGuard} from "./guards/auth.guard";
import {NoteCreatePageComponent} from "./components/pages/note-create-page/note-create-page.component";

export const routes: Routes = [
  {path:'', component:HomePageComponent},
  {path:'Note',component:NotePageComponent,canActivate:[authGuard]},
  {path:'Note/:id', component:NoteDetailsPageComponent,canActivate:[authGuard]},
  {path:'CreateNote', component: NoteCreatePageComponent },
  {path:'Account/Login',component:LoginPageComponent},
  {path:'Account/Register',component:RegisterPageComponent},
  {path:'**', redirectTo:'',pathMatch:'full'},
];
