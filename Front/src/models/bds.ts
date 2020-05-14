import { Reference, BdsEntity, User } from "@bdverse/bds-sdk-vue";

export interface Sport extends BdsEntity {
    
}

export interface Member extends User {
    preferedSport?: Reference;
}