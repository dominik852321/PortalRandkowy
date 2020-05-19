import { Injectable } from '@angular/core';
    import { from, Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { Route } from '@angular/compiler/src/core';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';


@Injectable()
export class UserEditResolver implements Resolve<User> {
    
    constructor(private userService: UserService, 
                 private router: Router, 
                 private alertify: AlertifyService,
                 private authService: AuthService) {}
               
           
    resolve(route: ActivatedRouteSnapshot): Observable<User>  {
        return this.userService.GetUser(this.authService.decodedToken.nameid).pipe(
            catchError(error =>{
              this.alertify.error('Problem z pobraniem danych');
              this.router.navigate(['']);
              return of(null);
            })
        );
    }

     
                 

    }
    
