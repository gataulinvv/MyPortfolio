﻿import React from 'react';


const StringSorter = ( a,b ) => {

	if (a < b)
		return -1
	else if ((a > b))
		return 1;
	else
		return 0;
}

export default StringSorter;
