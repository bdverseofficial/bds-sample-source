import { ConfigService } from "@bdverse/bds-sdk-vue";

export interface MessageOptions {
}

export interface MessageBtn {
    tag: any;
    text: string;
}

export enum MessagePosition {
    Bottom = 1,
    Top = 2,
    Left = 4,
    Right = 8
}

export interface Message {
    show: boolean;
    message: string;
    close: MessageBtn;
    timneout: number;
    position: number | MessagePosition;
}

export interface MessageStore {
    currentMessage?: Message;
}

export class MessageService {

    private options: MessageOptions = {
    };
    private configService: ConfigService;

    public store: MessageStore = {
    };

    constructor(configService: ConfigService, options?: MessageOptions) {
        this.configService = configService;

        this.store.currentMessage = undefined;
    }

    message(message: string, closetext?: string, timeout?: number): void {
        this.store.currentMessage = {
            show: true,
            message: message,
            position: MessagePosition.Bottom,
            timneout: timeout || 4000,
            close: {
                tag: true,
                text: closetext || "Close",
            },
        };
    }

    close(button: MessageBtn): void {
        if (this.store.currentMessage) {
            this.store.currentMessage.show = false;
        }
    }
}