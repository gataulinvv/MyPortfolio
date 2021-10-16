import React, { useEffect } from 'react';
import { observer } from 'mobx-react'




const Events = observer((({ title }) => {

    useEffect(() => {
        document.title = title;


        return function cleanup() {
            document.title = "MVCApp";
        };
    });

    return (<h3>{title}</h3>)

}))

export default Events;
