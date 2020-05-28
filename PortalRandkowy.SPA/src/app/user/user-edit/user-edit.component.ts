import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  user: User;
  photoUrl: string;
  @ViewChild('editForm') editForm: NgForm;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
     if(this.editForm.dirty) {
       $event.returnValue = true;
     }
  }

  constructor(private route: ActivatedRoute, 
              private alertify: AlertifyService, 
              private userService: UserService,
              private authService: AuthService) { }

  ngOnInit(): void { 
    this.route.data.subscribe(data => {
      this.user= data.user;
    });
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  
  }

  updateUser() {
    this.userService.UpdateUser(this.authService.decodedToken.nameid, this.user)
    .subscribe(read => {
      this.alertify.success("Profil zakutalizowany");
      this.editForm.reset(this.user);
    }, error => {
       this.alertify.error(error);
       console.error(error);
    });
  }

  updateMainPhoto(photoUrl) {
    this.user.photoUrl = photoUrl;
  }


}
