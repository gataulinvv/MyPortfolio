import React from 'react';
import ReactDOM from 'react-dom';
import LoginForm from './LoginForm';
import 'antd/dist/antd.css';


var App = () => {

    return (
        <LoginForm/>
    );
}

ReactDOM.render(<App />, document.getElementById("root"));