import { Component, OnInit } from "@angular/core";
import { AddressModel } from "app/models/user/address.models";
import { UserAddressService } from "app/services/useraddressservice";


@Component({
    selector: 'user-address',
    templateUrl: './useraddress.component.html',
    styleUrls: ['./useraddress.component.css']
})

export class UserAddressComponent implements OnInit {

    address : AddressModel[] = [];
    constructor(private userAddressService: UserAddressService) {}

    ngOnInit(): void {
        this.getAllUserAddress();
    }

    getAllUserAddress() {
        this.userAddressService.getAllUserAddress().subscribe(response => {
            this.address = response;                       
        });
    }

    deleteAddress(id: string) : void {
        console.log(id);
        
        this.userAddressService.deleteAddress(id);
        this.getAllUserAddress();       
    }
}