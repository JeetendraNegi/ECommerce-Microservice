import { Component, OnInit } from "@angular/core";
import { CardModel } from "app/models/user/card.models";
import { CardService } from "app/services/card.service";


@Component({
    selector: 'user-card',
    templateUrl: './usercard.component.html',
    styleUrls: ['./usercard.component.css']
})

export class UserCardComponent implements OnInit {

    cards : CardModel[] = [];
    constructor(private cardService: CardService){}

    ngOnInit(): void {
        this.getAllCards();
    }

    getAllCards() {
        this.cardService.getAllCards().subscribe(res => {
            console.log(res);
            this.cards = res;
        });
    }
}