import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 
  @Output() cancelRegister: EventEmitter<boolean> = new EventEmitter();
   model: any = {};

  constructor(private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit(): void {
  }

  register(){
    this.authService.register(this.model).subscribe(s=> {
      this.alertifyService.success("Rejestracja udana!");
    }, error => {
      alertify.error(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertifyService.error("Anulowano");
  }
}
