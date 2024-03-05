import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filtroCliente'
})
export class FiltroClientePipe implements PipeTransform {

  transform(value: any, arg: any): any {


    if (value.length === 0 || arg === '') {
      return value;
    }

    const clientes = [];
    for (const cliente of value) {
      if (cliente.cliente.razonSocial.toLowerCase().indexOf(arg.toLowerCase()) > -1
        || cliente.numero.toLowerCase().indexOf(arg.toLowerCase()) > -1) {
        clientes.push(cliente);
      }
    }

    return clientes;
  }

}
