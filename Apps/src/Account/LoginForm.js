import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Input, Form } from 'antd';
import 'antd/dist/antd.css';
import themes from '/src/Shared/themes';
import { SaveOutlined } from '@ant-design/icons';
import LoginFormStore from './LoginFormStore';

const LoginForm = () => {
  const [store] = useState(new LoginFormStore());

  const theme = themes.male;

  const changeUserName = (event) => {
    store.data.userName = event.target.value;
  };
  const changePassword = (event) => {
    store.data.password = event.target.value;
  };

  const submit = (event) => {
    form.setFieldsValue(store.data);

    form.validateFields()
      .then((values) => {
        const item = form.getFieldsValue(true);

        store.DoLogin(item);
      })
      .catch((errorInfo) => {
        console.log('error');
      });
  };

  const [form] = Form.useForm();

  const errMessage = 'Заполните поле!';

  return (

    <div>

      <Form form={form} layout="horizontal" labelCol={{ span: 10 }}>

        <table>
          <tbody>

            <tr>
              <td>

                <Form.Item
                  name="userName"
                  label="Имя пользователя"
                  style={{ marginBottom: '0px' }}
                  rules={[{
                    required: true, min: 1, type: 'string', message: errMessage,
                  }]}
                >
                  <div><Input value={store.data.userName} onChange={changeUserName} /></div>
                </Form.Item>

                <Form.Item
                  name="password"
                  label="Пароль"
                  style={{ marginBottom: '0px' }}
                  rules={[{
                    required: true, min: 1, type: 'string', message: errMessage,
                  }]}
                >
                  <div><Input.Password value={store.data.password} onChange={changePassword} /></div>
                </Form.Item>

              </td>
            </tr>
            <tr>
              <td align="right">
                <Button
                  icon={<SaveOutlined />}
                  onClick={(event) => submit(event)}
                  style={{ background: theme.background, color: theme.color }}
                >
                  Войти

                </Button>
              </td>
            </tr>
          </tbody>
        </table>

      </Form>
    </div>
  );
};

export default observer(LoginForm);
