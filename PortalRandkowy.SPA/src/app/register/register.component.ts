import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/public_api';
import { User } from '../_models/user';
import { Router } from '@angular/router';


declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 
  @Output() cancelRegister: EventEmitter<boolean> = new EventEmitter();
   user: User;
   model: any = {};
   registerForm: FormGroup;
   DatepickerConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private alertifyService: AlertifyService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.DatepickerConfig = {
      containerClass: 'theme-blue'
    },
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]],
      confirmPassword: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      gender: ['Kobieta'],
      city: ['', Validators.required],
      country: ['', Validators.required],
      growth: [null]

    }, {validator: this.passwordMatchValidator});
  }


  passwordMatchValidator(fg: FormGroup) {
    return fg.get('password').value === fg.get('confirmPassword').value ? null : {missmatch: true};
  }
  register(){
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);

      this.authService.register(this.user).subscribe(s => {
        this.alertifyService.success("Rejestracja udana!");
      }, error => {alertify.error(error);
      }, () => {
        this.authService.login(this.user).subscribe( () => {
          this.router.navigate(['/użytkownicy']);
        })
      });
    }

   
    console.log(this.registerForm.value);
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertifyService.error("Anulowano");
  }
}
