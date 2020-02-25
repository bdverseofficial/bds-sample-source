import { WebOptions, WebService } from './services/webService';
import { LocaleMessageObject } from 'vue-i18n';
import { BdsApp, BdsAppOptions, BdsError } from "@bdverse/bds-sdk-vue";
import { DialogService, DialogOptions } from './services/dialogService';
import { MessageService, MessageOptions } from './services/messageService';

export interface WebAppOptions extends BdsAppOptions {
    web?: WebOptions;    
    message?: MessageOptions;
    dialog?: DialogOptions;
}

export interface WebAppStore {
    drawer: boolean | null;
    drawerVisible: boolean | null;
}

export class WebApp extends BdsApp {

    public dialogService: DialogService;
    public messageService: MessageService;
    public webService: WebService;    

    public store: WebAppStore = {
        drawer: null,
        drawerVisible: false,
    };

    constructor(options: WebAppOptions) {
        super(options);

        this.messageService = new MessageService(this.configService, options.message);
        this.dialogService = new DialogService(this.configService, options.dialog);
        this.webService = new WebService(this.apiService, this.routerService, this.configService, options.web);             
    }

    async errorHandler(context: string, error: BdsError): Promise<void> {
        const errorCode = (error.errorCode) ? error.errorCode + " : " : "";
        const errorMessage = (error.errorMessage) ? error.errorMessage : this.translationService.t("GLOBAL.ERROR");
        await this.dialogService.alert(errorCode + errorMessage, context);
    }

    async getLang(lang: string): Promise<LocaleMessageObject | undefined> {
        const response = await this.apiService.get('/lang/' + lang + '.json', { baseURL: '/' });
        return response.data as LocaleMessageObject;
    }    

    async init(): Promise<void> {
        await super.init();
        await this.webService.init();        
    }

    async onProfileChanged(): Promise<void> {
        await super.onProfileChanged();
        await this.webService.initUser(this.profileService.store.me);
    }
}