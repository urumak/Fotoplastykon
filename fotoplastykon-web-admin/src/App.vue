<template>
  <v-app v-if="$auth.ready()">
    <component v-if="useLayout()" :is="layout()">
      <router-view/>
    </component>
    <router-view v-else/>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component'

@Component({})
export default class AppComponent extends Vue
{
  private default_layout = 'default';

  useLayout() {
    return (!this.$route.meta.noLayout);
  }

  layout() {
    return (this.$route.meta.layout || this.default_layout) + '-layout';
  }
}
</script>
