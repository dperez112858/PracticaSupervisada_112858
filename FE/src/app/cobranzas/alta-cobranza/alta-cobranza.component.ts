import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ClienteProvider } from 'src/providers/clienteProvider';
import { CobranzaProvider } from 'src/providers/cobranzaProvider';
import { FacturaProvider } from 'src/providers/facturaProvider';
import { formatDate } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-alta-cobranza',
  templateUrl: './alta-cobranza.component.html',
  styleUrls: ['./alta-cobranza.component.css']
})
export class AltaCobranzaComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();

  cobranzaForm: FormGroup;
  listaClientes: any[];
  listaFacturas: any[];

  constructor(
    private formBuilder: FormBuilder,
    private clienteApi: ClienteProvider,
    private facturaApi: FacturaProvider,
    private cobranzaApi: CobranzaProvider) {

  }

  ngOnInit() {
    this.initForm();
    this.ObtenerClientes();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  initForm() {
    this.cobranzaForm = this.formBuilder.group({
      clienteId: ['', Validators.required],
      fechaLiquidacion: ['', Validators.required],
      total: [, Validators.required],
      detalleCobranza: this.formBuilder.array([])
    });
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

  ObtenerPorCliente(id: any) {
    this.facturaApi.ObtenerPorCliente(id).subscribe((data) => {
      if (data.ok) {
        this.listaFacturas = data.listaFacturas;
      } else {
        alert(data.error);
      }
    })
  }



  get detalleCobranza(): FormArray {
    return this.cobranzaForm.get('detalleCobranza') as FormArray;
  }

  addDetalleCobranza() {
    const detalleCobranza = this.formBuilder.group({
      importeCobrado: [, Validators.required],
      importeRetenido: [, Validators.required],
      fechaPago: ['', Validators.required],
      tipoPagoId: ['', Validators.required],
      importeTotal: []
    });

    this.detalleCobranza.push(detalleCobranza);
  }

  removeDetalleCobranza(index: number) {
    this.detalleCobranza.removeAt(index);
  }



  // calcularTotal(index: number) {
  //   const detalleCobranza = this.detalleCobranza.controls[index];
  //   const importeRetenido = detalleCobranza.get('importeRetenido')?.value ;
  //   const importeCobrado = detalleCobranza.get('importeCobrado')?.value ;
  //   const total = importeCobrado + importeRetenido;

  //   this.detalleCobranza.get('importeTotal')?.patchValue(total);

  // }

  onSubmit() {
    Swal.fire({
      title: '¿Desea ingresar el cobro?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.cobranzaForm.valid) {
          console.log(this.cobranzaForm.value);
          const fechaLiquidacionValue = this.cobranzaForm.get('fechaLiquidacion')?.value;
          const fechaFormateada = formatDate(fechaLiquidacionValue, 'yyyy-MM-dd', 'en');          
          this.cobranzaForm.patchValue({ fechaLiquidacion: fechaFormateada });
          this.subscription.add(
            this.cobranzaApi.Crear(this.cobranzaForm.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Cobranza ingresada correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
              },
              error: () => {
                Swal.fire({
                  title: 'Error al ingresar la cobranza',
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


}
