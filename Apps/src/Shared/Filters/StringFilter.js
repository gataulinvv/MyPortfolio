import { Input } from 'antd';
import React from 'react';



const StringFilter = (props) => {

	const { columnName, columnTitle, dataSource, parentViewStore } = props;


	const filterChange = (event) => {

		var matrix = parentViewStore.filterMatrix;

		const filterValue = event.target.value;

		for (var rowIndex in dataSource) {

			var dataRow = dataSource[rowIndex];

			var matrixRow = matrix[dataRow.id];

			var matrixColumn = matrixRow.columns[columnName];

			var cellValue = dataRow[columnName];

			var isInclude = cellValue.toString().toLowerCase().includes(filterValue.toLowerCase());

			if (!isInclude) {
				matrixRow.display = false;
				matrixColumn.match = false;
			}
			else {
				matrixColumn.match = true;
				matrixRow.display = true;

				for (var colName in matrixRow.columns) {

					var match = matrixRow.columns[colName].match

					if (!match) {
						matrixRow.display = false;
					}
				}

			}
		}

		const filteredData = dataSource.filter(

			entry => matrix[entry.id].display
		);
		
		parentViewStore.SetFilteredData(filteredData);

	}

	return (
		<>
			{columnTitle}
			<Input onClick={(e) => e.stopPropagation()}  onChange={(e) => filterChange(e)} />
		</>
	)
}

export default StringFilter;