import React from 'react';
import ReactDOM from 'react-dom';
import MainLayout from './MainLayout';
import 'antd/dist/antd.css';


var App = () => {

    return (
        <MainLayout/>
    );
}

ReactDOM.render(<App />, document.getElementById("root"));