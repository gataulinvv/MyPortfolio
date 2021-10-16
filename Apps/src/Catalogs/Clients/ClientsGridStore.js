import { makeObservable, observable, action } from 'mobx'
import axios from 'axios';
import { message } from 'antd';


class ClientsGridStore {

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
			SetFilteredData: action

		})
	}

	Delete(id) {
		this.loading = true;
		axios.delete('/api/Clients/' + id
		).then((response) => {

			if (response.status == 200) {

				message.success('Елемент успешно удален');

				this.RefreshStore();
			}
			else
				message.error('Елемент удалить не удалось!');

			this.loading = false;

		}).catch((error) => {

			this.loading = false;

			message.error(error.toString());
		});
	}


	ClearStore() {
		this.data = [];
	}




	RefreshStore() {

		this.loading = true;

		axios.get('/api/Clients'
		).then((response) => {

			if (response.status == 200) {

				if (!response.data.isOk)
					window.location.href = window.history.back();

				for (var i = 0; i < response.data.data.length; i++) {

					var row = response.data.data[i];

					var columns = [];

					for (var column in row) {

						columns[column] = { match: true };
					}

					this.filterMatrix[row.id] = { columns: columns, display: true };
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

export default ClientsGridStore;