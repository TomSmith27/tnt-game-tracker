import Vue from 'vue';
import './plugins/vuetify';
import App from './App.vue';
import router from './router';
import store from './store';
import StarRating from 'vue-star-rating';
import moment from 'moment';
import _ from 'lodash';
import VueApexCharts from 'vue-apexcharts';
Vue.config.productionTip = false;
//Vue.use(VueApexCharts);

import 'vue-swatches/dist/vue-swatches.min.css';

Vue.component('star-rating', StarRating);

Vue.filter('date', (value: string) => {
	return moment(value).format('Do MMM YYYY');
});

Vue.filter('round', (value: string) => {
	return _.round(Number.parseFloat(value), 2);
});

Vue.component('apexchart', VueApexCharts);
new Vue({
	router,
	store,
	render: (h) => h(App)
}).$mount('#app');
