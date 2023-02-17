import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AddressModel } from "app/models/user/address.models";
import { environment } from "environments/environment";
import { Observable } from "rxjs";


@Injectable({
    providedIn: 'root'
})

export class UserAddressService {

    apiURL = environment.APIUrl+'u/Address';
    token = localStorage.getItem('accessToken');
    header = new HttpHeaders({'Authorization': 'Bearer ' + this.token})

    constructor(private client: HttpClient){}

    getAllUserAddress() : Observable<AddressModel[]> {
        return this.client.get<AddressModel[]>(this.apiURL, {headers: this.header});
    }

    getAddressByID(id : string) : Observable<AddressModel> {
        return this.client.get<AddressModel>(this.apiURL+"/"+id, {headers: this.header});
    }

    updateAddress(id: string, address: AddressModel) : Observable<AddressModel> {
        return this.client.put<AddressModel>(this.apiURL+"?id="+id, address, {headers: this.header});
    }

    addNewAddress(address: AddressModel) : Observable<AddressModel> {
        return this.client.post<AddressModel>(this.apiURL,address, {headers: this.header} );
    }

    deleteAddress(id: string) : void {
        this.client.delete(`${this.apiURL}?id=${id}`, {headers: this.header});
    }
}