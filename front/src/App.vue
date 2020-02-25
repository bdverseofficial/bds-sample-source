<template>
  <div id="app" class="app">
    <v-app class="main-layout" heigth="100%">
      <v-content>
        <template v-if="$app.ready">
          <transition name="el-fade-in-linear" appear>
            <v-container fluid pa-0 ma-0 full-height>
              <v-app-bar dark clipped-left clipped-right app>
                <v-app-bar-nav-icon
                  v-if="appStore"
                  :disabled="!appStore.drawerVisible"
                  @click.stop="appStore.drawer = !appStore.drawer"
                ></v-app-bar-nav-icon>                               

                <template>
                  <v-toolbar-items>
                    <v-btn text :to="{name: 'Home'}">{{$t("APP.HOME")}}</v-btn>
                  </v-toolbar-items>

                  <v-toolbar-items>
                    <v-btn text :to="{name: 'About'}">{{$t("APP.About")}}</v-btn>
                  </v-toolbar-items>
                </template>

                <v-spacer></v-spacer>              

                <v-toolbar-items>
                  <v-menu offset-y>
                    <template v-slot:activator="{on}">
                      <v-btn icon v-on="on">
                        <v-icon>flag</v-icon>
                      </v-btn>
                    </template>
                    <v-list>
                      <v-list-item @click="changeLang('en')">
                        <v-list-item-title>English</v-list-item-title>
                      </v-list-item>
                      <v-list-item @click="changeLang('de')">
                        <v-list-item-title>German</v-list-item-title>
                      </v-list-item>
                      <v-list-item @click="changeLang('fr')">
                        <v-list-item-title>French</v-list-item-title>
                      </v-list-item>
                      <v-list-item @click="changeLang('it')">
                        <v-list-item-title>Italian</v-list-item-title>
                      </v-list-item>
                    </v-list>
                  </v-menu>
                </v-toolbar-items>

                <v-toolbar-items v-if="!profileStore.me">
                  <v-btn icon :to="{name: 'Login'}">
                    <v-icon>account_circle</v-icon>
                  </v-btn>
                </v-toolbar-items>

                <v-toolbar-items v-if="profileStore.me">
                  <v-menu offset-y left>
                    <template v-slot:activator="{on}">
                      <v-btn icon v-on="on">
                        <v-icon>account_circle</v-icon>
                      </v-btn>
                    </template>
                    <v-list>
                      <v-list-item :to="{name: 'Profile_Default'}">
                        <v-list-item-title>{{profileStore.me.displayName}}</v-list-item-title>
                      </v-list-item>
                      <v-list-item @click="signOut">
                        <v-list-item-title>Logout</v-list-item-title>
                      </v-list-item>
                    </v-list>
                  </v-menu>
                </v-toolbar-items>                
              </v-app-bar>
              <router-view class="content"></router-view>
            </v-container>
          </transition>
        </template>
        <MainDialog></MainDialog>
        <MainMessage></MainMessage>
      </v-content>
    </v-app>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { ProfileStore } from "@bdverse/bds-sdk-vue";
import { WebStore } from "./services/webService";
import { WebAppStore } from "./webApp";
import MainDialog from "./views/Dialog/MainDialog.vue";
import MainMessage from "./views/Dialog/MainMessage.vue";

@Component({
  name: "App",  
  components: { MainDialog, MainMessage }
})
export default class App extends Vue {
  profileStore: ProfileStore | null = null;
  webStore: WebStore | null = null;  
  appStore: WebAppStore | null = null;

  created() {
    this.profileStore = this.$app.profileService.store;
    this.webStore = this.$app.webService.store;
    this.appStore = this.$app.store;
  }

  async signOut() {
    this.$app.loadingService.usingLoading({}, async () => {
      await this.$app.authService.signOut(false);
    });
  } 

  async changeLang(locale: string) {
    await this.$app.setLocale(locale);
    window.location.reload();
  }
}
</script>