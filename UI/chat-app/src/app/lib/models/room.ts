import { Message } from "./message";
import { Participant } from "./participant";

export class Room {
    id!: number;
    name!: string;
    messages!: Array<Message>;
    participants!: Array<Participant>;
}