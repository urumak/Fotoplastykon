import Vue from 'vue'
import VueAxios from 'vue-axios';
import axios from 'axios';

Vue.use(VueAxios, axios);
Vue.axios.defaults.baseURL = process.env.VUE_APP_API_URL;
Vue.axios.interceptors.response.use(
    response => { return response; },
    error =>
    {
        let ex = {
            code: 500,
            message: 'Unexpected exception occured.',
            data: null as any,
            inner: null as any
        };

        if (error.response)
        {
            ex.code = error.response.status;
            ex.data = (typeof error.response.data === 'string') ? { message: error.response.data } : error.response.data;
            ex.message = ex.data.message ? ex.data.message : error.message;
            ex.inner = error;
        }

        if (ex.code === 401)
        {
            console.log('401 catched');
        }
        if (ex.code === 403)
        {
            console.log('403 catched');
        }
        if (ex.code === 404)
        {
            console.log('404 catched');
        }
        if (ex.code === 500)
        {
            console.log('500 catched');
        }

        return Promise.reject(ex);
    }
);
