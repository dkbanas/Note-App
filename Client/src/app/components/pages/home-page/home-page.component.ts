import { Component } from '@angular/core';
import {CarouselComponent} from "../../partials/carousel/carousel.component";
import {CommonModule} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    CarouselComponent, CommonModule, RouterLink,
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {
  userOpinions = [
    { title: 'John Doe', text: 'This app has transformed how I manage my daily tasks and notes. Highly recommend it!' },
    { title: 'Jane Smith', text: 'A user-friendly app with excellent features. It\'s a game-changer for note-taking.' },
    { title: 'Michael Brown', text: 'Secure and easy to use. The best note app I\'ve used so far.' },
    { title: 'Alice Johnson', text: 'Perfect for organizing my work and personal notes. Love it!' },
    { title: 'Robert Wilson', text: 'The search and sort functions make it so easy to find my notes.' },
    { title: 'Emily Davis', text: 'A reliable app for all my note-taking needs. Highly recommend!' }
  ];
}
