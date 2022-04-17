/* eslint-disable no-restricted-syntax */
/* eslint-disable guard-for-in */
import { Input } from 'antd';
import React from 'react';

const StringFilter = (props) => {
  const {
    columnName, columnTitle, dataSource, parentViewStore,
  } = props;

  const filterChange = (event) => {
    const matrix = parentViewStore.filterMatrix;

    const filterValue = event.target.value;

    for (const rowIndex in dataSource) {
      const dataRow = dataSource[rowIndex];

      const matrixRow = matrix[dataRow.id];

      const matrixColumn = matrixRow.columns[columnName];

      const cellValue = dataRow[columnName];

      const isInclude = cellValue.toString().toLowerCase().includes(filterValue.toLowerCase());

      if (!isInclude) {
        matrixRow.display = false;
        matrixColumn.match = false;
      } else {
        matrixColumn.match = true;
        matrixRow.display = true;

        for (const colName in matrixRow.columns) {
          const { match } = matrixRow.columns[colName];

          if (!match) {
            matrixRow.display = false;
          }
        }
      }
    }

    const filteredData = dataSource.filter(

      (entry) => matrix[entry.id].display,
    );

    parentViewStore.SetFilteredData(filteredData);
  };

  return (
    <>
      {columnTitle}
      <Input onClick={(e) => e.stopPropagation()} onChange={(e) => filterChange(e)} />
    </>
  );
};

export default StringFilter;
