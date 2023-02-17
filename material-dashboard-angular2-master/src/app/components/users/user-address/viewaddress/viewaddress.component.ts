import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AddressModel } from "app/models/user/address.models";
import { UsersModel } from "app/models/user/users.models";
import { UserService } from "app/services/user.service";
import { UserAddressService } from "app/services/useraddressservice";


@Component({
    selector: 'view-address',
    templateUrl: './viewaddress.component.html',
    styleUrls: ['./viewaddress.component.css']
})

export class ViewAddressComponent implements OnInit {

    id = "";
    userid = "";
    userAddress : AddressModel = new AddressModel;
    user : UsersModel = new UsersModel;
    constructor(private addressService: UserAddressService,
        private route: ActivatedRoute, private userService: UserService){
            this.id = this.route.snapshot.queryParamMap.get("id") || "0";
            this.userid = this.route.snapshot.queryParamMap.get("userid") || "0";
        }

    ngOnInit(): void {
        this.getAddressByID();  
        this.getUserFromUserID(this.userid);      
    }

    getAddressByID() : void {
        this.addressService.getAddressByID(this.id).subscribe(response=>{
            console.log(response);
            this.userAddress = response;                     
        });
    }

    getUserFromUserID(id: string) : void {
        console.log(id);
        
        this.userService.GetUserByID(id).subscribe(response => {
            this.user = response;
        });
    }
}