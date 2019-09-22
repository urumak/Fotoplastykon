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
            console.log('asdasd');
        }
        if (ex.code === 403)
        {
            if (error.response.config.method === "get")
                Vue.router.replace({ name: 'error-403' });
            else
                Vue.alert.danger('');
        }
        if (ex.code === 404)
        {
            console.log('eluwa');
            //Vue.alert.danger('dfgdfg');
            console.log(Vue);
            console.log(Vue.router);
            Vue.router.replace({ name: 'error-404' });
        }
        if (ex.code === 500)
        {
            if (!error.data)
            {
                Vue.alert.danger('jjnjnj');
            }
            else
            {
                Vue.alert.danger('jhnkjnj');
            }

            if (ex.data && ex.data.stackTrace)
            {
                console.log(ex.data.message);
                console.log(ex.data.stackTrace);
            }
        }

        return Promise.reject(ex);
    }
);