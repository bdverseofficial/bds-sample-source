<template>
  <v-dialog
    persistent
    v-if="dialogStore.currentDialog"
    v-model="dialogStore.currentDialog.show"
    width="500"
  >
    <v-card>
      <v-card-title v-if="dialogStore.currentDialog.title" primary-title>
        <h3>{{ dialogStore.currentDialog.title }}</h3>
      </v-card-title>

      <v-card-text>{{ dialogStore.currentDialog.message }}</v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          v-if="dialogStore.currentDialog.cancel"
          @click="close(dialogStore.currentDialog.cancel)"
          outline
          >{{ dialogStore.currentDialog.cancel.text }}</v-btn
        >
        <v-btn
          v-if="dialogStore.currentDialog.ok"
          color="primary"
          depressed
          @click="close(dialogStore.currentDialog.ok)"
          >{{ dialogStore.currentDialog.ok.text }}</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { DialogBtn, DialogStore } from "@bdverse/bds-sdk-vue";

@Component({
  name: "MainDialog",
})
export default class MainDialog extends Vue {
  dialogStore: DialogStore | null = null;

  private created(): void {
    this.dialogStore = this.$app.dialogService.store;
  }

  private close(button: DialogBtn): void {
    this.$app.dialogService.close(button);
  }
}
</script>