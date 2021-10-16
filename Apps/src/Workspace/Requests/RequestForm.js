import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import {
  DatePicker, Button, Select, Form, Spin, message, Skeleton,
} from 'antd';

import 'antd/dist/antd.css';
import moment from 'moment';
import themes from '/src/Shared/themes';
import { SaveOutlined } from '@ant-design/icons';

import RequestFormStore from './RequestFormStore';

const RequestForm = (props) => {
  const { id, parentViewStore } = props;

  const [store] = useState(new RequestFormStore());

  useEffect(() => {
    store.RefreshStore(id);

    return function cleanup() {
      store.ClearStore();
    };
  }, []);

  const theme = themes.male;

  const changeDate = (date) => {
    if (date == null) { store.data.date = undefined; } else { store.data.date = moment(date); }
  };

  const checkIntValue = (rule, value) => {
    if (value == 0) value = null;

    const regExp = /[0-9]/;

    if (regExp.test(value)) {
      return Promise.resolve();
    }

    return Promise.reject(errMessage);
  };

  const chengeUserId = (value) => {
    store.data.user_id = value;
  };

  const changeClientId = (value) => {
    store.data.client_id = value;
  };
  const submit = (event) => {
    form.setFieldsValue(store.data);

    form.validateFields()
      .then((values) => {
        const item = form.getFieldsValue(true);

        if (!item.id) {
          async function add(item) {
            const result = await store.Add(item);
            if (result == true) {
              message.success('Елемент успешно создан');
              parentViewStore.RefreshStore();
            } else { message.error('Создать изменить не удалось!'); }
          }
          add(item);
        } else {
          async function update(item) {
            const result = await store.Update(item);
            if (result == true) {
              message.success('Елемент успешно изменен');
              parentViewStore.RefreshStore();
            } else { message.error('Елемент изменить не удалось!'); }
          }
          update(item);
        }
      })
      .catch((errorInfo) => {
        console.log('error');
      });
  };

  const [form] = Form.useForm();

  const errMessage = 'Заполните поле!';

  const checkUserId = (rule, value) => {
    if (value == null) return Promise.reject(errMessage);
    return Promise.resolve();
  };

  return (

    <div onMouseDown={(e) => e.stopPropagation()}>

      <Skeleton loading={store.loading}>
        <Form form={form} layout="horizontal" labelCol={{ span: 10 }}>

          <table>
            <tbody>
              <tr>
                <td align="right">
                  <Button
                    icon={<SaveOutlined />}
                    onClick={(event) => submit(event)}
                    style={{ background: theme.background, color: theme.color }}
                  >
                    Сохранить

                  </Button>
                </td>
              </tr>
              <tr>
                <td>

                  <Form.Item
                    name="date"
                    label="Дата заявки"
                    style={{ marginBottom: '0px' }}
                    rules={[{ required: true, message: errMessage }]}
                  >

                    <div>
                      <DatePicker
                        style={{ width: '100%' }}
                        format="DD-MM-YYYY"

                        value={store.data.date}

                        onChange={(date) => changeDate(date)}
                      />
                    </div>
                  </Form.Item>

                  <Form.Item
                    name="user_id"
                    label="Пользователь"
                    style={{ marginBottom: '0px' }}

                    rules={[{ validator: (rule, value) => checkUserId(rule, store.data.user_id) }]}
                  >
                    <div>
                      <Select
                        showSearch
                        onChange={chengeUserId}
                        value={store.data.user_id}
                        filterOption={(input, option) => option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                      >
                        {
                                                    store.data.users_list

                                                    && store.data.users_list.map((item) => (
                                                      <Select.Option
                                                        key={item.id}
                                                        value={item.id}
                                                      >
                                                        {item.userName}
                                                      </Select.Option>
                                                    ))
                                                }

                      </Select>
                    </div>
                  </Form.Item>

                  <Form.Item
                    name="client_id"
                    label="Клиент"
                    style={{ marginBottom: '0px' }}

                    rules={[{ validator: (rule, value) => checkIntValue(rule, store.data.client_id) }]}
                  >
                    <div>
                      <Select
                        showSearch
                        onChange={changeClientId}
                        value={store.data.client_id}
                        filterOption={(input, option) => option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                      >
                        {
                                                    store.data.clients_list

                                                    && store.data.clients_list.map((item) => (
                                                      <Select.Option
                                                        key={item.id}
                                                        value={item.id}
                                                      >
                                                        {item.name}
                                                      </Select.Option>
                                                    ))
                                                }

                      </Select>
                    </div>
                  </Form.Item>

                </td>
              </tr>
            </tbody>
          </table>

        </Form>

      </Skeleton>
    </div>
  );
};

export default observer(RequestForm);

// <Spin spinning={store.loading} >
// <Spin spinning={store.loading}>
//	</Spin>
