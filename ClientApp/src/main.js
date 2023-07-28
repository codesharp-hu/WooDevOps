import { createApp } from 'vue';
import App from './App.vue';

const app = createApp(App);
app.config.globalProperties.$filters = {
  dateTimeFormat(value) {
    let options = {
      year: 'numeric', month: 'numeric', day: 'numeric',
      hour: 'numeric', minute: 'numeric'
    };

    const date = new Date(value);
    return new Intl.DateTimeFormat('default', options).format(date);
  },
  dateFormat(value) {
    let options = {
      year: 'numeric', month: 'numeric', day: 'numeric'
    };

    const date = new Date(value);
    return new Intl.DateTimeFormat('default', options).format(date);
  },
  timeFormat(value) {
    let options = {
      hour: 'numeric', minute: 'numeric'
    };

    const date = new Date(value);
    return new Intl.DateTimeFormat('default', options).format(date);
  },
};

app.mount('#app');