/* eslint-disable no-tabs */
/* eslint-disable no-mixed-spaces-and-tabs */
import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react';
import {
  Table, Empty, Spin, Button, Space, Popconfirm,
} from 'antd';
import styled from 'styled-components';
import themes from '/src/Shared/themes';
import { RedoOutlined } from '@ant-design/icons';
import BasePopper from '/src/Shared/BasePopper/BasePopper';
import { Delete } from '@styled-icons/fluentui-system-regular';
import ClientsGridStore from './ClientsGridStore';
import ClientForm from '/src/Catalogs/Clients/ClientForm';
import StringFilter from '/src/Shared/Filters/StringFilter';
import StringSorter from '/src/Shared/Sorters/StringSorter';

const StyledTitle = styled.div`
padding-top:0.5em;
`;

const DeleteIcon = styled(Delete)`
height: 1.5em;
`;

const ClientsGrid = observer((({ title }) => {
  const columns = [

    {
      // width: '20%',
      title: () => (
        <StringFilter
          columnName="name"
          columnTitle="Имя"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'name',
      key: 'name',
      sorter: {
        compare: (a, b) => StringSorter(a.name, b.name),
      },

    },

    {
      // width: '20%',
      title: () => (
        <StringFilter
          columnName="email"
          columnTitle="E-mail"
          dataSource={store.data}
          parentViewStore={store}
        />
      ),
      dataIndex: 'email',
      key: 'email',
      sorter: {
        compare: (a, b) => StringSorter(a.email, b.email),
      },

    },
    {
      // width: '10%',
      dataIndex: 'id',
      key: 'id',
      render: (text, record, index) => (
        <BasePopper
          titleType="edit"
          header="Карточка клиента"
          showcase={record.name}
          Content={<ClientForm id={record.id} parentViewStore={store} />}
        />
      ),
    },

    {
      // width: '10%',
      dataIndex: 'id',
      key: 'id',
      render: (text, record, index) => (

        <Popconfirm
          title="Удалить?"
          onConfirm={() => deleteItem(record.id)}
          okText="Yes"
          cancelText="No"
        >
          <Button type="link" icon={<DeleteIcon />} />
        </Popconfirm>
      ),
    },
  ];

  const deleteItem = (id) => {
    store.Delete(id);
  };

  const theme = themes.male;

  const [store] = useState(new ClientsGridStore());

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
                header="Карточка клиента"
                showcase="Новый клиент"
                Content={<ClientForm id={0} parentViewStore={store} />}
              />
            </td>
          </tr>
        </tbody>
      </table>

      <Table
        pageSize={20}
        rowClassName={(record, index) => (index % 2 === 0 ? 'table-row-light' : 'table-row-dark')}
        locale={{
				  emptyText: <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Нет данных" />,
        }}
        debug
        rowKey="id"
        size="small"
        dataSource={store.filteredData}
        columns={columns}
      />
    </Spin>
  );
}));

export default ClientsGrid;
