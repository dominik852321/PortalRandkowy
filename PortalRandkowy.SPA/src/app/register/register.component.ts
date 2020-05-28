import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/public_api';


declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 
  @Output() cancelRegister: EventEmitter<boolean> = new EventEmitter();
   model: any = {};
   registerForm: FormGroup;
   DatepickerConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private alertifyService: AlertifyService, private fb: FormBuilder) { }

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
    // this.authService.register(this.model).subscribe(s=> {
    //   this.alertifyService.success("Rejestracja udana!");
    // }, error => {
    //   alertify.error(error);
    // });
    console.log(this.registerForm.value);
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertifyService.error("Anulowano");
  }
}
