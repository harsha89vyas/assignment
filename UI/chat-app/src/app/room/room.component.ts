import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { map, startWith } from 'rxjs/operators';
import { Message, Participant, Room } from '../lib/models';
import { RoomHubService, RoomService } from '../lib/services';
import { MessageService } from '../lib/services/message.service';

@Component({
  selector: 'chat-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {

  @Input()
  public room!: Room;
  public messageInput = new FormControl();
  public subscriptions: Subscription[] = [];
  public currentParticipant: Participant = new Participant();
  constructor(
    private _roomService: RoomService,
    private _messageService: MessageService,
    private _roomHubService: RoomHubService
  ) {
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(x => {
      if (!x.closed) {
        x.unsubscribe();
      }
    });
  }
  ngOnInit(): void {
    this.subscriptions.push(
      this._roomService.currentParticipant$.subscribe(participant => {
        this.currentParticipant = participant;
      }));
    this.subscriptions.push(
      this._roomHubService.roomMessage$.subscribe(message => {
        if(message?.id)
        {
          this.room.messages.push(message);
        }
      }));
  }

  sendMessage(e: MouseEvent) {
    e.preventDefault();
    let message = new Message();
    message.participantId = this.currentParticipant.id;
    message.roomId = this.room.id;
    message.text = this.messageInput.value;
    this._messageService.create(message).subscribe(id => {
      message.id = id;
    });
  }

}
