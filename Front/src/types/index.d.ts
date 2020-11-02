import Vue from 'vue';
import { WebApp } from "../webApp";

declare module 'vue/types/vue' {
    interface Vue {
        $app: WebApp;
    }
}