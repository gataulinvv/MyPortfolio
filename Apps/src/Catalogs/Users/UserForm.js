import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react'
import { Button, Input, Form, message, Checkbox, Spin, Skeleton } from 'antd';

import 'antd/dist/antd.css';
import themes from '/src/Shared/themes';
import { SaveOutlined } from '@ant-design/icons';

import UserFormStore from './UserFormStore';



const UserForm = (props) => {

	const { id, parentViewStore } = props;

	const [store] = useState(new UserFormStore(parentViewStore));


	useEffect(() => {

		store.RefreshStore(id);

		return function cleanup() {
			store.ClearStore();
		};
	}, [])

	var theme = themes["male"];


	const [form] = Form.useForm();

	const errMessage = 'Заполните поле!'


	const submit = (event) => {

		form.setFieldsValue(store.data);

		form.validateFields()
			.then((values) => {

				var item = form.getFieldsValue(true);

				if (item.id == 0) {

					async function add(item) {
						var result = await store.Add(item);
						if (result == true) {
							message.success('Елемент успешно создан');
							parentViewStore.RefreshStore();
						}
						else
							message.error('Создать изменить не удалось!');
					}
					add(item);
				}
				else {

					async function update(item) {

						var result = await store.Update(item);
						if (result == true) {
							message.success('Елемент успешно изменен');
							parentViewStore.RefreshStore();
						}
						else
							message.error('Елемент изменить не удалось!');
					};
					update(item);
				}

			})
			.catch((errorInfo) => {
				console.log("error");
			});
	}


	const checkEmail = (rule, value) => {

		var regExp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

		if (regExp.test(value)) {
			return Promise.resolve()
		}
		else {
			return Promise.reject(errMessage);
		}
	}



	const changeEmail = (event) => {
		store.data.email = event.target.value;
	}


	const changeUserName = (event) => {

		store.data.userName = event.target.value;
	}

	const userRolesChange = (value) => {

		store.data.userroles = value;
	}

	const changePassword = (event) => {
		store.data.password = event.target.value;
	}
	return (

		<div onMouseDown={(e) => e.stopPropagation()} >
			<Skeleton loading={store.loading}>
				<Form form={form} layout="horizontal" labelCol={{ span: 10 }}>

					<table >
						<tbody>
							<tr>
								<td align="right">
									<Button icon={<SaveOutlined />} onClick={(event) => submit(event)}
										style={{ background: theme.background, color: theme.color }}>Сохранить</Button>
								</td>
							</tr>
							<tr>
								<td>

									<Form.Item name="userName" label="Имя" style={{ marginBottom: "0px" }} rules={[{ required: true, min: 1, type: "string", message: errMessage }]}>
										<div><Input value={store.data.userName} onChange={changeUserName} /></div>
									</Form.Item>

									<Form.Item name="email" label="Email" style={{ marginBottom: "0px" }}
										rules={[{ validator: checkEmail }]}
										validateTrigger="onChange"
									>
										<div><Input value={store.data.email} onChange={changeEmail} /></div>
									</Form.Item>

									<Form.Item name="password" label="Пароль" style={{ marginBottom: "0px" }}

										rules={[{ required: store.data.id == 0 ? true : false, min: 1, type: "string", message: errMessage }]}
									>
										<div><Input.Password value={store.data.password} onChange={changePassword} /></div>
									</Form.Item>


									<Form.Item name="allroles" label="Роли" style={{ marginBottom: "0px" }}

									>
										<div>
											<Checkbox.Group
												options={store.data.allroles}
												onChange={userRolesChange}
												value={store.data.userroles}
											/>
										</div>

									</Form.Item>


								</td>
							</tr>
						</tbody>
					</table>

				</Form>
			</Skeleton>
		</div>
	)
}


export default observer(UserForm);

