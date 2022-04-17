import React from 'react';
import ReactDOM from 'react-dom';
import LoginForm from './LoginForm';
import 'antd/dist/antd.css';

const App = () => (
  <LoginForm />
);

ReactDOM.render(<App />, document.getElementById('root'));
