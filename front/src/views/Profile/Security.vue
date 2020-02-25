<template>
  <v-container fluid pa-3 ma-0>
    <v-row v-if="$app.profileService.store.me">
      <v-col cols="12">
        <h1>{{ $app.profileService.store.me.displayName }}</h1>
      </v-col>
    </v-row>
    <template v-if="!challengeMode">
      <v-form v-model="loginValid">
        <v-row>
          <v-col cols="6" xs="12">
            <v-text-field
              prepend-icon="account_box"
              :label="$t('USER.FIELD_LOGIN')"
              v-model="login"
              :rules="[ v => !!v || $t('PROFILE.LOGIN_REQUIRED')]"
            ></v-text-field>
          </v-col>
          <v-col cols="12">
            <v-btn
              color="primary"
              :disabled="!loginValid"
              @click="updateLogin"
              :loading="loginLoading"
              class="float-right"
            >{{$t("PROFILE.BTN_UPDATE_LOGIN")}}</v-btn>
          </v-col>
        </v-row>
      </v-form>
      <v-form v-model="emailValid">
        <v-row>
          <v-col cols="6" xs="12">
            <v-text-field
              prepend-icon="email"
              :label="$t('USER.FIELD_EMAIL')"
              v-model="email"
              :rules="[ v => !!v || $t('PROFILE.EMAIL_REQUIRED')]"
            ></v-text-field>
          </v-col>
          <v-col cols="12">
            <v-btn
              color="primary"
              :disabled="!emailValid"
              @click="updateEmail"
              :loading="emailLoading"
              class="float-right"
            >{{$t("PROFILE.BTN_UPDATE_EMAIL")}}</v-btn>
          </v-col>
        </v-row>
      </v-form>
      <v-form v-model="phoneValid" v-if="phone">
        <v-row>
          <v-col cols="6" xs="12">
            <v-select
              prepend-icon="language"
              :label="$t('USER.FIELD_COUNTRY_PHONE')"
              :items="$app.bdsService.store.countries"
              item-text="phoneExtensionName"
              item-value="phoneExtension"
              menu-props="auto"
              :rules="[ v => !!v || $t('PROFILE.PHONE_REQUIRED')]"
              v-model="phone.countryPhoneExt"
            ></v-select>
          </v-col>
          <v-col cols="6" xs="12">
            <v-text-field
              prepend-icon="phone"
              :label="$t('USER.FIELD_MOBILE_PHONE')"
              type="text"
              v-model="phone.number"
              :rules="[ v => !!v || $t('PROFILE.PHONE_REQUIRED')]"
            ></v-text-field>
          </v-col>
          <v-col cols="12">
            <v-btn
              color="primary"
              :disabled="!phoneValid"
              @click="updatePhone()"
              :loading="phoneLoading"
              class="float-right"
            >{{$t("PROFILE.BTN_UPDATE_PHONE")}}</v-btn>
          </v-col>
        </v-row>
      </v-form>
      <v-form v-model="twoFactorValid" v-if="twoFactorAuthentication">
        <v-row>
          <v-col cols="6" xs="12">
            <v-select
              prepend-icon="lock"
              :label="$t('PROFILE.FIELD_TWO_FACTOR_ACTIVATED')"
              :items="booleanValues"
              item-value="value"
              item-text="label"
              menu-props="auto"
              v-model="twoFactorAuthentication.activated"
            ></v-select>
          </v-col>
          <v-col cols="6" xs="12">
            <v-select
              prepend-icon="send"
              :label="$t('PROFILE.FIELD_TWO_FACTOR_METHOD')"
              :items="this.$app.authService.challengeMethods"
              v-model="twoFactorAuthentication.method"
              item-value="method"
              :item-text="methodName"
              menu-props="auto"
            ></v-select>
          </v-col>
          <v-col cols="12">
            <v-btn
              color="primary"
              :disabled="!twoFactorValid"
              @click="updateTwoFactor"
              :loading="twoFactorLoading"
              class="float-right"
            >{{$t("PROFILE.BTN_UPDATE_TWO_FACTOR")}}</v-btn>
          </v-col>
        </v-row>
      </v-form>
      <v-form v-model="passwordValid">
        <v-row>
          <v-col cols="6" xs="12">
            <v-text-field
              prepend-icon="lock"
              :label="$t('PROFILE.FIELD_OLD_PASSWORD')"
              v-model="oldPassword"
              :append-icon="showPassword ? 'visibility' : 'visibility_off'"
              :type="showPassword ? 'text' : 'password'"
              @click:append="showPassword = !showPassword"
              :rules="[ v => !!v || $t('PROFILE.PASSWORD_REQUIRED')]"
            ></v-text-field>
          </v-col>
          <v-col cols="6" xs="12"></v-col>
          <v-col cols="6" xs="12">
            <v-text-field
              prepend-icon="lock"
              :label="$t('PROFILE.FIELD_NEW_PASSWORD')"
              v-model="newPassword"
              :append-icon="showPassword ? 'visibility' : 'visibility_off'"
              :type="showPassword ? 'text' : 'password'"
              @click:append="showPassword = !showPassword"
              :rules="[ v => !!v || $t('PROFILE.PASSWORD_REQUIRED')]"
            ></v-text-field>
          </v-col>
          <v-col cols="6" xs="12">
            <v-text-field
              prepend-icon="lock"
              :label="$t('PROFILE.FIELD_NEW_PASSWORD_CHECK')"
              v-model="newPasswordCheck"
              :append-icon="showPassword ? 'visibility' : 'visibility_off'"
              :type="showPassword ? 'text' : 'password'"
              @click:append="showPassword = !showPassword"
              :rules="[ v => !!v || $t('PROFILE.PASSWORD_REQUIRED'), v => v === newPassword || $t('PROFILE.PASSWORD_NOT_CHECK')]"
            ></v-text-field>
          </v-col>
          <v-col cols="12">
            <v-btn
              color="primary"
              :disabled="!passwordValid"
              @click="updatePassword"
              :loading="passwordLoading"
              class="float-right"
            >{{$t("PROFILE.BTN_UPDATE_PASSWORD")}}</v-btn>
          </v-col>
        </v-row>
      </v-form>
    </template>
    <template v-else>
      <v-form v-model="challengeValid">
        <v-row>
          <v-col cols="12" xs="12">
            <v-text-field
              name="challengeCode"
              :label="$t('PROFILE.FIELD_CHALLENGE')"
              type="text"
              prepend-icon="lock"
              :rules="[ v => !!v || $t('PROFILE.CODE_REQUIRED')]"
              v-model="challengeCode"
            ></v-text-field>
          </v-col>
          <template v-if="challenge">
            <v-col cols="12" xs="12" v-for="method in challenge.methods" :key="method.method">
              <v-text-field
                :label="$t('LOGIN.TWOFACTOR_' + method.method.toUpperCase())"
                type="text"
                :readonly="true"
                v-model="method.target"
                append-icon="send"
                @click:append="getChallengeCode(method.method)"
              ></v-text-field>
            </v-col>
          </template>
          <v-col cols="12">
            <v-btn text @click="challengeMode = false">{{$t('PROFILE.CANCEL')}}</v-btn>
            <v-btn
              color="primary"
              :disabled="!challengeValid"
              @click="submitChallengeFunction"
              :loading="challengeLoading"
              class="float-right"
            >{{submitChallengeText}}</v-btn>
          </v-col>
        </v-row>
      </v-form>
    </template>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Mixins, Prop, Watch } from "vue-property-decorator";
