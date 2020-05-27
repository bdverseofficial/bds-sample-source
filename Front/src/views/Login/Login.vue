<template>
  <v-container pa-6 ma-6>
    <template v-if="viewMode == 'SignIn'">
      <h2>{{$t('LOGIN.TITLE')}}</h2>
      <h3>{{$t('LOGIN.SUBTITLE_LOGIN')}}</h3>
      <v-form v-model="valid">
        <v-text-field
          prepend-icon="email"
          name="login"
          :label="$t('LOGIN.FIELD_EMAIL')"
          type="text"
          v-model="userName"
          :rules="[ v => !!v || $t('LOGIN.EMAIL_REQUIRED')]"
        ></v-text-field>
        <v-text-field
          id="password"
          name="password"
          prepend-icon="lock"
          :label="$t('LOGIN.FIELD_PASSWORD')"
          v-model="password"
          :append-icon="showPassword ? 'visibility' : 'visibility_off'"
          :type="showPassword ? 'text' : 'password'"
          :rules="[ v => !!v || $t('LOGIN.PASSWORD_REQUIRED')]"
          @keyup.enter.native="login"
          @click:append="showPassword = !showPassword"
        ></v-text-field>
        <v-switch v-model="rememberMe" :label="$t('LOGIN.REMEMBER_ME')"></v-switch>
      </v-form>
      <div style="clear:both">
        <v-btn text :to="{name: 'register'}">{{$t("LOGIN.SIGNIN")}}</v-btn>
        <v-btn text @click="viewMode = 'ForgotPassword'">{{$t("LOGIN.FORGOT_PASSWORD")}}</v-btn>
        <v-btn
          color="primary"
          @click="login"
          :disabled="!valid || loading"
          :loading="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_LOGIN")}}</v-btn>
      </div>
    </template>
    <template v-if="viewMode == 'ForgotPassword'">
      <h2>{{$t('LOGIN.TITLE')}}</h2>
      <h3>{{$t('LOGIN.SUBTITLE_FORGOT_PASSWORD')}}</h3>
      <v-form v-model="valid">
        <v-text-field
          name="login"
          prepend-icon="email"
          :label="$t('LOGIN.FIELD_EMAIL')"
          type="text"
          v-model="userName"
          :rules="[ v => !!v || $t('LOGIN.EMAIL_REQUIRED')]"
        ></v-text-field>
      </v-form>
      <div>
        <v-btn
          text
          @click="viewMode = 'SignIn'"
          :disabled="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_CANCEL")}}</v-btn>
        <v-btn
          color="primary"
          @click="forgot"
          :disabled="!valid || loading"
          :loading="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_FORGOT")}}</v-btn>
      </div>
    </template>
    <template v-if="viewMode == 'Challenge'">
      <h2>{{$t('LOGIN.TITLE')}}</h2>
      <h3>{{$t('LOGIN.SUBTITLE_CHALLENGE')}}</h3>
      <v-form v-model="valid">
        <v-text-field
          name="challengeCode"
          prepend-icon="lock"
          :label="$t('LOGIN.FIELD_CHALLENGE')"
          type="text"
          :rules="[ v => !!v || $t('LOGIN.CODE_REQUIRED')]"
          v-model="challengeCode"
          @keyup.enter.native="login"
        ></v-text-field>
        <template v-if="challenge">
          <v-text-field
            v-for="method in challenge.methods"
            :key="method.method"
            prepend-icon="cached"
            :label="$t('LOGIN.TWOFACTOR_' + method.method.toUpperCase())"
            type="text"
            :readonly="true"
            v-model="method.target"
            append-icon="send"
            @click:append="getChallengeCode(method.method)"
          ></v-text-field>
        </template>
      </v-form>
      <div>
        <v-btn
          text
          @click="viewMode = 'SignIn'"
          :disabled="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_CANCEL")}}</v-btn>
        <v-btn
          color="primary"
          @click="loginWithChallenge"
          :disabled="!valid || loading"
          :loading="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_LOGIN")}}</v-btn>
      </div>
    </template>
    <template v-if="viewMode == 'AccountLocked'">
      <h2>{{$t('LOGIN.TITLE')}}</h2>
      <h3>{{$t('LOGIN.SUBTITLE_RESET_PASSWORD')}}</h3>
      <h3>{{$t('LOGIN.ACCOUNT_LOCKED')}}</h3>
      <v-form v-model="valid">
        <v-text-field
          name="login"
          prepend-icon="email"
          :label="$t('LOGIN.FIELD_EMAIL')"
          type="text"
          :readonly="true"
          v-model="userName"
          :rules="[ v => !!v || $t('LOGIN.EMAIL_REQUIRED')]"
        ></v-text-field>
      </v-form>
      <div>
        <v-btn
          text
          @click="viewMode = 'SignIn'"
          :disabled="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_CANCEL")}}</v-btn>
        <v-btn
          color="primary"
          @click="sendActivation"
          :disabled="!valid || loading"
          :loading="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_SEND")}}</v-btn>
      </div>
    </template>
    <template v-if="viewMode == 'ResetPassword'">
      <h2>{{$t('LOGIN.TITLE')}}</h2>
      <h3>{{$t('LOGIN.SUBTITLE_RESET_PASSWORD')}}</h3>
      <v-form v-model="valid">
        <v-text-field
          id="newpassword"
          name="newpassword"
          prepend-icon="lock"
          :label="$t('LOGIN.FIELD_NEW_PASSWORD')"
          v-model="newPassword"
          :append-icon="showPassword ? 'visibility' : 'visibility_off'"
          :type="showPassword ? 'text' : 'password'"
          :rules="[ v => !!v || $t('LOGIN.PASSWORD_REQUIRED')]"
          @click:append="showPassword = !showPassword"
        ></v-text-field>
        <v-text-field
          id="newpassword2"
          name="newpassword2"
          prepend-icon="lock"
          :label="$t('LOGIN.FIELD_NEW_PASSWORD_CHECK')"
          v-model="newPasswordCheck"
          :append-icon="showPassword ? 'visibility' : 'visibility_off'"
          :type="showPassword ? 'text' : 'password'"
          :rules="[ v => !!v || $t('LOGIN.PASSWORD_REQUIRED'), v => v === newPassword || $t('LOGIN.PASSWORD_NOT_CHECK')]"
          @click:append="showPassword = !showPassword"
          @keyup.enter.native="reset"
        ></v-text-field>
      </v-form>
      <div>
        <v-btn
          text
          @click="viewMode = 'SignIn'"
          :disabled="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_CANCEL")}}</v-btn>
        <v-btn
          color="primary"
          @click="reset"
          :disabled="!valid || loading"
          :loading="loading"
          class="float-right"
        >{{$t("LOGIN.BTN_RESET")}}</v-btn>
      </div>
    </template>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { BdsError, Challenge } from "@bdverse/bds-sdk-vue";

