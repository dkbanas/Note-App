import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AccountService} from "../../../services/account.service";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [
    NgIf,
    ReactiveFormsModule
  ],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss'
})
export class RegisterPageComponent {
  registerForm!: FormGroup;
  passwordFieldType: string = 'password';
  serverError: string | null = null;
  constructor(private fb: FormBuilder, private router:Router, private accountService:AccountService) {}

  ngOnInit(){
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required,Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('', [Validators.required])
    })
  }

  togglePasswordVisibility() {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    this.accountService.register(this.registerForm.value).subscribe({
      next: () => {
        this.router.navigateByUrl('/Note');
      },
      error: (err) => {
        if (err.error?.errors?.length) {
          this.serverError = err.error.errors[0];
        } else {
          this.serverError = 'An error occurred. Please try again later.';
        }
      }
    });
  }

}