import {
  Phone,
  TwoFactorAuthentication,
  Challenge,
  ChallengeMethod
} from "@bdverse/bds-sdk-vue";

@Component({
  name: "Security"
})
export default class Security extends Vue {
  loginValid: boolean = false;
  loginLoading: boolean = false;
  phoneValid: boolean = false;
  phoneLoading: boolean = false;
  emailValid: boolean = false;
  emailLoading: boolean = false;
  showPassword: boolean = false;
  passwordValid: boolean = false;
  passwordLoading: boolean = false;
  twoFactorValid: boolean = false;
  twoFactorLoading: boolean = false;
  submitChallengeText: string | null = null;
  submitChallengeFunction: any = null;
  challengeCode: string | null = null;
  challengeMode: boolean = false;
  challengeValid: boolean = false;
  challenge: Challenge | null = null;
  challengeLoading: boolean = false;

  login: string | null = null;
  oldPassword: string | null = null;
  newPassword: string | null = null;
  newPasswordCheck: string | null = null;
  email: string | null = null;
  phone: Phone | null = null;
  twoFactorAuthentication: TwoFactorAuthentication | null = null;

  get booleanValues() {
    return [
      {
        label: this.$app.translationService.t("GLOBAL.TRUE"),
        code: "true",
        value: true
      },
      {
        label: this.$app.translationService.t("GLOBAL.FALSE"),
        code: "false",
        value: false
      }
    ];
  }

  get twoFactorMethods() {
    return [
      {
        label: this.$app.translationService.t("LOGIN.TWOFACTOR_EMAIL"),
        code: "Email",
        value: "Email"
      },
      {
        label: this.$app.translationService.t("LOGIN.TWOFACTOR_SMS"),
        code: "SMS",
        value: "SMS"
      }
    ];
  }

  methodName(item: ChallengeMethod) {
    return this.$app.translationService.t(
      "PROFILE.FIELD_TWO_FACTOR_METHOD_" + item.method!.toUpperCase()
    );
  }

  async mounted() {
    await this.refresh();
  }

  async refresh() {
    if (this.$app.profileService.store.me) {
      let me = this.$app.profileService.store.me;
      this.login = me.login || null;
      this.email = me.email || null;
      this.phone = me.phone || {};
      this.twoFactorAuthentication = me.twoFactorAuthentication || {};
    }
  }

