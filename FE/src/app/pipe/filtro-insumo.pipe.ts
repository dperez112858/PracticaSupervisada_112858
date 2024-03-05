import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filtroInsumo'
})
export class FiltroInsumoPipe implements PipeTransform {

  transform(value: any, arg: any): any {

    
    if (value.length === 0 || arg === '') {
      return value;
    }

    const insumos = [];
    for (const insumo of value) {
      if (insumo.descripcion.toLowerCase().indexOf(arg.toLowerCase()) > -1) {
        insumos.push(insumo);
      }
    }

    return insumos;
  }

}
