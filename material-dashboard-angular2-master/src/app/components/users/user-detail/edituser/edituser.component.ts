import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersModel } from 'app/models/user/users.models';
import { UserService } from 'app/services/user.service';

@Component({
  selector: 'edit-user.detail',
  templateUrl: './edituser.component.html',
  styleUrls: ['./edituser.component.css']
})
export class EditUserComponent implements OnInit {

  user : any;
  id = "";

  constructor(private userService : UserService,
     private route: ActivatedRoute, private router: Router) {
    this.id = this.route.snapshot.queryParamMap.get('id')||"0";    
   }

  ngOnInit(): void {
    this.getUserByID();    
  }

  getUserByID() {
    this.userService.GetUserByID(this.id).subscribe(response => {
        console.log(response.firstName);
        this.user = response;
      })
  }

  updateUserSubmit(){
    console.log(this.user);
    this.userService.UpdateUser(this.user.id, this.user).subscribe(response=> {
        console.log('res:',response);
        this.router.navigate(['/users'])
    });
    
  }
}
