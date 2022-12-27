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

  app.config.globalProperties.$filters = {
    str_limit(value, size) {
      if (!value) return '';
      value = value.toString();
  
      if (value.length <= size) {
        return value;
      }
      return value.substr(0, size) + '...';
    }
  }
app.use(store).use(router).mount('#app')
