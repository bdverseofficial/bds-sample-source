import Vue from 'vue';
import BdsVuetify from './plugins/vuetify';
import App from './App.vue';
import { WebApp, WebAppOptions } from './webApp';
import "./scss/main.scss";

function view(name: string) {
  return () => import('./views/' + name + '.vue');
}

let options: WebAppOptions = {
  title: "BDVerse Sport",
  api: {
    throwOnlyCustomError: true,
    applyTranslation: true
  },
  router: {
    routes: [
      { name: 'Home', path: '/', meta: { auth: false }, component: view('Home') },      
      { name: 'About', path: '/about', meta: { auth: false }, component: view('About') },      
      { name: 'Login', path: '/login', meta: { auth: false }, component: view('Login/Login') },      
      {
        path: '/profile', meta: { auth: true }, component: view('Profile/Profile'),
        children: [
          { name: 'Profile_Default', path: 'account', meta: { auth: true }, component: view('Profile/Account') },
          { name: 'Profile_Security', path: 'security', meta: { auth: true }, component: view('Profile/Security') },
        ]
      },      
      { name: 'resetpassword', path: '/resetpassword', meta: { auth: false }, component: view('Login/ResetPassword') },      
      { name: 'register', path: '/register', meta: { auth: false }, component: view('Login/Register') },      
      { name: 'activation', path: '/activation', meta: { auth: false }, component: view('Login/Activation') },
    ]
  },
  auth: {
    refreshOnInit: true,
  },
};

const web = new WebApp(options);

web.init().then(() => {
  const bdsVuetify = new BdsVuetify(web.translationService.i18n);  
  const vue = new Vue({
      vuetify: bdsVuetify.vuetify,
      i18n: web.translationService.i18n,
      router: web.routerService.router,
      render: (h) => h(App),
    }).$mount('#app');    
});