type ViewMode =
  | "SignIn"
  | "Challenge"
  | "ResetPassword"
  | "ForgotPassword"
  | "AccountLocked";

@Component({
  name: "Login"
})
export default class Login extends Vue {
  @Prop()
  url?: string;

  @Prop({ default: false })
  selectProvider?: boolean;

  private showPassword: boolean = false;
  private valid: boolean = false;
  private userName: string | null = null;
  private password: string | null = null;
  private newPassword: string | null = null;
  private newPasswordCheck: string | null = null;
  private challengeCode: string | null = null;
  private rememberMe: boolean = true;
  private viewMode: ViewMode = "SignIn";
  private challenge: Challenge | null = null;
  private loading: boolean = false;

  private async mounted(): Promise<void> {
    await this.$app.authService.TryAutoAuth();
    if (this.$app.authService.store.isAuthenticated) {
      this.$app.routerService.replace(this.$props.url);
    }
  }

  private async getChallengeCode(method: string): Promise<void> {
    try {
      this.loading = true;
      let response = await this.$app.authService.getChallengeCode({
        userName: this.userName!,
        password: this.password!,
        method: method
      });
      if (response) {
        this.challenge = response;
      }
    } finally {
      this.loading = false;
    }
  }

  private async sendActivation(): Promise<void> {
    try {
      this.loading = true;
      await this.$app.authService.sendActivation({
        userName: this.userName!
      });
      this.viewMode = "SignIn";
    } catch {
      this.userName = "";
    } finally {
      this.loading = false;
    }
  }

