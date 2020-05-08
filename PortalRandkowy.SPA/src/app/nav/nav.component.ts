import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

declare let alertify: any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {};

  constructor(public authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit(): void {

  }

  login(){
   this.authService.login(this.model).subscribe(s => {
      this.alertifyService.success('Zalogowałeś się do aplikacji');
   }, error => {
     this.alertifyService.error('Wystąpił błąd');
   });
  }

  loggedIn(){
   return this.authService.loggedIn();
  }

  logOut(){
    localStorage.removeItem('token');
    this.alertifyService.success('Zostałeś wylogowany');
  }

}
