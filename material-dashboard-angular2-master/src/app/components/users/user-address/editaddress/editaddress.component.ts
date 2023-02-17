import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AddressModel } from "app/models/user/address.models";
import { UserAddressService } from "app/services/useraddressservice";


@Component({
    selector: 'edit-address',
    templateUrl: './editaddress.component.html',
    styleUrls: ['./editaddress.component.css']
})

export class EditUserAddressComponent implements OnInit {

    address : AddressModel = new AddressModel;
    id = "";
    constructor(private addressService: UserAddressService,
        private route: ActivatedRoute, private router: Router){
            this.id = this.route.snapshot.queryParamMap.get('id')||"0";
        }

    ngOnInit(): void {
        this.getAddressByID();
    }

    getAddressByID(){
        this.addressService.getAddressByID(this.id).subscribe(response => {
            this.address = response;
        });
    }

    updateAddressSubmit() {
        this.addressService.updateAddress(this.address.id, this.address).subscribe(response =>{
            console.log(response);
            this.router.navigate(['/address']);
        });
    }
}