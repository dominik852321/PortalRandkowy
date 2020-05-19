import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from './alertify.service';

@Injectable()
export class Errorinterceptor implements HttpInterceptor{
    
    constructor(public alertify: AlertifyService) {}
    
    intercept(req: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>> {
     return next.handle(req).pipe(
         catchError(error => {
             if(error instanceof HttpErrorResponse) 
             {
               const applicationError = error.headers.get('Application-Error');

               if(applicationError)
               {
                   this.alertify.error(applicationError);
                   return throwError(applicationError);
               }

               const serverError = error.error.errors;
               let errors = '';

               if(serverError && typeof serverError == "object")
               {
                   for(const key in serverError)
                   {
                       if(serverError[key]) 
                       {
                           errors += serverError[key]+ ', \n';
                       }
                   }
               }
               return throwError(errors || serverError || 'Server Error');
             }
         })
     );
    }

}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: Errorinterceptor,
    multi: true
}