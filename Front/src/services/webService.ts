import { ApiService, RouterService, ConfigService, ApiRequestConfig, ProfileService } from "@bdverse/bds-sdk-vue";
import { Member, SearchEntityResponse, Sport } from '@/models/bds';

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
    private routerService: RouterService;
    private configService: ConfigService;
    private profileService: ProfileService;

    constructor(apiService: ApiService, routerService: RouterService, profileService: ProfileService, configService: ConfigService, options?: WebOptions) {
        this.apiService = apiService;
        this.routerService = routerService;
        this.configService = configService;
        this.profileService = profileService;
        this.options = options ? options : this.options;
    }

    public async setPreferredSport(sport: Sport, options?: ApiRequestConfig): Promise<Member| null> {
        const response = await this.apiService.put('api/sample/v1/updateSport/' + sport.id, options);
        if (response) {
            this.profileService.store.me = response.data;
            return response.data;
        }
        return null
    }

    public async helloworld(options?: ApiRequestConfig): Promise<string | null> {
        const response = await this.apiService.get('api/sample/v1/helloworld', options);
        if (response) return response.data;
        return null;
    }

    public async register(member: Member, password: string, options?: ApiRequestConfig): Promise<Member | null> {
        const request = {
            user: member,
            password: password,
            identityProviderType: "Default",
        };
        const response = await this.apiService.post('api/bds/v1/users/register', request, options);
        if (response) return response.data;
        return null;
    }

    public async search(request: any): Promise<SearchEntityResponse | null> {
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
        const response = await this.apiService.post(
            "api/bds/v1/search",
            request,
            options
        );
        return response.data;
    }
}