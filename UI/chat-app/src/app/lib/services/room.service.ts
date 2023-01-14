import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { environment } from "src/environment/environment";
import { Participant, Room } from "../models";



@Injectable({
    providedIn: 'root'
})
export class RoomService { 
    private readonly apiUrl = environment.webApiRoot+'/room';
    private readonly JOIN_ROOM = '/join/';
    roomParticipants$: BehaviorSubject<Map<number,Participant>> = new BehaviorSubject<Map<number,Participant>>(new Map<number,Participant>());
    currentParticipant$: BehaviorSubject<Participant> = new BehaviorSubject<Participant>(new Participant());
    constructor(private httpClient: HttpClient) {
        

    }
    
    create(room: Room): Observable<number>{
        return this.httpClient.post<number>(this.apiUrl, room);
    }

    getRooms():Observable<Array<Room>>{
        return this.httpClient.get<Array<Room>>(this.apiUrl);
    }

    joinRoom(participant: Participant):Observable<Room>{
        return this.httpClient.post<Room>(this.apiUrl + this.JOIN_ROOM, participant).pipe(tap(room => {
            var participantMap = new Map<number,Participant>();
            
            room.participants.forEach(x => {
                participantMap.set(x.id, x);
                if(x.name.toLowerCase() === participant.name.toLowerCase())
                {
                    this.currentParticipant$.next(x);
                }
            });
            this.roomParticipants$.next(participantMap);
        }));
    }
}