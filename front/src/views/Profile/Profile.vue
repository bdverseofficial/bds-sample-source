<template>
  <v-container pa-6 ma-6>
     <v-tabs>
      <v-tab :to="{name: 'Profile_Default'}">
          {{$t("PROFILE.MENU_ACCOUNT")}}
      </v-tab>
      <v-tab :to="{name: 'Profile_Security'}">
          {{$t("PROFILE.MENU_SECURITY")}}
      </v-tab>
     </v-tabs>
      <router-view></router-view>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop, Watch } from "vue-property-decorator";

@Component({
  name: "Profile",
})
export default class Profile extends Vue {}
</script>