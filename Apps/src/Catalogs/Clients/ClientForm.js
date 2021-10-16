import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react'
import { Button, Input, Form, Spin, message, Skeleton } from 'antd';

import 'antd/dist/antd.css';
import themes from '/src/Shared/themes';
import { SaveOutlined } from '@ant-design/icons';

import ClientFormStore from './ClientFormStore';



const ClientForm = (props) => {

	const { id, parentViewStore } = props;

	const [store] = useState(new ClientFormStore(parentViewStore));


	useEffect(() => {

		//if (id != 0)
		store.RefreshStore(id);

		return function cleanup() {
			store.ClearStore();
		};
	}, [])

	var theme = themes["male"];

	const checkEmail = (rule, value) => {

		var regExp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

		if (regExp.test(value)) {
			return Promise.resolve()
		}
		else {
			return Promise.reject(errMessage);
		}
	}

	const changeName = (event) => {
		store.data.name = event.target.value;
	}
	const changeEmail = (event) => {
		store.data.email = event.target.value;
	}

	const submit = (event) => {

		form.setFieldsValue(store.data);

		form.validateFields()
			.then((values) => {

				var item = form.getFieldsValue(true);

				if (!item.id) {

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

	const [form] = Form.useForm();

	const errMessage = 'Заполните поле!'




	return (

		<div onMouseDown={(e) => e.stopPropagation()}>
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

									<Form.Item name="name" label="Имя" style={{ marginBottom: "0px" }} rules={[{ required: true, min: 1, type: "string", message: errMessage }]}>
										<div><Input value={store.data.name} onChange={changeName} /></div>
									</Form.Item>

									<Form.Item name="email" label="Email" style={{ marginBottom: "0px" }}
										rules={[{ validator: checkEmail }]}
										validateTrigger="onChange"
									>
										<div><Input value={store.data.email} onChange={changeEmail} /></div>
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


export default observer(ClientForm);

