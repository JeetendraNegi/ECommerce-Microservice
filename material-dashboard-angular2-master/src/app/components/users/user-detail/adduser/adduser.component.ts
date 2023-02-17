import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersModel } from 'app/models/user/users.models';
import { UserService } from 'app/services/user.service';

@Component({
  selector: 'app-user.detail',
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.css']
})
export class AddUserComponent implements OnInit {

  users : UsersModel = new UsersModel;

  constructor(private userService : UserService, private router : Router) {  }

  ngOnInit(): void {
    // this.userService.GetAllUsers().subscribe(response => {
    //   console.log(response);
    //   this.users = response;
    // })
  }

  addUserSubmit() {
    this.userService.AddUser(this.users).subscribe(response => {
      console.log(response);
      this.router.navigate(['/users']);
    });
    console.log(this.users);
  }
}