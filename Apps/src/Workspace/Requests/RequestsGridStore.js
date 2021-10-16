import { message } from 'antd';
import { makeObservable, observable, action } from 'mobx';
import axios from 'axios';
import moment from 'moment';

class RequestsGridStore {
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
    axios.delete(`/api/Requests/${id}`).then((response) => {
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

  parseDate(data) {
    const checkDate = moment().diff('0001-01-01', 'minutes');

    const bDate = moment().diff(data.date, 'minutes');

    if (bDate == checkDate) {
      data.date = '';
    } else {
      data.date = moment(data.date).format('DD-MM-YYYY');
    }
  }

  parseRoles(data) {
    data.roleNamesList = data.roleNamesList.join(' ');
  }

  RefreshStore() {
    const spec = { Page: 1, Take: 25 };

    const x = 1;

    this.loading = true;

    axios.get('/api/Requests').then((response) => {
      if (response.status == 200) {
        if (!response.data.isOk) { window.location.href = window.history.back(); }

        for (let i = 0; i < response.data.data.length; i += 1) {
          const row = response.data.data[i];

          const columns = [];

          for (let j = 0; j < Object.keys(row).length; j += 1) {
            const column = Object.keys(row)[j];
            columns[column] = { match: true };
          }

          this.filterMatrix[row.id] = { columns, display: true };

          this.parseDate(row);
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

export default RequestsGridStore;
