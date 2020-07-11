import { Injectable } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Message } from 'src/app/_models/message';
import { AuthService } from '../_services/auth.service';


@Injectable()
export class MessagesResolver implements Resolve<Message[]> {
    
    pageNumber = 1;
    pageSize = 12;
    messageContainer = 'Nieprzeczytane'; 

    constructor(private userService: UserService, 
                 private router: Router, 
                 private alertify: AlertifyService,
                 private authService: AuthService) {}
                
    resolve(route: ActivatedRouteSnapshot): Observable<Message[]>  {
        return this.userService.GetMessages(this.authService.decodedToken.nameid, this.pageNumber, 
                                            this.pageSize, this.messageContainer).pipe(
            catchError(error =>{
              this.alertify.error('Problem z wyszukiwaniem wiadomości');
              this.router.navigate(['/home']);
              return of(null);
            })
        );
    }    
    
    

    }
    
