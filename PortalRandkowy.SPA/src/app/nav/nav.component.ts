import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

declare let alertify: any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {};

  constructor(public authService: AuthService, private alertifyService: AlertifyService,
    private router: Router) { }

  ngOnInit(): void {

  }

  login(){
   this.authService.login(this.model).subscribe(s => {
      this.alertifyService.success('Zalogowałeś się do aplikacji');
   }, error => {
     this.alertifyService.error('Wystąpił błąd');
   }, () => {
     this.router.navigate(['/użytkownicy']);
   });
  }

  loggedIn(){
   return this.authService.loggedIn();
  }

  logOut(){
    localStorage.removeItem('token');
    this.alertifyService.message('Zostałeś wylogowany');
    this.router.navigate(['/home']);
  }

}
