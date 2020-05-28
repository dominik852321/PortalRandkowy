import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'users';


  constructor(private http: HttpClient) { }

  GetUsers(): Observable<User[]>{
    return this.http.get<User[]>(this.baseUrl);
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
  




}
