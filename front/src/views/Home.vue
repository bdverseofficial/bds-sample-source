<template>
  <div class="home">    
    <h1>Home</h1>
    <div>{{helloworld}}</div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  name: "Home",  
})
export default class Home extends Vue {
  helloworld: string | null = null;

  async mounted() {
    this.helloworld = await this.$app.webService.helloworld();
  }
}
</script>
