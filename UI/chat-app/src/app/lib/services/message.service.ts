import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { environment } from "src/environment/environment";
import { Participant, Message } from "../models";



@Injectable({
    providedIn: 'root'
})

export class MessageService { 
    public apiUrl = environment.webApiRoot+'/message';
    private readonly JOIN_ROOM = '/join/';
    roomParticipants$: BehaviorSubject<Map<number,Participant>> = new BehaviorSubject<Map<number,Participant>>(new Map<number,Participant>());
    currentParticipant$: BehaviorSubject<number> = new BehaviorSubject<number>(0);
    constructor(private httpClient: HttpClient) {
        

    }
    
    create(message: Message): Observable<number>{
        return this.httpClient.post<number>(this.apiUrl, message);
    }

}