<template>
  <v-container pa-6 ma-6>
    <v-form v-if="user">
      <v-text-field
        name="login"
        :label="$t('LOGIN.FIELD_EMAIL')"
        type="text"
        readonly
        v-model="user.login"
      ></v-text-field>
      <v-text-field
        id="password1"
        name="password1"
        :label="$t('LOGIN.FIELD_PASSWORD')"
        type="password"
        v-model="password1"
      ></v-text-field>
      <v-text-field
        id="password2"
        name="password2"
        :label="$t('LOGIN.FIELD_PASSWORD')"
        type="password"
        v-model="password2"
      ></v-text-field>
    </v-form>
    <template v-else>{{ $t("LOGIN.TOKEN_INVALID") }}</template>
    <div>
      <v-btn
        color="primary"
        @click="resetPassword"
        :disabled="!canResetPassword"
        class="float-right"
        >{{ $t("LOGIN.BTN_RESET") }}</v-btn
      >
    </div>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { User } from "@bdverse/bds-sdk";

@Component({
  name: "ResetPassword",
})
export default class ResetPassword extends Vue {
  @Prop()
  token?: string;

  user: User | null = {};

  private password1: string = "";
  private password2: string = "";

  private get canResetPassword(): unknown {
    return (
      this.password1 &&
      this.password2 &&
      this.password1 === this.password2 &&
      this.token
    );
  }

  private async resetPassword(): Promise<void> {
    if (this.canResetPassword) {
      try {
        await this.$app.authService.resetPassword({
          token: this.token!,
          password: this.password1,
        });
        this.$app.routerService.login();
      } catch {
        this.password1 = "";
        this.password2 = "";
      }
    }
  }

  private async mounted(): Promise<void> {
    try {
      if (this.token) {
        let user = await this.$app.authService.validateCommunicationToken(
          this.token
        );
        if (user) this.user = user;
      }
    } catch {
      this.user = null;
    }
  }
}
</script>