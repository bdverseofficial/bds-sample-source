import Vue from 'vue';
import VueRouter, { RawLocation, Route } from 'vue-router';
import { WebApp } from "@/webApp";

declare module 'vue/types/vue' {
    interface Vue {
        $app: WebApp;
        $getLocation: any;
    }
}

declare global {
    interface Window {
        $router(to: RawLocation): void;
        H: any;
    }
}