  private async ManageException(ex: any) {
    switch (ex.errorCode) {
      case "AUTHENTICATION_TWO_FACTOR_NEEDED":
        {
          this.challengeMode = true;
          if (ex.challenge) {
            this.challenge = ex.challenge;
          }
        }
        break;
      case "NEW_AUTHENTICATION_NEEDED":
        {
          await this.$app.authService.signOut(false);
          this.$app.routerService.login();
        }
        break;
      default:
        {
          this.$app.errorService.error("LogIn", ex);
        }
        break;
    }
  }

  private resetChallenge() {
    this.challenge = null;
    this.challengeMode = false;
  }

  async updateLogin() {
    if (this.login) {
      this.submitChallengeText = this.$app.translationService.t(
        "PROFILE.BTN_UPDATE_LOGIN"
      );
      this.submitChallengeFunction = this.updateLogin;
      let loginRequest = {
        challenge:
          this.challenge && this.challengeCode
            ? {
                hash: this.challenge.hash,
                date: this.challenge.date,
                code: this.challengeCode
              }
            : undefined,
        login: this.login
      };
      try {
        this.challengeLoading = true;
        this.loginLoading = true;
        await this.$app.profileService.updateLogin(loginRequest, {
          silentError: true
        });
        this.resetChallenge();
      } catch (ex) {
        await this.ManageException(ex);
      } finally {
        this.loginLoading = false;
        this.challengeLoading = false;
      }
    }
  }

  async updateEmail() {
    if (this.email) {
      this.submitChallengeText = this.$app.translationService.t(
        "PROFILE.BTN_UPDATE_EMAIL"
      );
      this.submitChallengeFunction = this.updateEmail;
      let emailRequest = {
        challenge:
          this.challenge && this.challengeCode
            ? {
                hash: this.challenge.hash,
                date: this.challenge.date,
                code: this.challengeCode
              }
            : undefined,
        email: this.email
      };
      try {
        this.challengeLoading = true;
        this.emailLoading = true;
        await this.$app.profileService.updateEmail(emailRequest, {
          silentError: true
        });
        this.resetChallenge();
      } catch (ex) {
        await this.ManageException(ex);
      } finally {
        this.emailLoading = false;
        this.challengeLoading = false;
      }
    }
  }

  async updatePassword() {
    if (this.oldPassword && this.newPassword) {
      this.submitChallengeText = this.$app.translationService.t(
        "PROFILE.BTN_UPDATE_PASSWORD"
      );
      this.submitChallengeFunction = this.updatePassword;
      let passwordRequest = {
        challenge:
          this.challenge && this.challengeCode
            ? {
                hash: this.challenge.hash,
                date: this.challenge.date,
                code: this.challengeCode
              }
            : undefined,
        oldPassword: this.oldPassword,
        newPassword: this.newPassword
      };
      try {
        this.challengeLoading = true;
        this.passwordLoading = true;
        await this.$app.profileService.updatePassword(passwordRequest, {
          silentError: true
        });
        this.resetChallenge();
      } catch (ex) {
        await this.ManageException(ex);
      } finally {
        this.challengeLoading = false;
        this.passwordLoading = false;
      }
    }
  }

  async updatePhone() {
    if (this.phone) {
      this.submitChallengeText = this.$app.translationService.t(
        "PROFILE.BTN_UPDATE_PHONE"
      );
      this.submitChallengeFunction = this.updatePhone;
      let phoneRequest = {
        challenge:
          this.challenge && this.challengeCode
            ? {
                hash: this.challenge.hash,
                date: this.challenge.date,
                code: this.challengeCode
              }
            : undefined,
        phone: this.phone
      };
      try {
        this.challengeLoading = true;
        this.phoneLoading = true;
        await this.$app.profileService.updatePhone(phoneRequest, {
          silentError: true
        });
        this.resetChallenge();
      } catch (ex) {
        await this.ManageException(ex);
      } finally {
        this.challengeLoading = false;
        this.phoneLoading = false;
      }
    }
  }

  async updateTwoFactor(event: any) {
    if (this.twoFactorAuthentication) {
      this.submitChallengeText = this.$app.translationService.t(
        "PROFILE.BTN_UPDATE_TWO_FACTOR"
      );
      this.submitChallengeFunction = this.updateTwoFactor;
      let twoFactorRequest = {
        challenge:
          this.challenge && this.challengeCode
            ? {
                hash: this.challenge.hash,
                date: this.challenge.date,
                code: this.challengeCode
              }
            : undefined,
        twoFactorAuthentication: this.twoFactorAuthentication
      };
      try {
        this.challengeLoading = true;
        this.twoFactorLoading = true;
        await this.$app.profileService.updateTwoFactor(twoFactorRequest, {
          silentError: true
        });
        this.resetChallenge();
      } catch (ex) {
        await this.ManageException(ex);
      } finally {
        this.challengeLoading = false;
        this.twoFactorLoading = false;
      }
    }
  }
}
</script>