import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AccountService} from "../../../services/account.service";
import {CommonModule} from "@angular/common";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
 loginForm!: FormGroup;
 passwordFieldType: string = 'password';
  returnUrl!: string;
 constructor(private accountService:AccountService,private router:Router,private activatedRoute:ActivatedRoute) {
 }
 ngOnInit(){
   this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
   this.createLoginForm();
 }

 createLoginForm(){
   this.loginForm = new FormGroup({
     email: new FormControl('', [Validators.required,Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
     password: new FormControl('', [Validators.required])
   });
 }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
     next: () => { this.router.navigateByUrl('/Note'); },
      error: (err) => { console.log(err); }
    });
  }

  togglePasswordVisibility() {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }
}
