<template>
  <q-layout view="hHh lpR fFf">

    <q-header class="bg-primary text-purple">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar>
            <img src="assets/icon.png">
          </q-avatar>
          Fotoplastykon
        </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <div class="absolute-left film-tape">
    </div>

    <q-page-container>
      <router-view />
    </q-page-container>

    <div class="footer">

    </div>

  </q-layout>
</template>

<script>
import { openURL } from "quasar";

export default {
  name: "MyLayout",
  data() {
    return {
      leftDrawerOpen: this.$q.platform.is.desktop,
      left: true,
      right: true
    };
  },
  methods: {
    openURL
  }
};
</script>

<style></style>
