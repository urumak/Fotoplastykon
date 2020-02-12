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
        primary: '#9277ce',
        secondary: '#a1a1a1',
        accent: '#9277ce',
        error: '#FF5252',
        info: '#1461f3',
        success: '#4CAF50',
        warning: '#FFC107'
      },
    },
  },
  icons: {
    iconfont: 'mdi',
  },
});
