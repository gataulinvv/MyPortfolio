/* eslint-disable no-tabs */
/* eslint-disable no-mixed-spaces-and-tabs */
import { makeObservable, observable, action } from 'mobx';
import axios from 'axios';

class UserPositionFormStore {
	data = {};

	loading = true;

	constructor() {
	  makeObservable(this, {

	    data: observable,
	    RefreshStore: action,
	    loading: observable,
	  });
	}

	ClearStore() {
	  this.data = {};
	}

	async Update(model) {
	  this.loading = true;

	  const res = await axios.put('/api/Clients/', model)
	    .then(
	      (response) => {
	        if (response.status == 200) {
	          this.RefreshStore(model.id);
	          return true;
	        }

	        this.loading = false;
	        return false;
	      },
	    ).catch((error) => {
	      this.loading = false;
	      return false;
	    });

	  return res;
	}

	async Add(model) {
	  this.loading = true;

	  const res = await axios.post('/api/Clients/', model)
	    .then(
	      (response) => {
	        if (response.status == 200) {
	          this.RefreshStore(response.data.id);
	          return true;
	        }

	        this.loading = false;
	        return false;
	      },
	    ).catch((error) => {
	      this.loading = false;
	      return false;
	    });

	  return res;
	}

	RefreshStore(id) {
	  axios.get(`/api/Clients/${id}`).then((response) => {
	    if (response.status == 200) {
	      this.data = response.data;
	    }
	    this.loading = false;
	  }).catch((error) => {
	    this.loading = false;
	    console.log(error);
	  });
	}
}

export default UserPositionFormStore;
