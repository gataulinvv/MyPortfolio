import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react';
import {
  Table, Empty, Spin, Button, Popconfirm,
} from 'antd';
import styled from 'styled-components';
import themes from '/src/Shared/themes';
import { RedoOutlined } from '@ant-design/icons';
import BasePopper from '/src/Shared/BasePopper/BasePopper';
import { Delete } from '@styled-icons/fluentui-system-regular';
import RequestForm from './RequestForm';
import RequestsGridStore from './RequestsGridStore';
import StringFilter from '/src/Shared/Filters/StringFilter';
import DateSorter from '/src/Shared/Sorters/DateSorter';
import StringSorter from '/src/Shared/Sorters/StringSorter';

const StyledTitle = styled.div`
padding-top:0.5em;
`;

const DeleteIcon = styled(Delete)`
height: 1.5em;
`;

const RequestsGrid = ({ title }) => {
  const columns = [
    {
      width: '13.3%',
      title: () => (
        <StringFilter
          columnName="id"
          columnTitle="Номер заявки"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      // title:'Номер заявки',
      dataIndex: 'id',
      key: 'id',

      sorter: {
        compare: (a, b) => (a.id - b.id),
      },

    },

    {
      width: '13.3%',
      title: () => (
        <StringFilter
          columnName="date"
          columnTitle="Дата заявки"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'date',
      key: 'date',
      sorter: {
        compare: (a, b) => DateSorter(a.date, b.date),
      },

    },
    {
      width: '13.3%',
      title: () => (
        <StringFilter
          columnName="clientName"
          columnTitle="Организация"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'clientName',
      key: 'clientName',
      sorter: {
        compare: (a, b) => StringSorter(a.clientName, b.clientName),
      },
    },
    {
      width: '13.3%',
      title: () => (
        <StringFilter
          columnName="userName"
          columnTitle="Пользователь"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'userName',
      key: 'userName',
      sorter: {
        compare: (a, b) => StringSorter(a.userName, b.userName),
      },

    },
    {
      width: '13.3%',
      title: () => (
        <StringFilter
          columnName="userEmail"
          columnTitle="Email"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'userEmail',
      key: 'userEmail',
      sorter: {
        compare: (a, b) => StringSorter(a.email, b.email),
      },
    },

    {
      width: '13.3%',
      title: () => (
        <StringFilter
          columnName="roleNamesList"
          columnTitle="Роли (должности)"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'roleNamesList',
      key: 'roleNamesList',
      sorter: {
        compare: (a, b) => StringSorter(a.roleNamesList, b.roleNamesList),
      },

    },

    {
      width: '10%',
      dataIndex: 'id',
      key: 'id',
      render: (text, record, index) => (
        <BasePopper
          titleType="edit"
          header="Карточка заявки"
          showcase={text}
          Content={<RequestForm id={record.id} parentViewStore={store} />}
        />
      ),
    },
    {
      width: '10%',
      dataIndex: 'id',
      key: 'id',
      render: (text, record, index) => (

        <Popconfirm
          title="Удалить заявку?"
          onConfirm={() => deleteRequest(record.id)}
          okText="Yes"
          cancelText="No"
        >
          <Button type="link" icon={<DeleteIcon />} />
        </Popconfirm>
      ),
    },
  ];

  const deleteRequest = (id) => {
    store.Delete(id);
  };

  const theme = themes.male;

  const [store] = useState(new RequestsGridStore());

  useEffect(() => {
    document.title = title;
    store.RefreshStore();
    return function cleanup() {
      document.title = 'MVCApp';
      store.ClearStore();
    };
  }, []);

  return (
    <Spin spinning={store.loading}>
      <table width="100%">
        <tbody>
          <tr align="center"><td colSpan="2"><StyledTitle><h2>{title}</h2></StyledTitle></td></tr>
          <tr>
            <td>
              <Button
                icon={<RedoOutlined />}
                onClick={() => store.RefreshStore()}
                style={{ background: theme.background, color: theme.color }}
              >
                Обновить
              </Button>
            </td>

            <td align="right">
              <BasePopper
                titleType="add"
                header="Карточка заявки"
                showcase="Новая заявка"
                Content={<RequestForm id={0} parentViewStore={store} />}
              />
            </td>
          </tr>
        </tbody>
      </table>

      <Table
        pageSize={20}
        rowClassName={(record, index) => (index % 2 === 0 ? 'table-row-light' : 'table-row-dark')}
        locale={{ emptyText: <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Нет данных" /> }}
        debug
        rowKey="id"
        size="small"
        dataSource={store.filteredData}
        columns={columns}
      />
    </Spin>
  );
};

export default observer(RequestsGrid);
