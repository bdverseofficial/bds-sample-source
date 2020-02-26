/*
Sample of WebApp, must extend BdsApp from the bds-sdk-vue
*/
import { WebOptions, WebService } from './services/webService';
import { LocaleMessageObject } from 'vue-i18n';
import { BdsApp, BdsAppOptions, BdsError } from "@bdverse/bds-sdk-vue";
import { DialogService, DialogOptions } from './services/dialogService';
import { MessageService, MessageOptions } from './services/messageService';

// Add more options to the BdsAppOptions
export interface WebAppOptions extends BdsAppOptions {
    web?: WebOptions;    
    message?: MessageOptions;
    dialog?: DialogOptions;
}

// Define a store for some data
export interface WebAppStore {    
}

// Define the WebApp
export class WebApp extends BdsApp {

    // Define the dialogService
    public dialogService: DialogService;
    // Define the messageService
    public messageService: MessageService;
    // Define the webService
    public webService: WebService;    
    // Expose the options
    public options: WebAppOptions;

    // Create the store
    public store: WebAppStore = {        
    };

    // Create all needed services
    constructor(options: WebAppOptions) {
        super(options);

        this.messageService = new MessageService(this.configService, options.message);
        this.dialogService = new DialogService(this.configService, options.dialog);
        this.webService = new WebService(this.apiService, this.routerService, this.configService, options.web);             
        this.options = options;
    }

    // Override the error Handler to use the dialogService for displaying the error
    async errorHandler(context: string, error: BdsError): Promise<void> {
        const errorCode = (error.errorCode) ? error.errorCode + " : " : "";
        const errorMessage = (error.errorMessage) ? error.errorMessage : this.translationService.t("GLOBAL.ERROR");
        await this.dialogService.alert(errorCode + errorMessage, context);
    }

    // Intercept the need to get a language pack
    async getLang(lang: string): Promise<LocaleMessageObject | undefined> {
        const response = await this.apiService.get('/lang/' + lang + '.json', { baseURL: '/' });
        return response.data as LocaleMessageObject;
    }    

    // Override the init of the BdsApp to initialize our own services
    async init(): Promise<void> {
        await super.init();
        await this.webService.init();        
    }

    // Intercept the on Profile Changed to send the message to the webService
    async onProfileChanged(): Promise<void> {
        await super.onProfileChanged();
        await this.webService.initUser(this.profileService.store.me);
    }
}