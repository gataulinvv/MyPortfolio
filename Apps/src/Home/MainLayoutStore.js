import { makeObservable, action, observable } from 'mobx'
import axios from 'axios';

class MainLayoutStore {

    collapsed = false;
    
    rolesLists = [];    

    constructor() {

        makeObservable(this, {
            
            SetCollapse: action,
            collapsed: observable,

        })    
    }

    ClearStore() {
        this.rolesLists = [];
    }

	RefreshStore(id) {

		axios.get('/api/Roles' + id
		).then((response) => {

			if (response.status == 200) {

				//if (!response.data.isOk)
				//	window.location.href = response.data.url;

				this.rolesList = response.data.data;
				
				this.loading = false;
			}

		}).catch((error) => {
			console.log(error);
		});

	}

   
    SetCollapse() {
        this.collapsed = !this.collapsed;
    }
}

const LayoutStoreInstace = new MainLayoutStore();

export default LayoutStoreInstace;