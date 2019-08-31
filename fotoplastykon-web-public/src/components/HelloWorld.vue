<template>
  <v-container class="flex flex-center">
    <v-row>
      <div class="col-8">
        <div class="row">
          <v-card class="my-card col-8">
              <div class="card-title">
                jakiś tytuł
              </div>
              <div class="card-title-line">
              </div>
              <div class="news-card-content">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
              </div>
          </v-card>
          <v-img class="news-img" src="@/assets/TheGodfather_02.jpg" contain>
          </v-img>
        </div>
        <div class="row">
          <v-img class="news-img-reflection" src="@/assets/once-upon-a-time-in-hollywood.jpg" contain>
          </v-img>
          <v-card class="my-card col-8">
              <div class="card-title">
                Lorem ipsum dolor sit ame
              </div>
              <div class="card-title-line">
              </div>
              <div class="news-card-content-reflection">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
              </div>
          </v-card>
        </div>
        <div class="row">
          <v-card class="my-card col-8">
            <div class="card-title">
                tytuł tytuł tytuł tytuł tytuł 
              </div>
              <div class="card-title-line">
              </div>
              <div class="news-card-content">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
              </div>
          </v-card>
          <v-img class="news-img" src="@/assets/intherain.jpg" contain>
          </v-img>
        </div>
        <div class="row">
          <v-img class="news-img-reflection" src="@/assets/looney.jpg" contain>
          </v-img>
          <v-card class="my-card col-8">
            <div class="card-title">
                tytuł tytuł tytuł tytuł tytuł 
              </div>
              <div class="card-title-line">
              </div>
              <div class="news-card-content-reflection">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
              </div>
          </v-card>
        </div>
                <div class="row">
          <v-card class="my-card col-8">
            <div class="card-title">
                tytuł tytuł tytuł tytuł tytuł 
              </div>
              <div class="card-title-line">
              </div>
              <div class="news-card-content">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
              </div>
          </v-card>
          <v-img class="news-img" src="@/assets/bird.jpg" contain>
          </v-img>
        </div>
        <div class="row">
          <v-img class="news-img-reflection" src="@/assets/TheGodfather_02.jpg" contain>
          </v-img>
          <v-card class="my-card col-8">
            <div class="card-title">
                tytuł tytuł tytuł tytuł tytuł 
              </div>
              <div class="card-title-line">
              </div>
              <div class="news-card-content-reflection">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
              </div>
          </v-card>
        </div>
      </div>
      <v-col>
      </v-col>
      <div class="float-right col-3 right-slider">
        <v-card class="vertical-slider">
        </v-card>
      </div>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  data: () => ({
    ecosystem: [
      {
        text: 'vuetify-loader',
        href: 'https://github.com/vuetifyjs/vuetify-loader',
      },
      {
        text: 'github',
        href: 'https://github.com/vuetifyjs/vuetify',
      },
      {
        text: 'awesome-vuetify',
        href: 'https://github.com/vuetifyjs/awesome-vuetify',
      },
    ],
    importantLinks: [
      {
        text: 'Documentation',
        href: 'https://vuetifyjs.com',
      },
      {
        text: 'Chat',
        href: 'https://community.vuetifyjs.com',
      },
      {
        text: 'Made with Vuetify',
        href: 'https://madewithvuejs.com/vuetify',
      },
      {
        text: 'Twitter',
        href: 'https://twitter.com/vuetifyjs',
      },
      {
        text: 'Articles',
        href: 'https://medium.com/vuetify',
      },
    ],
    whatsNext: [
      {
        text: 'Explore components',
        href: 'https://vuetifyjs.com/components/api-explorer',
      },
      {
        text: 'Select a layout',
        href: 'https://vuetifyjs.com/layout/pre-defined',
      },
      {
        text: 'Frequently Asked Questions',
        href: 'https://vuetifyjs.com/getting-started/frequently-asked-questions',
      },
    ],
  }),
});
</script>
