import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { UsersModel } from "app/models/user/users.models";
import { UserService } from "app/services/user.service";


@Component({
    selector : 'view-user',
    templateUrl : './viewuser.component.html',
    styleUrls : ['./viewuser.component.css']
})

export class ViewUserComponent implements OnInit {

    user : UsersModel = new UsersModel;
    userId = "";
    constructor(private userService: UserService,
         private route : ActivatedRoute, private router: Router){
            this.userId = this.route.snapshot.queryParamMap.get('id')||"0";
            console.log(this.userId);
            
         }

    ngOnInit(): void {
        this.getUser();
    }

    getUser() {
        this.userService.GetUserByID(this.userId).subscribe(response => {
            console.log(response);
            this.user = response;
        });
    }
}