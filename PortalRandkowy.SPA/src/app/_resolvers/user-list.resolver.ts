import { Injectable } from '@angular/core';
    import { from, Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';


@Injectable()
export class UserListResolver implements Resolve<User[]> {
    
    pageNumber = 1;
    pageSize = 16;

    constructor(private userService: UserService, 
                 private router: Router, 
                 private alertify: AlertifyService) {}
                
    resolve(route: ActivatedRouteSnapshot): Observable<User[]>  {
        return this.userService.GetUsers(this.pageNumber, this.pageSize).pipe(
            catchError(error =>{
              this.alertify.error('Problem z pobraniem danych');
              this.router.navigate(['']);
              return of(null);
            })
        );
    }      

    }
    
