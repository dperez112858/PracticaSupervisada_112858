import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClienteProvider } from 'src/providers/clienteProvider';
import { DetalleProductoProvider } from 'src/providers/detalleProductoProvider';
import { InsumoProvider } from 'src/providers/insumoProvider';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import { ProductoProvider } from 'src/providers/productoProvider';


@Component({
  selector: 'app-baja-presupuesto',
  templateUrl: './baja-presupuesto.component.html',
  styleUrls: ['./baja-presupuesto.component.css']
})
export class BajaPresupuestoComponent implements OnInit {

  presupuestoForm: FormGroup;
  detalleFormGroup: FormGroup<any>;
  
  
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private presupuestoApi: PresupuestoProvider,
    private insumoApi: InsumoProvider,
    private detalleProductoApi: DetalleProductoProvider,
    private productoApi: ProductoProvider,
    private clienteApi: ClienteProvider
  ) { }

  ngOnInit(): void {
    this.presupuestoForm = this.formBuilder.group({
      campania: ['', Validators.required],
      total: [0, Validators.required],
      detallePresupuesto: this.formBuilder.array([])
    });
  }

  get detallePresupuesto(): FormArray {
    return this.presupuestoForm.get('detallePresupuesto') as FormArray;
  }

  get detalleProductoControls(): AbstractControl[] {
    const producto = this.presupuestoForm.get('producto');
    if (producto) {
      const detalleProducto = producto.get('detalleProducto');
      if (detalleProducto instanceof FormArray) {
        return detalleProducto.controls;
      }
    }
    return [];
  }

  agregarDetallePresupuesto() {
    const detallePresupuestoFormGroup = this.formBuilder.group({
      cantidadProductos: [0, Validators.required],
      precioUnitario: [0, Validators.required],
      producto: this.formBuilder.group({
        nombre: ['', Validators.required],
        banio: [''],
        fechaCreacion: [null],
        costoProducto: [0, Validators.required],
        utilidad: [0, Validators.required],
        costoTotal: [0, Validators.required],
        detalleProducto: this.formBuilder.array([]) // Inicializar como FormArray vacío
      })
    });

    this.detallePresupuesto.push(detallePresupuestoFormGroup);
  }

  eliminarDetallePresupuesto(index: number) {
    this.detallePresupuesto.removeAt(index);
  }

  agregarDetalleProducto(detalleProducto: FormGroup) {
    const detalleProductoFormGroup = this.formBuilder.group({
      cantidad: [0, Validators.required],
      fechaCreacion: [],
      precioInsumo: [0, Validators.required],
      total: [0, Validators.required],
      insumo: this.formBuilder.group({
        descripcion: ['', Validators.required],
        precio: [0, Validators.required],
        
        fechaCompra: []
      })
    });

    const detalleProductoArray = detalleProducto.get('producto.detalleProducto') as FormArray;
    if (detalleProductoArray) {
      detalleProductoArray.push(detalleProductoFormGroup);
    }
  }


  eliminarDetalleProducto(detallePresupuesto: FormGroup, index: number) {
    const detalleProductoArray = detallePresupuesto.get('producto.detalleProducto') as FormArray;
    detalleProductoArray.removeAt(index);
  }

  guardarPresupuesto() {
    if (this.presupuestoForm.valid) {
      const presupuesto = { ...this.presupuestoForm.value };

      // Realizar la acción necesaria para guardar el presupuesto
      // Por ejemplo, enviarlo a través de una API REST o guardarlo en una base de datos

      console.log(presupuesto);
    }
  }
  

}
  





