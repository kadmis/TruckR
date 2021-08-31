import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'datetime'
})
export class DatetimePipe implements PipeTransform {

  transform(date: Date | string, day: number, format: string = 'dd-MM-yyyy, HH:mm:ss'): string {
    date = new Date(date);  // if orginal type was a string
    date.setDate(date.getDate()-day);
    return new DatePipe('pl-PL').transform(date, format);
  }

}
