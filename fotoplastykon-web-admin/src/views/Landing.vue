<template>
  <v-content class="landing-page">
    <app-alert></app-alert>
    <div class="float-left film-tape">
    </div>
    <v-app-bar app>
      <v-toolbar-title class="headline text-uppercase">
        <span class="font-weight-light">Fotoplastykon</span>
      </v-toolbar-title>
    </v-app-bar>
    <div style="height: 80px"></div>
    <v-row style="margin-bottom: -80px">
      <div class="col-8"><v-img class="landing-img" src="@/assets/main.png"></v-img></div>
      <div class="col-4 float-right">
        <v-card color="white" style="margin-top: -40px">
          <v-tabs>
            <v-tabs-slider></v-tabs-slider>
            <v-tab :href="`#tab-login`">Zaloguj się</v-tab>
            <v-tab-item :value="'tab-login'">
              <login-component></login-component>
            </v-tab-item>
          </v-tabs>
        </v-card>
      </div>
    </v-row>
  </v-content>
</template>

<script lang="ts">
  import Vue from "vue";
  import Component from "vue-class-component";
  import Login from '@/views/Auth/Login.vue';
  import Register from '@/views/Auth/Register.vue';
  import AppAlertComponent from '@/components/AppAlert.vue';

  @Component({
    components: {
      'login-component': Login,
      'app-alert': AppAlertComponent
    }
  })
  export default class LandingComponent extends Vue {

  }
</script>
