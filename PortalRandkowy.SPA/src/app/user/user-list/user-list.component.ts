import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[];

  constructor(private userService: UserService, public alertify: AlertifyService) { }

  ngOnInit(): void {
    this.loadUsers()
  }

  loadUsers() {
    this.userService.GetUsers().subscribe((users: User[]) => {
      this.users = users;
      this.alertify.success("Pobrano użytkowników")
      
    }, error => {
      console.log(error);
      this.alertify.error("Nie udało się pobrać")
    });
  }
}
