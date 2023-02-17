import { AddressModel } from "./address.models";
import { CardModel } from "./card.models";

export class UsersModel{

    id = "";
    firstName = "";
    lastName = "";
    contactNo = "";
    userAddress : AddressModel[] = [];
    cardDetail : CardModel[] = [];    
}