<template>
  <v-container pa-6 ma-6>
    <h2>{{$t('REGISTER.TITLE')}}</h2>
    <h3>{{$t('REGISTER.SUBTITLE')}}</h3>
    <v-form v-model="valid">
      <v-container fluid>
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              name="firstName"
              :label="$t('USER.FIELD_FIRST_NAME')"
              type="text"
              :rules="[ v => !!v || 'First Name is required']"
              v-model="member.firstName"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              name="lastName"
              :label="$t('USER.FIELD_LAST_NAME')"
              type="text"
              :rules="[ v => !!v || 'Last Name is required']"
              v-model="member.lastName"
            ></v-text-field>
          </v-col>         
          <v-col cols="12">
            <v-text-field
              name="email"
              :label="$t('USER.FIELD_EMAIL')"
              type="text"
              :rules="[ v => !!v || 'E-mail is required',  v => /.+@.+\..+/.test(v) || 'E-mail must be valid' ]"
              v-model="member.email"
            ></v-text-field>
          </v-col>         
          <v-col cols="12">
            <v-select
              name="country"
              :label="$t('ADDRESS.FIELD_COUNTRY')"
              :items="countries"
              item-text="displayName"
              item-value="id"
              return-object
              :rules="[ v => !!v || 'Country is required']"
              v-model="member.address.country"
            ></v-select>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              id="password1"
              name="password1"
              :label="$t('REGISTER.FIELD_PASSWORD')"
              type="password"
              v-model="password"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              id="password2"
              name="password2"
              :label="$t('REGISTER.FIELD_PASSWORD_REPEAT')"
              :rules="[ v => !!v || 'Password is required', v => v === password || 'Password are not matching' ]"
              type="password"
              v-model="password2"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-form>
    <div>
      <v-btn
        text
        @click="$app.routerService.back()"
        :disabled="loading"
        class="float-right"
      >{{$t("LOGIN.BTN_CANCEL")}}</v-btn>
      <v-btn
        color="primary"
        :loading="loading"
        :disabled="!valid"
        @click="register"
        class="float-right"
      >{{$t("REGISTER.BTN_REGISTER")}}</v-btn>
    </div>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { Member } from "@/models/bds";
import { Reference, BdsEntity, User } from "@bdverse/bds-sdk-vue";
import PageLayout from "../PageLayout.vue";

type Rule = (value: object) => boolean | string;
type Rules = Rule[];

@Component({
  name: "Register"
})
export default class Register extends Vue {
  loading: boolean = false;
  valid: boolean = false;

  password2: string | null = null;
  password: string | null = null;

  get countries(): Reference[] | null {
    return this.$app.bdsService.toReferencesOrDefault(
      this.$app.bdsService.store.countries
    );
  }

  private member: Member = {  
    address: {},
    type: "SAMPLE.Member"
  };

  async register() {
    if (this.valid) {
      this.loading = true;
      try {
        await this.$app.profileService.registerUser(this.member, this.password!);
        this.$app.routerService.back();
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>