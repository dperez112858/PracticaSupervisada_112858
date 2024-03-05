import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteProvider } from '../../../providers/clienteProvider'
import { FacturaProvider } from '../../../providers/facturaProvider'
import { Cliente } from 'src/interfaces/cliente';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-alta-factura',
  templateUrl: './alta-factura.component.html',
  styleUrls: ['./alta-factura.component.css']
})
export class AltaFacturaComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();

  clienteCompleto = {} as Cliente;
  formAgregarComprobante: FormGroup;
  listaClientes: any[];

  constructor(
    private router: Router,
    private clienteApi: ClienteProvider,
    private facturaApi: FacturaProvider,
    private formBuilder: FormBuilder) {

    this.formAgregarComprobante = this.formBuilder.group({
      fecha: ['', Validators.required],
      numero: ['', Validators.required],
      tipoComprobante: [, Validators.required],
      netoGravado: [, Validators.required],
      iva: [],
      dolar: [false],
      tipoCambio: [1],
      total: [],
      ClienteId: ['', Validators.required],
    })
    this.formAgregarComprobante.get('netoGravado')?.valueChanges.subscribe(value => {
      if (value) {
        this.calcularIvaTotal(value);
        console.log(value);
      }
    });
  }

  ngOnInit(): void {
    this.ObtenerClientes();

  }

  calcularIvaTotal(netoGravado: number) {
    const iva = netoGravado * 0.21;
    const total = netoGravado + iva;
    this.formAgregarComprobante.patchValue({ iva: iva });
    this.formAgregarComprobante.patchValue({ total: total });
  }

  borrarFormulario() {
    this.formAgregarComprobante.reset();
  }

  ObtenerClientes() {
    this.subscription.add(
      // servicio inyectado.método(parámetros en caso de ser necesario).subscribe(resultado)
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

  guardar() {
    Swal.fire({
      title: '¿Desea ingresar la factura?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formAgregarComprobante.valid) {
          this.subscription.add(
            this.facturaApi.Crear(this.formAgregarComprobante.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Factura ingresada correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
              },
              error: () => {
                Swal.fire({
                  title: 'Error al ingresar la factura',
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

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get controlfecha(): FormControl {
    return this.formAgregarComprobante.controls['fecha'] as FormControl;
  }
  get controlnumero(): FormControl {
    return this.formAgregarComprobante.controls['numero'] as FormControl;
  }
  get controltipoComprobante(): FormControl {
    return this.formAgregarComprobante.controls['tipoComprobante'] as FormControl;
  }
  get controlnetoGravado(): FormControl {
    return this.formAgregarComprobante.controls['netoGravado'] as FormControl;
  }
  get controliva(): FormControl {
    return this.formAgregarComprobante.controls['iva'] as FormControl;
  }
  get controldolar(): FormControl {
    return this.formAgregarComprobante.controls['dolar'] as FormControl;
  }
  get controltipoCambio(): FormControl {
    return this.formAgregarComprobante.controls['tipoCambio'] as FormControl;
  }
  get controltotal(): FormControl {
    return this.formAgregarComprobante.controls['total'] as FormControl;
  }
  get controlClienteId(): FormControl {
    return this.formAgregarComprobante.controls['ClienteId'] as FormControl;
  }
}
