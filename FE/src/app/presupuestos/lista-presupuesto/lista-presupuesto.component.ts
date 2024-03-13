import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Insumo } from 'src/interfaces/insumo';
import { Presupuesto } from 'src/interfaces/presupuesto';
import { Producto } from 'src/interfaces/producto';
import { ClienteProvider } from 'src/providers/clienteProvider';
import { DetalleProductoProvider } from 'src/providers/detalleProductoProvider';
import { InsumoProvider } from 'src/providers/insumoProvider';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import { ProductoProvider } from 'src/providers/productoProvider';
import Swal from 'sweetalert2';
import { DatePipe } from '@angular/common';
import { Guid } from 'guid-typescript';
import { DetalleProductoT } from 'src/interfaces/detalleProductoT';
import { ProductoT } from 'src/interfaces/productoT';
import { DetallePresupuestoT } from 'src/interfaces/detallePresupuestoT';
import { PresupuestoCompleto } from 'src/interfaces/presupuesto-completo';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { DetalleProductoPdf } from 'src/interfaces/detalle-producto-pdf';

@Component({
  selector: 'app-lista-presupuesto',
  templateUrl: './lista-presupuesto.component.html',
  styleUrls: ['./lista-presupuesto.component.css']
})
export class ListaPresupuestoComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  public page: number;
  listaPresupuestos: any = [];
  listaClientes: any = [];
  listaInsumos: any[];
  insumoEncontrado: Insumo;
  mostrarPrecio = false;
  productoForm: FormGroup;
  formAgregarPresupuesto: FormGroup;
  clientes: any[];
  insumos: any[];
  mostrar = false;
  presupuesto = {} as Presupuesto;
  pros: Producto[];
  filtroPresupuesto: string = '';
  presupuestoCompleto = {} as PresupuestoCompleto;
  dia: any;
  diaHora: any;
  fecha: number;
  listaDetalleProducto: any = [];
  mostrarD: boolean = false;


  constructor(
    private formBuilder: FormBuilder,
    private insumoApi: InsumoProvider,
    private clienteApi: ClienteProvider,
    public datepipe: DatePipe,
    private presupuestoApi: PresupuestoProvider,
    private productoApi: ProductoProvider) { }

  ngOnInit() {
    this.initForm();
    this.ObtenerClientes();
    this.ObtenerInsumos();
    this.ObtenerPresupuestos();
    this.ObtenerFecha();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  initForm() {
    this.formAgregarPresupuesto = this.formBuilder.group({
      clienteId: ['', Validators.required],
      campania: ['', Validators.required],
      aceptado: [false],
      activo: [true]
    })

    this.productoForm = this.formBuilder.group({
      productos: this.formBuilder.array([])
    });
  }

  ObtenerFecha() {
    this.fecha = Date.now();
    this.dia = this.datepipe.transform(this.fecha, 'dd/MM/yyyy');
    this.diaHora = this.datepipe.transform(this.fecha, 'yyyy-MM-dd-hhmmss');
  }


  ObtenerPreupuestoPorId(id: any) {
    this.subscription.add(
      this.presupuestoApi.ObtenerPresupuestosPorId(id).subscribe((data) => {
        if (data) {
          this.presupuestoCompleto = data;
          console.log(this.presupuestoCompleto);
        } else {
          alert(data);
        }
      }))
  }

  AlternarDetalle() {
    this.mostrarD = !this.mostrarD;;
  }

  ObtenerDetalleProductoPorId(id: any) {
    this.subscription.add(
      this.productoApi.ObtenerDetallePorId(id).subscribe((data) => {
        if (data) {
          this.listaDetalleProducto = data.listaDetalleProducto;
          this.AlternarDetalle();
          console.log(this.listaDetalleProducto);
        } else {
          alert(data);
        }
      }))
  }

  productos(): FormArray {
    return this.productoForm.get('productos') as FormArray;
  }

  newProducto(): FormGroup {
    return this.formBuilder.group({
      nombreProducto: ['', Validators.required],
      banioProducto: ['', Validators.required],
      cantidadProductos: ['', Validators.required],
      utilidad: [0],
      costoProducto: [0],
      costoTotal: [0],
      DetalleProductos: this.formBuilder.array([])
    });
  }

  addProducto() {
    this.productos().push(this.newProducto());
  }

  removeProducto(productoIndex: number) {
    this.productos().removeAt(productoIndex);
  }
  productoInsumo(productoIndex: number): FormArray {
    return this.productos()
      .at(productoIndex)
      .get('DetalleProductos') as FormArray;
  }
  newInsumo(): FormGroup {
    return this.formBuilder.group({
      Cantidad: [0, Validators.required],
      fechaCreacion: [],
      precioInsumo: [0, Validators.required],
      total: [0, Validators.required],
      InsumoId: []
    });
  }

  addProductoInsumo(productoIndex: number) {
    this.productoInsumo(productoIndex).push(this.newInsumo());
  }

  removeProductoInsumo(productoIndex: number, insumoIndex: number) {
    this.productoInsumo(productoIndex).removeAt(insumoIndex);
  }

  onSubmit() {
    Swal.fire({
      title: '¿Desea ingresar el presupuesto?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formAgregarPresupuesto.valid && this.productoForm.valid) {
          this.presupuesto = this.formAgregarPresupuesto.value;
          this.pros = this.productoForm.value.productos;
          this.presupuesto.productos = this.pros;
          console.log(this.presupuesto);
          this.subscription.add(
            this.presupuestoApi.Crear(this.presupuesto).subscribe({
              next: (res) => {
                console.log(res);
                Swal.fire({
                  title: 'Presupuesto ingresado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",

                });
                this.ObtenerPresupuestos();
              },
              error: () => {
                Swal.fire({
                  title: 'Error al ingresar el presupuesto',
                  icon: 'error',
                  confirmButtonText: "Ok",
                });
              },
            })
          )
        } else {
          Swal.fire({
            title: 'Formulario inválido',
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      }
    })
  }

  crearPresupuesto() {
    this.mostrar = true;
  }


  limpiarFormularios() {
    this.formAgregarPresupuesto.reset();
    this.productoForm.reset();
  }

  ObtenerClientes() {
    this.subscription.add(
      this.clienteApi.ObtenerTodos().subscribe((data) => {
        if (data.ok) {
          this.listaClientes = data.listaClientes;
        }
        else {
          Swal.fire({
            title: 'Error al ingresar el cliente' + (data.error), //REVISAR
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  ObtenerInsumos() {
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
          this.insumoEncontrado = data;
          this.mostrarPrecio = true;
        } else {
          alert(data);
        }
      }))
  }

  ObtenerPresupuestos() {
    this.subscription.add(
      this.presupuestoApi.ObtenerTodos().subscribe((data) => {
        if (data.ok) {
          this.listaPresupuestos = data.listaPresupuestos;
        } else {
          alert(data.error);
        }
      }))
  }

  eliminarPresupuesto(id: string) {
    Swal.fire({
      title: '¿Desea eliminar el presupuesto?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.presupuestoApi.Eliminar(id).subscribe(() => {
          if (id) {
            Swal.fire({
              title: 'Presupuesto eliminado correctamente',
              icon: 'success',
              confirmButtonText: "Ok",
            });
            this.ObtenerPresupuestos();
          } else {
            Swal.fire({
              title: 'Error al eliminar el presupuesto',
              icon: 'error',
              confirmButtonText: "Ok",
            });
          }
        })
      }
    })
  }

  pdf() {
    var data = document.getElementById('presupuesto');
    if (data !== null) {
      html2canvas(data).then(canvas => {
        // Few necessary setting options  
        let imgWidth = 208;
        let imgHeight = canvas.height * imgWidth / canvas.width;
        const contentDataURL = canvas.toDataURL('image/png')
        let pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF  
        let position = 0;
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
        pdf.save('Presupuesto_' + this.presupuestoCompleto.numero + '_' + this.diaHora + '.pdf'); // Nombre de la salida PDF

      });
    }
  }




  get controlCompania(): FormControl {
    return this.formAgregarPresupuesto.controls['campania'] as FormControl;
  }
  get controlNombre(): FormControl {
    return this.productoForm.controls['nombreProducto'] as FormControl;
  }
  // get controlBanio(): FormControl {
  //   return this.productoForm.controls['banio'] as FormControl;
  // }
  // get controlCantidad(): FormControl {
  //   return this.productoForm.controls['Cantidad'] as FormControl;
  // }
  // get controlPrecioInsumo(): FormControl {
  //   return this.productoForm.controls['PrecioInsumo'] as FormControl;
  // }





  // private subscription = new Subscription();

  // productos: Producto[];


  // public page: number;
  // presupuestoT: any;
  // listaPresupuestos: any = [];
  // formAgregarPresupuesto: FormGroup;
  // listaClientes: any = [];
  // detallePresupuestoT = {} as DetallePresupuestoT;
  // listaDetallePresupuestoT: DetallePresupuestoT[] = [];


  // idInsumo: string;
  // nombreInsumo: string;
  // respuesta: any;
  // listaInsumos: any[];
  // insumosCargados: any[];
  // listaProducto: any[];
  // insumoEncontrado: Insumo;
  // listaInsumosCargados: Insumo[];
  // producto = {} as Producto;
  // ver = false;
  // verDetalle = false;
  // mostrarPrecio = false;
  // costoProducto: number;
  // costoTotal: number;
  // formAgregarProducto: FormGroup;
  // formAgregarDetalleProducto: FormGroup;
  // detalleProductoT: DetalleProductoT;
  // productoT: ProductoT;
  // listaDetalleProducto: DetalleProductoT[] = [];
  // ProductoTtotales: any;
  // mostrarListaProductos = false;
  // mostrarInsumos = false;
  // listaActualDetalleProducto: DetalleProductoT[] = [];

  // constructor(
  //   private router: Router,
  //   private formBuilder: FormBuilder,
  //   private presupuestoApi: PresupuestoProvider,
  //   private insumoApi: InsumoProvider,
  //   private detalleProductoApi: DetalleProductoProvider,
  //   private productoApi: ProductoProvider,
  //   private clienteApi: ClienteProvider
  // ) { }

  // ngOnInit(): void {

  //   this.ObtenerPresupuestos();
  //   this.ObtenerClientes();
  //   this.IniformAgregarPresupuesto();
  //   this.IniformAgregarProducto();
  //   this.IniformAgregarDetalleProducto();
  //   this.ObtenerInsumos();
  //   this.ver = false;
  // }

  // ngOnDestroy(): void {
  //   this.subscription.unsubscribe();
  // }

  // IniformAgregarPresupuesto() {
  //   this.formAgregarPresupuesto = this.formBuilder.group({
  //     clienteId: ['', Validators.required],
  //     campania: ['', Validators.required]
  //   })
  // }

  // IniformAgregarProducto() {
  //   this.formAgregarProducto = this.formBuilder.group({
  //     nombre: ['', Validators.required],
  //     banio: ['', Validators.required],
  //     Cantidad: [, Validators.required],
  //     utilidad: [0],
  //     costoProducto: [0],
  //     costoTotal: [0]
  //   })
  // }

  // IniformAgregarDetalleProducto() {

  //   this.formAgregarDetalleProducto = this.formBuilder.group({
  //     Cantidad: [, Validators.required],
  //     PrecioInsumo: [, Validators.required],
  //     Total: [0],
  //     InsumoId: ['']
  //   })
  // }

  // crearPresupuestoT() {
  //   if (this.formAgregarPresupuesto.valid) {
  //     this.presupuestoT = this.formAgregarPresupuesto.value;
  //     this.presupuestoT.Id = Guid.create().toString();
  //     this.presupuestoT.Activo = true;
  //     this.presupuestoT.Aceptado = false;
  //     console.log("PresupuestoT creado");
  //     console.log(this.presupuestoT);
  //   } else {
  //     Swal.fire({
  //       title: 'Debe ingresar cliente y campaña para continuar', 
  //       icon: 'error',
  //       confirmButtonText: "Ok",
  //     });
  //   }
  // }


  // crearProductoT() {
  //   if (this.formAgregarProducto.valid) {
  //     this.productoT = this.formAgregarProducto.value;
  //     this.productoT.Id = Guid.create().toString();
  //     this.productoT.FechaCreacion = Date.now();
  //     console.log("ProductoT creado:");
  //     console.log(this.productoT);
  //   } else {
  //     alert("Error al ingresar el producto");
  //   }
  // }

  // guardarDetalleProductoT() {
  //   debugger
  //   if (this.formAgregarDetalleProducto.valid) {
  //     this.detalleProductoT = this.formAgregarDetalleProducto.value;
  //     this.detalleProductoT.Id = Guid.create().toString();
  //     this.detalleProductoT.FechaCreacion = Date.now();
  //     this.detalleProductoT.ProductoId = this.productoT.Id.toString();
  //     this.detalleProductoT.NombreProducto = this.productoT.nombre;
  //     this.detalleProductoT.NombreBanio = this.productoT.banio;
  //     this.detalleProductoT.Total = this.formAgregarDetalleProducto.value.Cantidad * this.formAgregarDetalleProducto.value.PrecioInsumo;
  //     this.listaDetalleProducto.push(this.detalleProductoT);
  //     this.listaActualDetalleProducto.push(this.detalleProductoT);

  //     console.log("lista de detalleProductoT: ");
  //     console.log(this.detalleProductoT);
  //     this.formAgregarDetalleProducto.reset();
  //     this.actualizarProductoT();
  //     this.mostrarPrecio = false;
  //     this.mostrarInsumos = true;

  //   } else {
  //     alert("Error al almacenar el detalle de presupuesto");
  //   }
  // }

  // actualizarProductoT() {
  //   if (this.productoT.Id != null)
  //     this.productoT.detalleProductoT = this.listaDetalleProducto;
  //   this.productoT.costoProducto = 0;
  //   this.productoT.utilidad = 0;
  //   for (let item of this.productoT.detalleProductoT)
  //     this.productoT.costoProducto += item.Total,
  //       this.productoT.utilidad += (item.Total * 0.4),
  //       this.productoT.costoTotal = this.productoT.costoProducto + this.productoT.utilidad
  //   this.ProductoTtotales = this.productoT;
  //   this.mostrarListaProductos = true;
  //   console.log("ProductoT con totales actualizados: ");
  //   console.log(this.productoT);
  // }

  // guadarDetallePresupuestoT() {
  //   if (this.productoT != null) {
  //     this.listaActualDetalleProducto = [];
  //     this.detallePresupuestoT.CantidadProductos = 1;
  //     this.detallePresupuestoT.Id = Guid.create().toString();
  //     this.detallePresupuestoT.PrecioUnitario = this.productoT.costoTotal;
  //     this.detallePresupuestoT.Total = this.detallePresupuestoT.PrecioUnitario * this.detallePresupuestoT.CantidadProductos;
  //     this.detallePresupuestoT.ProductoId = this.productoT.Id;
  //     this.detallePresupuestoT.NombreProducto = this.productoT.nombre;
  //     this.detallePresupuestoT.BanioProducto = this.productoT.banio;
  //   }
  //   this.listaDetallePresupuestoT.push(this.detallePresupuestoT);
  //   this.formAgregarProducto.reset();
  //   console.log("lista de detallePresupuestoT: ");
  //   console.log(this.listaDetallePresupuestoT);
  // }

  // crearDetalleProductoNuevo(){
  //   this.IniformAgregarProducto();
  // }

  // guardarDetalleProductoNuevo() {
  //   if (this.formAgregarProducto.valid) {
  //     this.productoT = this.formAgregarProducto.value;
  //     this.productoT.Id = Guid.create().toString();
  //     this.productoT.FechaCreacion = Date.now();
  //     console.log("ProductoT creado:");
  //     console.log(this.productoT);
  //   } else {
  //     alert("Error al ingresar el producto");
  //   }
  //   if (this.productoT != null) {
  //     this.detallePresupuestoT.CantidadProductos = 1;
  //     this.detallePresupuestoT.Id = Guid.create().toString();
  //     this.detallePresupuestoT.PrecioUnitario = this.productoT.costoTotal;
  //     this.detallePresupuestoT.Total = this.detallePresupuestoT.PrecioUnitario * this.detallePresupuestoT.CantidadProductos;
  //     this.detallePresupuestoT.ProductoId = this.productoT.Id;
  //     this.detallePresupuestoT.NombreProducto = this.productoT.nombre;
  //     this.detallePresupuestoT.BanioProducto = this.productoT.banio;

  //     this.listaDetallePresupuestoT.push(this.detallePresupuestoT);
  //     this.formAgregarProducto.reset();
  //     console.log("lista de detallePresupuestoT: ");
  //     console.log(this.listaDetallePresupuestoT);
  //   } else {
  //     alert("Error al ingresar el producto");
  //   }
  // }

  // guardar() { }

  // ObtenerPresupuestos() {
  //   this.subscription.add(
  //     this.presupuestoApi.ObtenerTodos().subscribe((data) => {
  //       if (data.ok) {
  //         this.listaPresupuestos = data.listaPresupuestos;
  //       } else {
  //         alert(data.error);
  //       }
  //     }))
  // }

  // ObtenerClientes() {
  //   this.subscription.add(
  //     this.clienteApi.ObtenerTodos().subscribe((data) => {
  //       if (data.ok) {
  //         this.listaClientes = data.listaClientes;
  //       }
  //       else {
  //         Swal.fire({
  //           title: 'Error al ingresar el cliente' + (data.error), //REVISAR
  //           icon: 'error',
  //           confirmButtonText: "Ok",
  //         });
  //       }
  //     })
  //   )
  // }

  // eliminarPresupuesto(id: string) {
  //   Swal.fire({
  //     title: '¿Desea eliminar el presupuesto?',
  //     icon: 'question',
  //     showCancelButton: true,
  //     confirmButtonColor: '#3085d6',
  //     cancelButtonColor: '#d33',
  //     confirmButtonText: 'Sí',
  //     cancelButtonText: 'No'
  //   }).then((result) => {
  //     if (result.isConfirmed) {
  //       this.presupuestoApi.Eliminar(id).subscribe(() => {
  //         if (id) {
  //           Swal.fire({
  //             title: 'Presupuesto eliminado correctamente',
  //             icon: 'success',
  //             confirmButtonText: "Ok",
  //           });
  //           this.ObtenerPresupuestos();
  //         } else {
  //           Swal.fire({
  //             title: 'Error al eliminar el presupuesto',
  //             icon: 'error',
  //             confirmButtonText: "Ok",
  //           });
  //         }
  //       })
  //     }
  //   })
  // }

  // ObtenerInsumoPorId(id: any) {
  //   this.subscription.add(
  //     this.insumoApi.ObtenerPorId(id).subscribe((data) => {
  //       if (data) {
  //         this.insumoEncontrado = data;
  //         this.mostrarPrecio = true;
  //       } else {
  //         alert(data);
  //       }
  //     }))
  // }

  // ObtenerInsumos() {
  //   this.subscription.add(
  //     this.insumoApi.ObtenerTodos().subscribe((data) => {
  //       if (data.ok) {
  //         this.listaInsumos = data.listaInsumos;
  //       } else {
  //         alert(data.error);
  //       }
  //     }))
  // }

  // limpiarFormularios() {
  //   this.formAgregarPresupuesto.reset();
  //   this.formAgregarProducto.reset();
  //   this.formAgregarDetalleProducto.reset();
  // }

  // limpiarFormAgregarPresupuesto() {
  //   this.formAgregarPresupuesto.reset();
  // }

  // limpiarFormAgregarProducto() {
  //   this.formAgregarProducto.reset();
  // }

  // limpiarFormAgregarDetalleProducto() {
  //   this.formAgregarDetalleProducto.reset();
  // }

  // get controlCompania(): FormControl {
  //   return this.formAgregarPresupuesto.controls['campania'] as FormControl;
  // }
  // get controlNombre(): FormControl {
  //   return this.formAgregarProducto.controls['nombre'] as FormControl;
  // }
  // get controlBanio(): FormControl {
  //   return this.formAgregarProducto.controls['banio'] as FormControl;
  // }
  // get controlCantidad(): FormControl {
  //   return this.formAgregarDetalleProducto.controls['Cantidad'] as FormControl;
  // }
  // get controlPrecioInsumo(): FormControl {
  //   return this.formAgregarDetalleProducto.controls['PrecioInsumo'] as FormControl;
  // }


  // EditarProducto() { }
  // EditarPresupuesto(Id: any) { }
  // eliminarDetalleProducto(Id: string) { }
  // editarInsumo(Id: string) { }
  // eliminarInsumo(Id: string) { }


}
