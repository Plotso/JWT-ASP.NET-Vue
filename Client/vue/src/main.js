import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

import moment from 'moment';

const app = createApp(App)
app.config.globalProperties.$filters = {
    formatDate(value) {
        return moment(String(value)).format('MM/DD/YYYY hh:mm')
    }
  }

app.use(store).use(router).mount('#app')
