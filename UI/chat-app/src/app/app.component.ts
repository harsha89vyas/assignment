import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { map, startWith } from 'rxjs/operators';
import { Participant, Room } from './lib/models';
import { RoomHubService, RoomService } from './lib/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'chat-app';
  public rooms: Array<Room> = [];
  public filteredRooms!: Observable<Array<Room>>;

  public roomNameInput = new FormControl<Room>(new Room());
  public participantNameInput = new FormControl();
  public joinedRoom!: Room;

  constructor(
    private _roomService: RoomService,
    private _roomHubService: RoomHubService
  ) {
  }
  ngOnInit(): void {
    this._roomHubService.init();
    this._roomService.getRooms().subscribe(data => {
      this.rooms = data;

    });
    this.filteredRooms = this.roomNameInput.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.rooms.slice();
      }),
    );
  }

  displayFn(room: Room): string {
    return room && room.name ? room.name : '';
  }

  private _filter(name: string): Room[] {
    const filterValue = name.toLowerCase();

    return this.rooms.filter(option => option.name.toLowerCase().includes(filterValue));
  }

  joinRoom(e: MouseEvent) {
    e.preventDefault();
    let participant = new Participant();
    participant.name = this.participantNameInput.value;
    participant.roomId = this.roomNameInput.value?.id ?? 0;
    this._roomService.joinRoom(participant).subscribe(room => {
      this.joinedRoom = room;
      console.log(this.joinedRoom);
      participant.id = room.participants.find(x => x.name.toLowerCase() === participant.name.toLowerCase())?.id ?? 0;
      this._roomHubService.joinRoomHub(participant);
    });
  }
  createRoom(e: MouseEvent){
    e.preventDefault();
    let room = new Room();
    room.name = typeof this.roomNameInput.value === "string" ? this.roomNameInput.value : "";
    this._roomService.create(room).subscribe(x=>{
      room.id = x;
      this.rooms.push(room);
      this.roomNameInput.setValue(room);
    })
  }
}
