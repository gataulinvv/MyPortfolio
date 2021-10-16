import { makeObservable, observable, action } from 'mobx'
import axios from 'axios';
import moment from 'moment'

class RequestFormStore {

    data = {};
    permanentData = {};
    loading = true;
    

    constructor() {

        makeObservable(this, {

            data: observable,
            RefreshStore: action,
            loading: observable
        })

    }

	

    async Update(model) {

        this.loading = true;

        const res = await axios.put('/api/Requests/', model)
            .then(
                (response) => {

                    if (response.status == 200) {
                        this.RefreshStore(model.id);
                        return true;

                    }
                    else {                     
                        this.loading = false;
                        return false;
                    }
                }
            ).catch((error) => {                
                this.loading = false;
                return false;
            });

        return res;
    }

    async Add(model) {

        this.loading = true;

        var res = await axios.post('/api/Requests/', model)
            .then(
                (response) => {

                    if (response.status == 200) {

                        this.RefreshStore(response.data.id);
                        return true;
                    }
                    else {

                        this.loading = false;
                        return false;
                    }
                }
            ).catch((error) => {
                this.loading = false;
                return false;
            });

        return res;
    }



    parseDate(data) {

        if (data.id == 0)
            data.date = undefined;
        else
            data.date = moment(data.date);

    }

    RefreshStore(id) {

        axios.get('/api/Requests/' + id
        ).then((response) => {

            if (response.status == 200) {

                this.parseDate(response.data);

                response.data.client_id = response.data.client_id == 0 ? null : response.data.client_id;

                this.data = response.data;
                this.permanentData = response.data;
            }
            this.loading = false;

        }).catch((error) => {
            this.loading = false;
            console.log(error);
        });
    }

    ClearStore() {

        this.data = {};
        this.permanentData = {};

    }
}


export default RequestFormStore;