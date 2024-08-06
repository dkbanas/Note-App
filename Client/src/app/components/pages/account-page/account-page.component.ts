import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {Observable} from "rxjs";
import {IUser} from "../../../models/IUser";
import {AccountService} from "../../../services/account.service";

@Component({
  selector: 'app-account-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './account-page.component.html',
  styleUrl: './account-page.component.scss'
})

export class AccountPageComponent {
  accountForm!: FormGroup;
  passwordFieldType: string = 'password';
  currentUser$?: Observable<IUser | null>;
  editMode: { [key: string]: boolean } = {
    email: false,
    username: false,
    password: false
  };
  initialValues: { [key: string]: any } = {};

  constructor(private fb: FormBuilder, private accountService: AccountService) {
    this.currentUser$ = this.accountService.currentUser$;
  }

  ngOnInit(): void {
    this.createAccountForm();
    this.currentUser$!.subscribe(user => {
      if (user) {
        this.initialValues = {
          email: user.email,
          username: user.username,
          currentPassword: '',
          newPassword: ''
        };
        this.accountForm.patchValue(this.initialValues);
      }
    });
  }

  createAccountForm() {
    this.accountForm = new FormGroup({
      email: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.email]),
      username: new FormControl({ value: '', disabled: true }, [Validators.required]),
      currentPassword: new FormControl('', [Validators.required]),
      newPassword: new FormControl('', [Validators.required])
    });
  }

  enableEdit(field: string) {
    this.editMode[field] = true;
    this.accountForm.get(field)?.enable();
  }

  saveEdit(field: string) {
    this.editMode[field] = false;
    this.accountForm.get(field)?.disable();

    if (field === 'username') {
      this.updateUsername();
    } else if (field === 'password') {
      this.updatePassword();
    }
  }

  cancelEdit(field: string) {
    this.editMode[field] = false;
    this.accountForm.get(field)?.disable();

    if (field === 'username') {
      this.accountForm.get('username')?.setValue(this.initialValues['username']);
    } else if (field === 'password') {
      this.accountForm.get('currentPassword')?.setValue(this.initialValues['currentPassword']);
      this.accountForm.get('newPassword')?.setValue(this.initialValues['newPassword']);
    }
  }

  updateUsername() {
    const username = this.accountForm.get('username')?.value;
    this.accountService.updateUser({ username }).subscribe({
      next: () => {
        console.log('Username updated successfully');
        this.loadCurrentUser();
      },
      error: err => {
        console.log('Error updating username:', err);
      }
    });
  }

  updatePassword() {
    const currentPassword = this.accountForm.get('currentPassword')?.value;
    const newPassword = this.accountForm.get('newPassword')?.value;
    this.accountService.updatePassword({ currentPassword, newPassword }).subscribe({
      next: () => {
        console.log('Password updated successfully');
        this.loadCurrentUser();
      },
      error: err => {
        console.log('Error updating password:', err);
      }
    });
  }

  deleteAccount() {
    if (confirm('Are you sure you want to delete your account?')) {
      this.accountService.deleteAccount().subscribe({
        next: () => {
          console.log('Account deleted successfully');
          this.accountService.logout();
        },
        error: err => {
          console.log('Error deleting account:', err);
        }
      });
    }
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe();
  }

  togglePasswordVisibility() {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }

}
