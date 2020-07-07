import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  genderList = [{value: 'mężczyzna', display: 'Mężczyzni'},
                {value: 'kobieta', display: 'Kobiety'}];
  userParams: any = {};              
  pagination: Pagination;


  constructor(private userService: UserService, public alertify: AlertifyService, public route: ActivatedRoute) { }

  ngOnInit(): void {
   this.route.data.subscribe(data =>{ 
      this.users = data.users.result;
      this.pagination = data.users.pagination;
    });
    this.userParams.gender = this.user.gender === 'Kobieta' ? 'Mężczyzna' : 'Kobieta';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 100;
    this.userParams.orderBy = 'lastActive';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.gender = this.user.gender === 'Kobieta' ? 'Mężczyzna' : 'Kobieta';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 100;
    this.userParams.orderBy = 'lastActive';
    this.loadUsers();
  }

  loadUsers() {
     this.userService.GetUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
     .subscribe((res: PaginationResult<User[]>) => {
      this.users = res.result;
      this.pagination =res.pagination;
    }, error => {
      console.log(error);
      this.alertify.error("Nie udało się pobrać")
    });
   }
}
