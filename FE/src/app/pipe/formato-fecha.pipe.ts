import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'formatoFecha'
})
export class FormatoFechaPipe implements PipeTransform {

  transform(value: any): any {
    if (value) {
      // Assuming value is a valid Date object
      const date = moment.utc(value).local(); // Convert to local time zone
      const day = date.format('DD');
      const month = date.format('MM');
      const year = date.format('YYYY');
      return `${day}/${month}/${year}`;
    }
    return null;
  }
}



// import { Pipe, PipeTransform } from '@angular/core';

// @Pipe({
//   name: 'formatoFecha'
// })
// export class FormatoFechaPipe implements PipeTransform {

//   transform(value: any): any {
//     if (value) {
//       // Assuming value is a valid Date object
//       const date = new Date(value);
//       const day = this.pad(date.getDate(), 2);
//       const month = this.pad(date.getMonth() + 1, 2); // Months are zero-based
//       const year = date.getFullYear();
//       return `${day}/${month}/${year}`;
//     }
//     return null;
//   }

//   private pad(num: number, size: number): string {
//     let numString = num.toString();
//     while (numString.length < size) numString = '0' + numString;
//     return numString;
//   }
// }
