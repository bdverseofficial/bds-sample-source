/*
Sample of WebApp, must extend BdsApp from the bds-sdk-vue
*/
import { BdsVueApp, BdsVueAppOptions } from '@bdverse/bds-sdk-vue';
import { WebOptions, WebService } from './services/webService';

// Add more options to the BdsAppOptions
export interface WebAppOptions extends BdsVueAppOptions {
    web?: WebOptions;
}

// Define a store for some data
export interface WebAppStore {
}

// Define the WebApp
export class WebApp extends BdsVueApp {

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

        this.webService = new WebService(this.apiService, this.configService, this.searchService, this.profileService, this.genericEntityService, options.web);
        this.options = options;
    }
}