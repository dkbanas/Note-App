import { Routes } from '@angular/router';
import {HomePageComponent} from "./components/pages/home-page/home-page.component";
import {NotePageComponent} from "./components/pages/note-page/note-page.component";
import {NoteDetailsPageComponent} from "./components/pages/note-details-page/note-details-page.component";
import {LoginPageComponent} from "./components/pages/login-page/login-page.component";
import {RegisterPageComponent} from "./components/pages/register-page/register-page.component";
import {authGuard} from "./guards/auth.guard";
import {NoteCreatePageComponent} from "./components/pages/note-create-page/note-create-page.component";
import {NoteEditPageComponent} from "./components/pages/note-edit-page/note-edit-page.component";
import {AccountPageComponent} from "./components/pages/account-page/account-page.component";
import {AboutPageComponent} from "./components/pages/about-page/about-page.component";

export const routes: Routes = [
  {path:'', component:HomePageComponent},
  {path:'Note',component:NotePageComponent,canActivate:[authGuard]},
  {path:'Note/:id', component:NoteDetailsPageComponent,canActivate:[authGuard]},
  {path:'CreateNote', component: NoteCreatePageComponent },
  { path: 'EditNote/:id', component: NoteEditPageComponent },
  {path:'Account',component:AccountPageComponent},
  {path:'Account/Login',component:LoginPageComponent},
  {path:'Account/Register',component:RegisterPageComponent},
  {path:'About',component:AboutPageComponent},
  {path:'**', redirectTo:'',pathMatch:'full'},
];
