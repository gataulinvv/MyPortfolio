/* eslint-disable no-mixed-spaces-and-tabs */
/* eslint-disable no-tabs */
import { makeObservable, observable, action } from 'mobx';
import axios from 'axios';

class UserFormStore {
	data = {};

	loading = true;

	parentStore = {};

	constructor(parentViewStore) {
	  this.parentStore = parentViewStore;

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

	  const res = await axios.put('/api/Users/', model)
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

	  const res = await axios.post('/api/Users/', model)
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
	  axios.get(`/api/Users/${id}`).then((response) => {
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

export default UserFormStore;
