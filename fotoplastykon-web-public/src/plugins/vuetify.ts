import Vue from 'vue';
import vuetify from 'vuetify';
import Vuetify from 'vuetify/lib';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
      options: {
        customProperties: true,
      },
    themes: {
      light: {
        primary: '#b458f3',
        secondary: '#424242',
        accent: '#b458f3',
        error: '#FF5252',
        info: '#b458f3',
        success: '#4CAF50',
        warning: '#FFC107'
      },
    },
  },
  icons: {
    iconfont: 'mdi',
  },
});
