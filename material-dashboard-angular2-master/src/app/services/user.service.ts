import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import {HttpClient, HttpHeaders } from '@angular/common/http';
import { UsersModel } from '../models/user/users.models';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  token = localStorage.getItem('accessToken');
  header = new HttpHeaders({'Authorization': 'Bearer ' + this.token});
  url = environment.APIUrl+"u/Users";
  constructor(private client : HttpClient) {
   }

  public GetAllUsers() : Observable<UsersModel[]> {
    return this.client.get<UsersModel[]>(this.url,{headers: this.header});
  }

  public AddUser(user : UsersModel) : Observable<UsersModel> {
    return this.client.post<UsersModel>(this.url, user, {headers : this.header});
  }

  public GetUserByID(id: string) : Observable<UsersModel> {
    return this.client.get<UsersModel>(this.url+"/"+id,{headers: this.header});
  }

  public UpdateUser(id: string, user: UsersModel) : Observable<UsersModel> {
    return this.client.put<UsersModel>(this.url+"?id="+id, user, {headers: this.header});
  }
}
