<template>
  <v-form v-if="account" v-model="valid">
    <v-container fluid pa-3 ma-0>
      <v-row>
        <v-col cols="12">
          <h1>{{ account.displayName }}</h1>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="6" xs="12">
          <v-text-field prepend-icon="edit" :label="$t('USER.FIELD_TITLE')" v-model="account.title"></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('USER.FIELD_FIRST_NAME')"
            v-model="account.firstName"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('USER.FIELD_MIDDLE_NAME')"
            v-model="account.middleName"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('USER.FIELD_LAST_NAME')"
            v-model="account.lastName"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-select
            prepend-icon="language"
            :label="$t('USER.FIELD_CULTURE')"
            :items="cultures"
            item-text="displayName"
            item-value="id"
            return-object
            v-model="account.culture"
          ></v-select>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('ADDRESS.FIELD_ATTENTION')"
            v-model="account.address.attention"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('ADDRESS.FIELD_STREET')"
            v-model="account.address.street"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('ADDRESS.FIELD_CITY')"
            v-model="account.address.city"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('ADDRESS.FIELD_ZIP')"
            v-model="account.address.postalCode"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-text-field
            prepend-icon="edit"
            :label="$t('ADDRESS.FIELD_STATE')"
            v-model="account.address.state"
          ></v-text-field>
        </v-col>
        <v-col cols="6" xs="12">
          <v-select
            name="country"
            prepend-icon="language"
            :label="$t('ADDRESS.FIELD_COUNTRY')"
            :items="countries"
            item-text="displayName"
            item-value="id"
            return-object
            v-model="account.address.country"
          ></v-select>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <v-btn
            color="primary"
            :disabled="!valid"
            @click="update"
            :loading="loading"
            class="float-right"
          >{{$t("PROFILE.BTN_UPDATE_ACCOUNT")}}</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop, Watch } from "vue-property-decorator";
import { User, Reference } from "@bdverse/bds-sdk-vue";

@Component({
  name: "Account"
})
export default class Account extends Vue {
  account: User | null = null;
  valid: boolean = false;
  loading: boolean = false;

  private async mounted(): Promise<void> {
    await this.refresh();
  }

  private get cultures(): Reference[] | null {
    return [
      { id: "en-US", displayName: "English (US)" },
      { id: "fr-FR", displayName: "French (FR)" },
      { id: "it-IT", displayName: "Italian (IT)" },
      { id: "de-DE", displayName: "German (DE)" }
    ];
  }

  private get countries(): Reference[] | null {
    return this.$app.bdsService.toReferencesOrDefault(
      this.$app.bdsService.store.countries
    );
  }

  private async refresh(): Promise<void> {
    if (this.$app.profileService.store.me) {
      let me = this.$app.profileService.store.me;
      this.account = {
        id: me.id,
        type: me.type,
        title: me.title,
        name: me.name,
        firstName: me.firstName,
        middleName: me.middleName,
        culture: me.culture,
        address: me.address || {},
        lastName: me.lastName
      };
    }
  }

  private async update(): Promise<void> {
    if (this.account) {
      try {
        this.loading = true;
        await this.$app.profileService.updateUserInfo(this.account);
        await this.refresh();
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>