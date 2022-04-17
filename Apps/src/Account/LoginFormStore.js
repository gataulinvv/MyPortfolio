/* eslint-disable no-mixed-spaces-and-tabs */
/* eslint-disable no-tabs */
import { makeObservable, observable, action } from 'mobx';
import axios from 'axios';
import qs from 'qs';
import { message } from 'antd';

class LoginFormStore {
	data = { userName: '', password: '', returnUrl: '/' };

	constructor() {
	  makeObservable(this, {

	    data: observable,
	    DoLogin: action,
	  });
	}

	DoLogin(model) {
	  const token = document.querySelector('__RequestVerificationToken, input').getAttribute('value');

	  axios.post('/Account/Login', qs.stringify({ __RequestVerificationToken: token, model }))
	    .then((response) => {
	      if (response.status == 200) {
	        if (response.data.isOk) { window.location.href = response.data.url; } else {
	          message.error(response.data.errMessage);
	          this.data = { userName: '', password: '', returnUrl: '/' };
	        }
	      }
	    }).catch((error) => {
	      console.log(error);
	    });
	}
}

export default LoginFormStore;
