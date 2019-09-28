<template>
  <v-app>
    <!--<div class="landing">
      <v-app-bar class="app-bar-starter" :class="{ 'navbar--hidden': !showNavbarPicture }" app>
        <v-toolbar-title class="headline text-uppercase">
          <span class="font-weight-light">Fotoplastykon</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn v-if="$auth.check()" text @click="$auth.logout()">
          <span class="mr-2">Wyloguj się</span>
          <v-icon>mdi-open-in-new</v-icon>
        </v-btn>
        <v-btn v-else text :to="{name:'login'}">
          <span class="mr-2">Zaloguj się</span>
          <v-icon>mdi-open-in-new</v-icon>
        </v-btn>
        <v-btn text :to="{name:'register'}">
          <span class="mr-2">Zarejestruj się</span>
          <v-icon>mdi-open-in-new</v-icon>
        </v-btn>
      </v-app-bar>
      <v-img src="@/assets/main.png" class="align-center main-img">
      </v-img>
    </div>-->
<!--
    <v-app-bar class="app-bar-standard" :class="{ 'navbar&#45;&#45;hidden': showNavbarPicture }" app>
-->
    <v-app-bar class="app-bar-standard" app>
      <v-toolbar-title class="headline text-uppercase">
        <span class="font-weight-light">Fotoplastykon</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn v-if="$auth.check()" text @click="$auth.logout()">
        <span class="mr-2">Wyloguj się</span>
<!--
        <v-icon>mdi-open-in-new</v-icon>
-->
      </v-btn>
      <v-btn v-else text :to="{name:'login'}">
        <span class="mr-2">Zaloguj się</span>
<!--
        <v-icon>mdi-open-in-new</v-icon>
-->
      </v-btn>
      <v-btn text :to="{name:'register'}">
        <span class="mr-2">Zarejestruj się</span>
<!--
        <v-icon>mdi-open-in-new</v-icon>
-->
      </v-btn>
    </v-app-bar>
    <v-content style="margin-top:60px;">
      <div class="float-left film-tape">
      </div>
      <router-view/>
    </v-content>
    <!--<div class="footer">
      <v-app-bar class="app-bar-starter" :class="{ 'navbar--hidden': !showNavbarPicture }" app>
        <v-toolbar-title class="headline text-uppercase">
          <span class="font-weight-light">Fotoplastykon</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn
          text
          href="https://github.com/vuetifyjs/vuetify/releases/latest"
          target="_blank"
        >
          <v-btn v-if="$auth.check()" text @click="$auth.logout()">
            <span class="mr-2">Wyloguj się</span>
            <v-icon>mdi-open-in-new</v-icon>
          </v-btn>
          <v-btn v-else text :to="{name:'login'}">
            <span class="mr-2">Zaloguj się</span>
            <v-icon>mdi-open-in-new</v-icon>
          </v-btn>
          <v-btn text :to="{name:'register'}">
            <span class="mr-2">Zarejestruj się</span>
            <v-icon>mdi-open-in-new</v-icon>
          </v-btn>
        </v-btn>
      </v-app-bar>
    </div>-->
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component'

@Component({})
export default class AppComponent extends Vue
{
  private showNavbarPicture: boolean = true;
  private lastScrollPosition: number = 0;

/*  mounted ()
  {
    window.addEventListener('scroll', this.onScroll)
  }

  beforeDestroy ()
  {
    window.removeEventListener('scroll', this.onScroll)
  }

  onScroll ()
  {
    const currentScrollPosition = window.pageYOffset || document.documentElement.scrollTop

    let bottomOfWindow = Math.max(window.pageYOffset, document.documentElement.scrollTop, document.body.scrollTop) + window.innerHeight + 1 >= document.documentElement.offsetHeight

    this.showNavbarPicture = currentScrollPosition === 0 || bottomOfWindow
  }*/
}
</script>
