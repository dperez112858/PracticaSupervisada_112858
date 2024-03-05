import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { DetalleProducto } from 'src/interfaces/detalleProducto';
import { Insumo } from 'src/interfaces/insumo';
import { Producto } from 'src/interfaces/producto';
import { DetalleProductoProvider } from 'src/providers/detalleProductoProvider';
import { InsumoProvider } from 'src/providers/insumoProvider';
import { ProductoProvider } from 'src/providers/productoProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-alta-producto',
  templateUrl: './alta-producto.component.html',
  styleUrls: ['./alta-producto.component.css']
})
export class AltaProductoComponent implements OnInit {

  respuesta: any;
  private subscription = new Subscription();
  listaInsumos: any[];
  insumosCargados: any[];
  listaProducto: any[];
  listaDetalleProducto: any[];
  insumo = {} as Insumo;
  producto = {} as Producto;
  ver = false;
  verDetalle = false;
  mostrarPrecio = false;
  costoProducto: number;
  costoTotal: number;
  formAgregarProducto: FormGroup;
  formAgregarDetalleProducto: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private insumoApi: InsumoProvider,
    private detalleProductoApi: DetalleProductoProvider,
    private productoApi: ProductoProvider) { }


  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.ObtenerInsumos();
    this.ver = false;

    this.formAgregarProducto = this.formBuilder.group({
      nombre: ['', Validators.required],
      Banio: ['', Validators.required],
      Utilidad: ['0'],
      costoProducto: ['0'],
      CostoTotal: ['0']
    })

    this.formAgregarDetalleProducto = this.formBuilder.group({
      Cantidad: ['', Validators.required],
      PrecioInsumo: ['', Validators.required],
      InsumoId: [''],
      Total: ['0']
    })
  }

  async ObtenerInsumos() {
    this.subscription.add(
      this.insumoApi.ObtenerTodos().subscribe((data) => {
        if (data.ok) {
          this.listaInsumos = data.listaInsumos;
        } else {
          alert(data.error);
        }
      }))
  }

  ObtenerInsumoPorId(id: any) {
    // console.log(id);
    this.subscription.add(
      this.insumoApi.ObtenerPorId(id).subscribe((data) => {
        if (data) {
          this.insumo = data;
          this.mostrarPrecio = true;
        } else {
          alert(data);
        }
      }))
  }

  obtenerProductos() {
    this.subscription.add(
      this.productoApi.ObtenerTodos().subscribe((data) => {
        if (data) {
          this.listaProducto = data;
        } else {
          alert(data);
        }
      })
    )
  }

  actualizarCostoProducto() {
    this.subscription.add(
      this.productoApi.actualizarCostoProducto().subscribe((data) => {
        if (data) {
          //console.log("producto:" + data);

        } else {
          alert(data);
        }
      })
    )
  }

  ObtenerUltimo() {
    this.subscription.add(
      this.productoApi.ObtenerUltimo().subscribe((data) => {
        if (data) {
          this.producto = data;
          this.formAgregarProducto.patchValue({
            costoProducto: data.costoProducto,
            utilidad: data.utilidad,
            costoTotal: data.costoTotal
          })
        } else {
          alert(data);
        }
      })
    )
  }

  // async ObtenerDetalles() {
  //   this.subscription.add(
  //     this.detalleProductoApi.ObtenerTodos().subscribe((data) => {
  //       if (data.ok) {
  //         this.listaDetalleProducto = data.listaDetalleProducto;
  //       } else {
  //         alert(data.error);
  //       }
  //     }))
  // }

  guardarProducto() {
    if (this.formAgregarProducto.valid) {
      this.subscription.add(
        this.productoApi.Crear(this.formAgregarProducto.value).subscribe({
          next: (res) => {
            this.ver = true;
            this.respuesta = res;
            alert("Guardó Producto");
          },
          error: () => {
          },
        })
      )
    } else {
      alert("Error al ingresar el producto");
    }
  }

  async ObtenerTodosUltimoProducto() {
    this.subscription.add(
      this.detalleProductoApi.ObtenerTodosUltimoProducto().subscribe((data) => {
        if (data.ok) {
          this.listaDetalleProducto = data.listaDetalleProducto;
        } else {
          alert("Error al obtener los detalles del producto");
        }
      }))
  }


  guardarDetalleProducto() {
    if (this.formAgregarDetalleProducto.valid) {
      this.subscription.add(
        this.detalleProductoApi.Crear(this.formAgregarDetalleProducto.value).subscribe({
          next: (res) => {
            alert("Guardó Detalle de Producto");
            this.actualizarCostoProducto();
            this.ObtenerUltimo();
            this.ObtenerTodosUltimoProducto();
            this.verDetalle = true;
            this.formAgregarDetalleProducto.reset();
            this.mostrarPrecio = false;
          },
          error: () => {
            alert("Error al guardar los detalles del producto");
          },
        })
      )
    } else {
      alert("Error al guardar detalle de producto")
    }
  }



  // guardarProducto() {
  //   Swal.fire({
  //     title: '¿Desea ingresar el producto?',
  //     icon: 'question',
  //     showCancelButton: true,
  //     confirmButtonColor: '#3085d6',
  //     cancelButtonColor: '#d33',
  //     confirmButtonText: 'Sí',
  //     cancelButtonText: 'No'
  //   }).then((result) => {
  //     if (result.isConfirmed) {
  //       if (this.formAgregarProducto.valid) {
  //         this.subscription.add(
  //           this.productoApi.Crear(this.formAgregarProducto.value).subscribe({
  //             next: (res) => {
  //               console.log(res)
  //               this.verFormInsumo = true;
  //               Swal.fire({
  //                 title: 'Producto ingresado correctamente',
  //                 icon: 'success',
  //                 confirmButtonText: "Ok",
  //               });
  //             },
  //             error: () => {
  //               Swal.fire({
  //                 title: 'Error al ingresar el producto ',
  //                 icon: 'error',
  //                 confirmButtonText: "Ok",
  //               });
  //             },
  //           })
  //         )
  //       } else {
  //         Swal.fire({
  //           title: 'Formulario inválido',
  //           icon: 'error',
  //           confirmButtonText: "Ok",
  //         });
  //       }
  //     }
  //   })
  // }



  // guardarDetalleProducto() {
  //   Swal.fire({
  //     title: '¿Desea ingresar el insumo?',
  //     icon: 'question',
  //     showCancelButton: true,
  //     confirmButtonColor: '#3085d6',
  //     cancelButtonColor: '#d33',
  //     confirmButtonText: 'Sí',
  //     cancelButtonText: 'No'
  //   }).then((result) => {
  //     if (result.isConfirmed) {
  //       if (this.formAgregarDetalleProducto.valid) {
  //         this.subscription.add(
  //           this.detalleProductoApi.Crear(this.formAgregarDetalleProducto.value).subscribe({
  //             next: (res) => {
  //               console.log(res)
  //               Swal.fire({
  //                 title: 'Insumo ingresado correctamente',
  //                 icon: 'success',
  //                 confirmButtonText: "Ok",
  //               });

  //               this.costoProducto += this.formAgregarDetalleProducto.value.PrecioInsumo;
  //               console.log("Costo Producto: " + this.costoProducto);
  //             },
  //             error: () => {
  //               Swal.fire({
  //                 title: 'Error al ingresar el insumo ',
  //                 icon: 'error',
  //                 confirmButtonText: "Ok",
  //               });
  //             },
  //           })
  //         )
  //       } else {
  //         Swal.fire({
  //           title: 'Formulario inválido',
  //           icon: 'error',
  //           confirmButtonText: "Ok",
  //         });
  //       }
  //     }
  //   })
  // }


}
