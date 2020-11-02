/*
  The main will defined all the options for the bds-sdk-vue, start the bds main services, and launch the vue app
*/
import Vue from 'vue';
import BdsVuetify from './plugins/vuetify';
import App from './App.vue';
import { WebApp, WebAppOptions } from './webApp';
import "./scss/main.scss";

/*
Helper for lazy loading a view
*/
function view(name: string) {
  return () => import('./views/' + name + '.vue');
}

const bdsVuetify = new BdsVuetify();

/*
Define the main options for the WebApp
*/
const options: WebAppOptions = {
  // Title of the Web App
  title: "BDVerse Sample",

  // Api options
  api: {
    // We do not throw errors for unexpected exception
    throwOnlyCustomError: true,
    // We ask BDS to managed the translation on every entity containing some LocalizedString
    applyTranslation: true
  },
  // Router Options
  router: {
    routes: [
      { name: 'Root', path: '/', meta: { auth: false }, redirect: { name: "Home" } },
      { name: 'Home', path: '/home', meta: { auth: false }, component: view('Home') },
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
      { name: 'Search', path: '/search', meta: { auth: false }, component: view('Search/Search'), props: (route) => ({ query: route.query.q, sort: route.query.s }) },
    ]
  },
  // Authentication options
  auth: {
    // At the launch of the app, we will use the local or session storage to identify the current user
    refreshOnInit: true,
  },
  vue: {
    app: App,
    options: {
      vuetify: bdsVuetify.vuetify,
    }
  }
};

// Create the Web App
const web = new WebApp(options);