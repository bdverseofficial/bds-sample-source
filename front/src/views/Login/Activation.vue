<template>
  <v-container pa-6 ma-6>Activation</v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";

@Component({
  name: "Activation"
})
export default class Activation extends Vue {
  @Prop()
  url?: string;

  @Prop()
  token?: string;

  loading = false;

  async mounted() {
    try {
      this.loading = true;
      await this.$app.authService.activation(this.token!);
      this.$app.routerService.login();
    } finally {
      this.loading = false;
    }
  }
}
</script>
