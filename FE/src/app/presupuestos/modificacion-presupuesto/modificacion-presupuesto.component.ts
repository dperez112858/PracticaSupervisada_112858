import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';
import { DetallePresupuesto } from 'src/interfaces/detallePresupuesto';
import { Insumo } from 'src/interfaces/insumo';
import { Presupuesto } from 'src/interfaces/presupuesto';
import { Producto } from 'src/interfaces/producto';
import { ClienteProvider } from 'src/providers/clienteProvider';
import { InsumoProvider } from 'src/providers/insumoProvider';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-modificacion-presupuesto',
  templateUrl: './modificacion-presupuesto.component.html',
  styleUrls: ['./modificacion-presupuesto.component.css']
})
export class ModificacionPresupuestoComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();

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
  detallePresupuesto = {} as DetallePresupuesto;


  constructor(
    private formBuilder: FormBuilder,
    private insumoApi: InsumoProvider,
    private clienteApi: ClienteProvider,
    private presupuestoApi: PresupuestoProvider) { }

  ngOnInit() {
    this.initForm();
    this.ObtenerClientes();
    this.ObtenerInsumos();
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
      cantidad: [0, Validators.required],
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
   

    this.presupuesto = this.formAgregarPresupuesto.value;
    this.pros = this.productoForm.value.productos;
   
    this.presupuesto.productos = this.pros;
      console.log(this.presupuesto);
    if (this.presupuesto != null) {
      this.subscription.add(
        this.presupuestoApi.Crear(this.presupuesto).subscribe({
          next: (res) => {
            console.log(res);
 
          },
          error: () => {
            alert("Error al guardar el presupuesto");
          }
        })
      )
    } else {
      alert("Formulario invalido");
    }

  }

  crearPresupuesto() {
    this.mostrar = true;
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
}
