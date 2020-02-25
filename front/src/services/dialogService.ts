import { ConfigService } from "@bdverse/bds-sdk-vue";

export interface DialogOptions {
}

export interface DialogBtn {
    result: boolean;
    tag: any;
    text: string;
}

export interface Dialog {
    show: boolean;
    message: string;
    title: string;
    ok: DialogBtn;
    cancel?: DialogBtn;
    resolve: (value?: any) => void;
    reject: (reason?: any) => void;
}

export interface DialogStore {
    currentDialog?: Dialog;
}

export class DialogService {

    private options: DialogOptions = {
    };

    private configService: ConfigService;

    public store: DialogStore = {
    };

    constructor(configService: ConfigService, options?: DialogOptions) {
        this.configService = configService;

        this.store.currentDialog = undefined;
    }

    alert(message: string, title: string, oktext?: string): Promise<void> {
        return new Promise((resolve, reject) => {
            this.store.currentDialog = {
                show: true,
                message: message,
                title: title,
                ok: {
                    result: true,
                    tag: true,
                    text: oktext || "Ok",
                },
                resolve: resolve,
                reject: reject,
            };
        });
    }

    confirm(message: string, title: string, oktext?: string, canceltext?: string): Promise<void> {
        return new Promise((resolve, reject) => {
            this.store.currentDialog = {
                show: true,
                message: message,
                title: title,
                ok: {
                    result: true,
                    tag: true,
                    text: oktext || "Ok",
                },
                cancel: {
                    result: false,
                    tag: false,
                    text: canceltext || "Cancel",
                },
                resolve: resolve,
                reject: reject,
            };
        });
    }

    close(button: DialogBtn) {
        if (this.store.currentDialog) {
            this.store.currentDialog.show = false;
            if (button.result !== true) {
                this.store.currentDialog.reject(button);
            } else {
                this.store.currentDialog.resolve(button);
            }
        }
    }
}