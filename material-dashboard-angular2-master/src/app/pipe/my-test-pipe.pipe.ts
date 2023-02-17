import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'myTestPipe'
})
export class MyTestPipePipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    console.log(value);    
    return value;
  }

}
