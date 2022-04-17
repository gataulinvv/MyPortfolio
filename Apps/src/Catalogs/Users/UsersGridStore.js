/* eslint-disable no-restricted-syntax */
/* eslint-disable no-plusplus */
/* eslint-disable guard-for-in */
/* eslint-disable no-mixed-spaces-and-tabs */
/* eslint-disable no-tabs */
import { makeObservable, observable, action } from 'mobx';
import axios from 'axios';
import { message } from 'antd';

class UsersGridStore {
	filterMatrix = [];

	data = [];

	filteredData = [];

	loading = true;

	constructor() {
	  makeObservable(this, {

	    loading: observable,
	    data: observable,
	    Delete: action,
	    RefreshStore: action,
	    filteredData: observable,
	    SetFilteredData: action,

	  });
	}

	Delete(id) {
	  this.loading = true;
	  axios.delete(`/api/Users/${id}`).then((response) => {
	    if (response.status == 200) {
	      message.success('Елемент успешно удален');

	      this.RefreshStore();
	    } else { message.error('Елемент удалить не удалось!'); }

	    this.loading = false;
	  }).catch((error) => {
	    this.loading = false;

	    message.error(error.toString());
	  });
	}

	ClearStore() {
	  this.data = [];
	}

	parseRoles(data) {
	  data.roleNamesList = data.roleNamesList.join(' ');
	}

	RefreshStore() {
	  this.loading = true;

	  axios.get('/api/Users').then((response) => {
	    if (response.status == 200) {
	      if (!response.data.isOk) { window.location.href = window.history.back(); }

	      for (let i = 0; i < response.data.data.length; i++) {
	        const row = response.data.data[i];

	        const columns = [];

	        for (const column in row) {
	          columns[column] = { match: true };
	        }

	        this.filterMatrix[row.id] = { columns, display: true };

	        this.parseRoles(row);
	      }

	      this.data = response.data.data;
	      this.filteredData = response.data.data;
	    }

	    this.loading = false;
	  }).catch((error) => {
	    this.loading = false;

	    console.log(error);
	  });
	}

	SetFilteredData(filteredData) {
	  this.filteredData = filteredData;
	}
}

export default UsersGridStore;
