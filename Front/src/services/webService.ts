import { ApiService, ApiRequestConfig, ProfileService, SearchService, SearchEntityResponse, SearchRequest, SearchEntityRequest, GenericEntityService, BdsEntity } from "@bdverse/bds-sdk";
import { Member, Sport } from '@/models/bds';
import { ConfigService } from '@bdverse/bds-sdk';

export interface WebStore {
}

export interface WebOptions {

}

export class WebService {

    private options: WebOptions = {
    };

    public store: WebStore = {
    };

    private apiService: ApiService;
    private profileService: ProfileService;
    private entityService: GenericEntityService;
    private searchService: SearchService;
    private configService: ConfigService;

    constructor(apiService: ApiService, configService: ConfigService, searchService: SearchService, profileService: ProfileService, entityService: GenericEntityService, options?: WebOptions) {
        this.apiService = apiService;
        this.configService = configService;
        this.profileService = profileService;
        this.searchService = searchService;
        this.entityService = entityService;
        this.options = options ? options : this.options;
    }

    public async setPreferredSport(sport: Sport, options?: ApiRequestConfig): Promise<Member | null> {
        if (this.profileService.store.me) {
            let config = this.configService.configuration as any;
            let response: BdsEntity | null = null;
            if (config.pluginMode) {
                response = (await this.apiService.put('api/sample/v1/updateSport/' + sport.id, options)).data;
            }
            else {
                const request = { id: this.profileService.store.me.id, type: this.profileService.store.me.type, preferredSport: sport.key };
                response = await this.entityService.PartialUpdate(request, options);
            }
            if (response) {
                this.profileService.store.me = response;
                return response;
            }
        }
        return null
    }

    public async helloworld(options?: ApiRequestConfig): Promise<string | null> {
        let config = this.configService.configuration as any;
        if (config.pluginMode) {
            const response = await this.apiService.get('api/sample/v1/helloworld', options);
            if (response) return response.data;
        }
        else {
            return "Hello World";
        }
        return null;
    }

    public async search(request: SearchEntityRequest): Promise<SearchEntityResponse | null> {
        let options = {
            headers: {
                filters: [
                    "Facet:name|localName",
                    "FacetValue:name|localName",
                    "SAMPLE.SportCategory:name|localName",
                    "SAMPLE.Sport:key|name|localName|id|groupType|groupSize",
                ]
            }
        };
        return await this.searchService.searchFullText(request, options);
    }
}