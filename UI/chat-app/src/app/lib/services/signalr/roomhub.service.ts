import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { HubConnection } from "@microsoft/signalr";
import * as signalR from "@microsoft/signalr";
import { BehaviorSubject, Observable, of } from "rxjs";
import { map } from "rxjs/operators";
import { Subject } from "rxjs";
import { environment } from "src/environment/environment";
import { Message, Participant } from "../../models";

@Injectable({
  providedIn: 'root'
})
export class RoomHubService {
  private readonly signalRUrl: string = environment.signalRRoot+'/room';
  private hubConnection!: HubConnection;
  roomMessage$: BehaviorSubject<Message> = new BehaviorSubject<Message>(new Message());
  constructor() {
  }

  init(){
    let newConnection = false;
    if(this.hubConnection?.state !== signalR.HubConnectionState.Connected){
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(this.signalRUrl)
        .configureLogging(signalR.LogLevel.Information)
        .build();
      this.hubConnection.start().catch((err: { toString: () => any; }) => console.error(err.toString()));
      newConnection = true;
    }
    if(newConnection)
    {
      this.hubConnection.on('NewMessageBroadcast', message=>{
        this.roomMessage$.next(message);
      });
    }
  }
  

  joinRoomHub(participant: Participant) {
    this.init();
    this.hubConnection.invoke("JoinRoomAsync", participant);
  }
  



}