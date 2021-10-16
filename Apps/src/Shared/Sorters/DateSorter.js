import moment from 'moment'

const DateSorter = (a, b) => {

	var pattern = /(\d{2})\-(\d{2})\-(\d{4})/	

	var x = moment(a.replace(pattern, '$3-$2-$1'));
	var y = moment(b.replace(pattern, '$3-$2-$1'));

	if (x < y)
		return -1
	else if ((x > y))
		return 1;
	else
		return 0;

}

export default DateSorter;
