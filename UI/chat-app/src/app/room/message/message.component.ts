import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { map, startWith } from 'rxjs/operators';
import { RoomService } from 'src/app/lib/services';
import { Message, Participant } from '../../lib/models';

@Component({
    selector: 'chat-message',
    templateUrl: './message.component.html',
    styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit, OnDestroy {

    @Input()
    public message!: Message;
    public currentParticipant: Participant = new Participant();
    public messageInput = new FormControl();
    public participants: Map<number, Participant> = new Map<number, Participant>();
    public subscriptions: Subscription[] = [];
    constructor(private _roomService: RoomService) {
    }
    ngOnDestroy(): void {
        this.subscriptions.forEach(x => {
            if (!x.closed) {
                x.unsubscribe();
            }
        });
    }
    ngOnInit(): void {
        this.subscriptions.push(this._roomService.roomParticipants$.subscribe(participants => {
            this.participants = participants;
        }));
        this.subscriptions.push(this._roomService.currentParticipant$.subscribe(participant => {
            this.currentParticipant = participant;
        }));
    }


}
