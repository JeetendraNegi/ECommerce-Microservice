import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AddressModel } from "app/models/user/address.models";
import { UsersModel } from "app/models/user/users.models";
import { UserService } from "app/services/user.service";
import { UserAddressService } from "app/services/useraddressservice";



@Component({
    selector: 'new-address',
    templateUrl: './newaddress.component.html',
    styleUrls: ['./newaddress.component.css']
})

export class NewAddressComponent implements OnInit {

    address: AddressModel = new AddressModel;
    users: UsersModel[] = [];

    constructor(private addressService: UserAddressService,
        private userService: UserService, private router: Router){}

    ngOnInit(): void {
        this.getAllUser();
    }

    getAllUser(){
        this.userService.GetAllUsers().subscribe(response => {
            this.users = response;
        });
    }

    newAddressSubmit() {
        console.log(this.address);
        this.addressService.addNewAddress(this.address).subscribe(response =>{
            console.log(response);
            this.router.navigate(['/address']);
        });
    }
}