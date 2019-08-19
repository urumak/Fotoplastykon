<template>
  <v-container class="flex flex-center">
    
    <div class="absolute-right right-slider">
      <div class="vertical-slider">
      </div>
    </div>
    <div class="news-content">
      <div class="row">
      <v-card class="my-card col-8" style="background: #9C27B0">
          <div style="width:75%;text-align:center;">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.

Maecenas congue eleifend nisi, quis fringilla sapien vulputate vel. Quisque id metus ac ante tincidunt ullamcorper. Duis dui purus, interdum ut lacus in, ornare iaculis eros. Donec ut erat in mauris consequat pretium non eu sapien. Quisque dictum accumsan cursus. Nulla iaculis justo ac lectus mattis, in dictum diam viverra. Integer eu dui erat. In sit amet venenatis est. Cras eros ex, varius quis ultricies a, vestibulum a justo. Donec ac semper justo. Mauris facilisis vel risus sed vestibulum. Curabitur et est arcu. Nunc rhoncus enim vitae lobortis tristique. Vivamus vel tincidunt ipsum, ultrices ultrices sem.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
          </div>
      </v-card>
      <v-img class="news-img" src="@/assets/once-upon-a-time-in-hollywood.jpg" contain>
      </v-img>
    </div>
    <div class="row">
      <v-card class="my-card col-8" style="background: #9C27B0">
        <v-img class="news-img" src="@/assets/once-upon-a-time-in-hollywood.jpg" contain>
        </v-img>
          <div style="width:75%;text-align:center;">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.

Maecenas congue eleifend nisi, quis fringilla sapien vulputate vel. Quisque id metus ac ante tincidunt ullamcorper. Duis dui purus, interdum ut lacus in, ornare iaculis eros. Donec ut erat in mauris consequat pretium non eu sapien. Quisque dictum accumsan cursus. Nulla iaculis justo ac lectus mattis, in dictum diam viverra. Integer eu dui erat. In sit amet venenatis est. Cras eros ex, varius quis ultricies a, vestibulum a justo. Donec ac semper justo. Mauris facilisis vel risus sed vestibulum. Curabitur et est arcu. Nunc rhoncus enim vitae lobortis tristique. Vivamus vel tincidunt ipsum, ultrices ultrices sem.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu commodo molestie. Integer non mollis est, vel interdum eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien semper fermentum.
          </div>
      </v-card>
    </div>
    <div class="row">
      <v-card class="my-card">2
          dfdf
          sdfsdfsdfsdf
          sdfsd
      </v-card>
    </div>
    <div class="row">
      <v-card class="my-card">
          dfdf
          sdfsdfsdfsdf
          sdfsd
      </v-card>
    </div>
    </div>
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
