import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filtroPresupuesto'
})
export class FiltroPresupuestoPipe implements PipeTransform {

  transform(value: any, arg: any): any {


    if (value.length === 0 || arg === '') {
      return value;
    }

    const presupuestos = [];
    for (const presupuesto of value) {
      if (presupuesto.campania.toLowerCase().indexOf(arg.toLowerCase()) > -1 ||
          presupuesto.cliente.razonSocial.toLowerCase().indexOf(arg.toLowerCase()) > -1) {
        presupuestos.push(presupuesto);
      }
    }

    return presupuestos;
  }

}
