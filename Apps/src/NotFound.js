import React, { useEffect } from 'react';
import { observer } from 'mobx-react'



const NotFound = observer((({ title }) => {

    useEffect(() => {
        document.title = title;

        return function cleanup() {
            document.title = "MVCApp";
        };
    });


    return (<h2 style={{ color: "red"}}>{title}</h2>)
}))

export default NotFound

