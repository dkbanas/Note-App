import { Component } from '@angular/core';
import {AccordionComponent} from "../../partials/accordion/accordion.component";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-about-page',
  standalone: true,
  imports: [
    AccordionComponent,CommonModule
  ],
  templateUrl: './about-page.component.html',
  styleUrl: './about-page.component.scss'
})
export class AboutPageComponent {
  faqItems = [
    {
      question: 'How do I create a new note?',
      answer: 'Click the "Add Note" button available in the top navbar (only visible when logged in) and fill out the form with your note\'s details.',
      open: true
    },
    {
      question: 'How can I edit or delete a note?',
      answer: 'In the "My Notes" section, click on the note you wish to edit or delete. You\'ll be redirected to a detailed view where you can make changes or remove the note.',
      open: false
    },
    {
      question: 'How do I sort and search for notes?',
      answer: 'In the "My Notes" section, use the sort dropdown to arrange notes by date or title. The search bar allows you to find notes by keywords in their titles or descriptions.',
      open: false
    },
    {
      question: 'How can I update my account details?',
      answer: 'Navigate to the "Account" section where you can view and edit your email, username, and password.',
      open: false
    },
    {
      question: 'What technologies are used in this project?',
      answer: 'The app is built with .NET 8 for the backend, Angular 18.1.2 for the frontend, and Bootstrap for styling. It uses Entity Framework Core with PostgreSQL for database management.',
      open: false
    }
  ];
}
