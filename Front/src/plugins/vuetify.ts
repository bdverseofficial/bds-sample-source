import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import colors from 'vuetify/lib/util/colors';
import en from 'vuetify/src/locale/en';
import { VuetifyPreset } from 'vuetify/types/services/presets';

Vue.use(Vuetify);

export default class BdsVuetify {

    vuetify: any;

    constructor() {
        this.vuetify = new Vuetify({
            lang: {
                current: 'en'
            },
            theme: {
                themes: {
                    light: {
                        primary: colors.blueGrey.base,
                        secondary: colors.blueGrey.lighten3,
                        accent: colors.teal.base,
                        error: colors.red.accent4
                    },
                    dark: {
                        primary: colors.blueGrey.base,
                        secondary: colors.blueGrey.lighten3,
                        accent: colors.teal.base,
                        error: colors.red.accent4
                    }
                },
                dark: false,
            },
            icons: {
                iconfont: "md",
            },
        });
    }
}
