import { ApiService, RouterService, ConfigService, ApiRequestConfig } from "@bdverse/bds-sdk-vue";
import { Member, SearchEntityResponse } from '@/models/bds';

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

    constructor(apiService: ApiService, routerService: RouterService, configService: ConfigService, options?: WebOptions) {
        this.apiService = apiService;
        this.routerService = routerService;
        this.configService = configService;
        this.options = options ? options : this.options;
    }

    async helloworld(options?: ApiRequestConfig): Promise<string | null> {
        const response = await this.apiService.get('api/sample/v1/helloworld', options);
        if (response) return response.data;
        return null;
    }

    async register(member: Member, password: string, options?: ApiRequestConfig): Promise<Member | null> {
        const request = {
            member: member,
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
                    "SAMPLE.Sport:name|localName|id|groupType|groupSize",
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