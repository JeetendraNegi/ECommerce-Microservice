import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'countArrayItem'
})
export class CountArrayItemPipe implements PipeTransform {

  transform(Value: any): number {   
    return Value.length;
  }
}
