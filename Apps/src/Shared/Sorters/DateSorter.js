/* eslint-disable no-useless-escape */
import moment from 'moment';

const DateSorter = (a, b) => {
  const pattern = /(\d{2})\-(\d{2})\-(\d{4})/;

  const x = moment(a.replace(pattern, '$3-$2-$1'));
  const y = moment(b.replace(pattern, '$3-$2-$1'));

  if (x < y) return -1;
  if ((x > y)) return 1;
  return 0;
};

export default DateSorter;
