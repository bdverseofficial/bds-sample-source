<template>
  <v-snackbar
    v-if="messageStore.currentMessage"
    v-model="messageStore.currentMessage.show"
    :bottom="(messageStore.currentMessage.position & 1) == 1"
    :top="(messageStore.currentMessage.position & 2) == 2"
    :right="(messageStore.currentMessage.position & 8) == 8"
    :left="(messageStore.currentMessage.position & 4) == 4"
    :timeout="messageStore.currentMessage.timeout"
  >
    {{ messageStore.currentMessage.message }}
    <v-btn flat @click="close(messageStore.currentMessage.close)">{{
      messageStore.currentMessage.close.text
    }}</v-btn>
  </v-snackbar>
</template>


<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { MessageBtn, MessageStore } from "@bdverse/bds-sdk-vue";

@Component({
  name: "MainMessage",
})
export default class MainMessage extends Vue {
  messageStore: MessageStore | null = null;

  private created(): void {
    this.messageStore = this.$app.messageService.store;
  }

  private close(button: MessageBtn): void {
    this.$app.messageService.close(button);
  }
}
</script>