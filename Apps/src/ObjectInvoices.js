import React, { useEffect, useState, useContext } from 'react';
import { observer } from 'mobx-react';

const ObjectInvoices = observer((({ title }) => {
  useEffect(() => {
    document.title = title;

    return function cleanup() {
      document.title = 'MVCApp';
    };
  }, []);

  return (
    <h3>{title}</h3>
  );
}));

export default ObjectInvoices;
