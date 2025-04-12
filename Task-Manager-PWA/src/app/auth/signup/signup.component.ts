import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service'; // Assuming AuthService handles registration

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  username: string = '';
  password: string = '';
  email:string='';
  confirmPassword: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  signup() {
    if (this.password === this.confirmPassword) {
      this.authService.signup(this.username, this.email, this.password).subscribe(
        (response) => {
          // Navigate to login after successful signup
          this.router.navigate(['/login']);
        },
        (error) => {
          this.errorMessage = 'Failed to create account, please try again.';
        }
      );
    } else {
      this.errorMessage = 'Passwords do not match!';
    }
  }
}