import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginationResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'users';


  constructor(private http: HttpClient) { }

  GetUsers(page?, itemsPerPage?, userParams?, likesParam?): Observable<PaginationResult<User[]>>{

    const paginationResult: PaginationResult<User[]> = new PaginationResult<User[]>();
    let params = new HttpParams();

    if(page != null && itemsPerPage !=null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (userParams != null ) {
      params = params.append('minAge', userParams.minAge);
      params = params.append('maxAge', userParams.maxAge);
      params = params.append('gender', userParams.gender);
      params = params.append('orderBy', userParams.orderBy);
    }

    if (likesParam === 'UserLikes') {
      params = params.append('UserLikes', 'true');
    }
    if (likesParam === 'UserIsLiked') {
      params = params.append('UserIsLiked', 'true');
    }


    return this.http.get<User[]>(this.baseUrl, {observe: 'response', params})
    .pipe(
       map(response => {
         paginationResult.result = response.body;
        if(response.headers.get('Pagination') != null) {
          paginationResult.pagination = JSON.parse(response.headers.get('Pagination'))
        }

        return paginationResult;
       })
    )
  }

  GetUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl +'/'+ id);
  }

  UpdateUser(id: number, user: User) {
    return this.http.put(this.baseUrl +'/'+ id, user);
  }
  SetMainPhoto(id : number, idPhoto: number) {
    return this.http.post(this.baseUrl +'/'+ id +'/photos/'+ idPhoto + '/SetMain', {});
  }

  DeletePhoto(id : number, idPhoto: number) {
    return this.http.delete(this.baseUrl + '/' + id + '/photos/' + idPhoto );
  }

  SendLike(id: number, recipientId: number) {
    return this.http.post(this.baseUrl + '/'  + id + '/like/' + recipientId, {});
  }

  GetMessages(id: number, page?, itemsPerPage?, messageContainer?) {
    const paginationResult: PaginationResult<Message[]> = new PaginationResult<Message[]>();

    let params = new HttpParams();

    params = params.append('MessageContainer', messageContainer);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Message[]>(this.baseUrl +"/"+ id + "/messages", { observe: 'response', params })
      .pipe(
        map(response => {
          paginationResult.result = response.body;

          if (response.headers.get('Pagination') != null) {
            paginationResult.pagination = JSON.parse(response.headers.get('Pagination'))
          }

          return paginationResult;
        })
      )

  }

  GetMessageThread(id: number, recipientId: number) {
    return this.http.get<Message[]>(this.baseUrl + "/" + id + "/messages/thread/" + recipientId);
  }
  
}
