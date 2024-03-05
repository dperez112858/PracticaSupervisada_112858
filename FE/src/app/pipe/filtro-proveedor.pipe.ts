import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filtroProveedor'
})
export class FiltroProveedorPipe implements PipeTransform {

   transform(value: any, arg: any): any {}


  //   if (value.length === 0 || arg === '') {
  //     return value;
  //   }

  //   const proveedores = [];
  //   for (const proveedor of value) {
  //     if (proveedor.proveedor.razonSocial.toLowerCase().indexOf(arg.toLowerCase()) > -1
  //       || proveedor.numero.toLowerCase().indexOf(arg.toLowerCase()) > -1) {
  //         proveedor.push(proveedor);
  //     }
  //   }

  //   return proveedores;
  // }

}
