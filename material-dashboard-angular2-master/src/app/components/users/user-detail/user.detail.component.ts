import { Component, OnDestroy, OnInit } from '@angular/core';
import { UsersModel } from 'app/models/user/users.models';
import { UserService } from 'app/services/user.service';

@Component({
  selector: 'app-user.detail',
  templateUrl: './user.detail.component.html',
  styleUrls: ['./user.detail.component.css']
})
export class UserDetailComponent implements OnInit, OnDestroy {

  users : UsersModel[] = [];

  constructor(private userService : UserService) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  ngOnDestroy(): void {
      
  }

  getAllUsers() {
    this.userService.GetAllUsers().subscribe(response => {
      console.log(response);      
      this.users = response;
    }, 
    error => console.log('error',error)
    );
  }
}
