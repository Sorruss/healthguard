import { Component } from '@angular/core';
import { AuthService, User } from '../../services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  isLoggedIn: boolean = this.authService.isLoggedIn();
  userInfo: User = new User();
  userId: number = 0;

  constructor(public authService: AuthService) {
    if (this.isLoggedIn) {
      this.userInfo = this.authService.getUserInfo()!;

      this.authService.getCurrentUser(this.userInfo.role).subscribe(data => {
        if ("patientId" in data) {
          this.userId = data.patientId;
        } else if ("doctorId" in data) {
          this.userId = data.doctorId;
        } else if ("companyManagerId" in data) {
          this.userId = data.companyManagerId;
        }
      });
    }
  }

  logout() {
    this.authService.logout();
  }

}
