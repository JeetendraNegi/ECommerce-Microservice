import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CardModel } from "app/models/user/card.models";
import { environment } from "environments/environment";
import { Observable } from "rxjs";


@Injectable({
    providedIn: 'root'
})

export class CardService {

    apiUrl = environment.APIUrl+'u/Card'
    token = localStorage.getItem("accessToken");
    header = new HttpHeaders({'Authorization': 'Bearer '+ this.token});

    constructor(private client: HttpClient){}

    getAllCards() : Observable<CardModel[]> {
        return this.client.get<CardModel[]>(this.apiUrl,{headers: this.header});
    }
}