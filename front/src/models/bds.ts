import { Reference, BdsEntity, User, UserEvent, Address } from "@bdverse/bds-sdk-vue";

export interface SearchRequest {
    searchKey?: string;
    query?: string;
    imLucky?: boolean;
    explain?: boolean;
    sortField?: string;
    sortDesc?: boolean;
    limit?: number;
    scroll?: boolean;
    scrollId?: string;
    filter?: string;
    filterFacetsIfNotCategoryLeaf?: boolean;
}

export interface SearchItem {
    item: BdsEntity;
}

export interface SearchParameters {
    sortField?: string;
    sortDesc?: boolean;
}

export interface SearchEntityResponse {
    items: SearchItem[];
    scrollId?: string;
    query?: SearchRequest;
    parameters?: SearchParameters;
}

export interface Phone {
    countryPhoneExt?: string;
    number?: string;
}

export interface Personal {
    mobilePhone?: Phone;
}

export interface Company {
    name?: string;
}

export interface Professional {
    workdPhone?: Phone;
    jobTitle?: string;
    company?: Company;
}

export interface Lead extends User {
    personal?: Personal;
    professional?: Professional;
    address?: Address;
}

export interface Account extends Lead {
}

export interface Person extends Account {
}

export interface B2CCustomer extends Person {
}