import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react'
import { Table, Empty, Spin, Button, Space, Popconfirm } from 'antd';
import moment from 'moment'

import styled from 'styled-components';
import themes from '/src/Shared/themes';
import { RedoOutlined } from '@ant-design/icons';
import BasePopper from '/src/Shared/BasePopper/BasePopper';

import UsersGridStore from './UsersGridStore';

import UserForm from '/src/Catalogs/Users/UserForm'

import StringFilter from '/src/Shared/Filters/StringFilter';

import StringSorter from '/src/Shared/Sorters/StringSorter';

import { Delete } from '@styled-icons/fluentui-system-regular';

const StyledTitle = styled.div`
padding-top:0.5em;
`;

const DeleteIcon = styled(Delete)`
height: 1.5em;
`;



const UsersGrid = observer((({ title }) => {

	const columns = [



		{
			//width: '20%',
			title: () => <StringFilter columnName='userName' columnTitle='Имя' dataSource={store.data} parentViewStore={store} />,
			dataIndex: 'userName',
			key: 'userName',
			sorter: {
				compare: (a, b) => StringSorter(a.name, b.name)
			},

		},
		{
			//width: '20%',
			title: () => <StringFilter columnName='roleNamesList' columnTitle='Роли' dataSource={store.data} parentViewStore={store} />,
			dataIndex: 'roleNamesList',
			key: 'roleNamesList',
			sorter: {
				compare: (a, b) => StringSorter(a.roleNamesList, b.roleNamesList)
			},

		},

		{
			//width: '20%',
			title: () => <StringFilter columnName='email' columnTitle='E-mail' dataSource={store.data} parentViewStore={store} />,
			dataIndex: 'email',
			key: 'email',
			sorter: {
				compare: (a, b) => StringSorter(a.email, b.email)
			},

		},
		{
			//width: '10%',
			dataIndex: 'id',
			key: 'id',
			render: (text, record, index) => {
				return (
					<BasePopper titleType={"edit"} header="Карточка пользователя" showcase={record.userName} Content={<UserForm id={record.id} parentViewStore={store} />} />
				)
			}
		},

		{
			//width: '10%',
			dataIndex: 'id',
			key: 'id',
			render: (text, record, index) => {
				return (

					<Popconfirm
						title="Удалить?"
						onConfirm={() => deleteItem(record.id)}
						okText="Yes"
						cancelText="No"
					>
						<Button type="link" icon={<DeleteIcon />} ></Button>
					</Popconfirm>
				)
			}
		}
	];

	const deleteItem = (id) => {
		store.Delete(id);
	}



	var theme = themes["male"];

	const [store] = useState(new UsersGridStore());

	useEffect(() => {
		document.title = title;
		store.RefreshStore();
		return function cleanup() {
			document.title = "MVCApp";
			store.ClearStore();
		};

	}, []);

	return (
		<Spin spinning={store.loading} >


			<table width="100%">
				<tbody>
					<tr align='center'><td colSpan="2" ><StyledTitle><h2>{title}</h2></StyledTitle></td></tr>
					<tr><td><Button icon={<RedoOutlined />} onClick={() => store.RefreshStore()}
						style={{ background: theme.background, color: theme.color }}>Обновить</Button></td>
						<td align='right'>
							<BasePopper titleType={"add"} header="Карточка пользователя" showcase={"Новый пользователь"} Content={<UserForm id={0} parentViewStore={store} />} />
						</td>
					</tr>
				</tbody>
			</table>


			<Table
				pageSize={20}
				rowClassName={(record, index) => index % 2 === 0 ? 'table-row-light' : 'table-row-dark'}

				locale={{
					emptyText: <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Нет данных" />
				}}

				debug rowKey="id"
				size="small"
				dataSource={store.filteredData}
				columns={columns}
			/>
		</Spin>
	)

}))

export default UsersGrid;