  private async forgot(): Promise<void> {
    try {
      this.loading = true;
      await this.$app.authService.forgotPassword({
        userName: this.userName!
      });
      this.viewMode = "SignIn";
    } catch {
      this.userName = "";
    } finally {
      this.loading = false;
    }
  }

  private async reset(): Promise<void> {
    if (
      this.valid &&
      this.newPassword &&
      this.newPassword === this.newPasswordCheck
    ) {
      try {
        this.loading = true;
        await this.$app.authService.resetPassword(
          {
            login: {
              userName: this.userName!,
              password: this.password!
            },
            password: this.newPassword!
          },
          { silentError: true }
        );
        this.viewMode = "SignIn";
        this.newPassword = null;
        this.password = null;
        this.challengeCode = null;
        this.newPasswordCheck = null;
      } catch (ex) {
        this.$app.errorService.error("LogIn", ex);
        this.newPassword = null;
        this.newPasswordCheck = null;
      } finally {
        this.loading = false;
      }
    }
  }

  private async loginWithChallenge(): Promise<void> {
    if (
      this.valid &&
      this.password &&
      this.userName &&
      this.challengeCode &&
      this.challenge
    ) {
      try {
        this.loading = true;
        await this.$app.authService.signIn(
          {
            userName: this.userName!,
            password: this.password!,
            challenge: {
              hash: this.challenge.hash,
              date: this.challenge.date,
              code: this.challengeCode
            }
          },
          this.rememberMe,
          { silentError: true }
        );
        this.$app.routerService.replace(this.$props.url);
      } catch (ex) {
        switch (ex.errorCode) {
          case "DENIED_ACCESS": {
            this.$app.errorService.error("LogIn", {
              errorCode: "",
              errorMessage: this.$app.translationService.t(
                "LOGIN.CODE_INCORRECT"
              )
            });
            return;
          }
          case "AUTHENTICATION_PASSWORD_CHANGE":
            {
              this.viewMode = "ResetPassword";
            }
            break;
          default:
            {
              this.$app.errorService.error("LogIn", ex);
            }
            break;
        }
        this.challengeCode = null;
      } finally {
        this.loading = false;
      }
    }
  }

  private async login(): Promise<void> {
    if (this.valid && this.password && this.userName) {
      try {
        this.loading = true;
        await this.$app.authService.signIn(
          {
            userName: this.userName!,
            password: this.password!
          },
          this.rememberMe,
          { silentError: true }
        );
        this.$app.routerService.replace(this.$props.url);
      } catch (ex) {
        switch (ex.errorCode) {
          case "AUTHENTICATION_TWO_FACTOR_NEEDED":
            {
              this.viewMode = "Challenge";
              if (ex.challenge) {
                this.challenge = ex.challenge;
              }
            }
            break;
          case "AUTHENTICATION_PASSWORD_CHANGE":
            {
              this.viewMode = "ResetPassword";
            }
            break;
          case "LOCKED":
            {
              this.viewMode = "AccountLocked";
            }
            break;
          case "DENIED_ACCESS":
            {
              this.$app.errorService.error("Log In", {
                errorCode: "",
                errorMessage: this.$app.translationService.t("LOGIN.FAILED")
              });
              this.userName = null;
              this.password = null;
              this.challengeCode = null;
            }
            break;
          default:
            {
              this.$app.errorService.error("LogIn", ex);
              this.userName = null;
              this.password = null;
              this.challengeCode = null;
            }
            break;
        }
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>

