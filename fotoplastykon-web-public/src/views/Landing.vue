<template>
  <v-content class="landing-page pa-0 mt-0">
    <app-alert></app-alert>
    <v-row class="pa-0">
      <v-col cols="1" class="pa-0">
        <div class="film-tape-landing">
        </div>
      </v-col>
      <v-col cols="11">
        <v-row class="mt-12">
          <v-col cols="8">
            <span class="headline text-uppercase font-weight-light">Fotoplastykon</span>
            <v-img class="landing-img" src="@/assets/main.png"></v-img>
          </v-col>
          <v-col cols="4">
            <v-row>
              <v-col cols="11">
                <v-btn class="float-right" text :to="{name:'home'}">
                  <span class="mr-2">Kontynuuj bez logowania</span>
                </v-btn>
              </v-col>
            </v-row>
            <v-card color="white">
              <v-tabs>
                <v-tabs-slider></v-tabs-slider>
                <v-tab :href="`#tab-login`">Zaloguj się</v-tab>
                <v-tab :href="`#tab-register`">Zarejestruj się</v-tab>
                <v-tab-item :value="'tab-login'">
                  <login-component></login-component>
                </v-tab-item>
                <v-tab-item :value="'tab-register'">
                  <register-component></register-component>
                </v-tab-item>
              </v-tabs>
            </v-card>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-content>
</template>

<script lang="ts">
  import Vue from "vue";
  import Component from "vue-class-component";
  import Login from '@/components/auth/Login.vue';
  import Register from '@/components/auth/Register.vue';
  import AppAlertComponent from '@/components/AppAlert.vue';

  @Component({
    components: {
      'login-component': Login,
      'register-component': Register,
      'app-alert': AppAlertComponent
    }
  })
  export default class LandingComponent extends Vue {

  }
</script>
