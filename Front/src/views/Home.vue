<template>
  <v-container pa-6 ma-6>    
    <h1>Home</h1>
    <div>{{helloworld}}</div>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  name: "Home",  
})
export default class Home extends Vue {
  helloworld: string | null = null;

  private async mounted(): Promise<void> {
    this.helloworld = await this.$app.webService.helloworld();
  }
}
</script>
