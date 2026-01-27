import { router } from '@/app/router';
import { classNamePlugin } from '@/shared/plugins';
import { createApp } from 'vue';
import App from './app/App.vue';
import { i18n } from '@/shared/i18n';

createApp(App).use(i18n).use(router).use(classNamePlugin).mount('#app');
