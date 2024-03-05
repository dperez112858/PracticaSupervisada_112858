import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CobranzaProvider } from 'src/providers/cobranzaProvider';
import { ClienteProvider } from 'src/providers/clienteProvider';
import { Cliente } from 'src/interfaces/cliente';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lista-cobranza',
  templateUrl: './lista-cobranza.component.html',
  styleUrls: ['./lista-cobranza.component.css']
})
export class ListaCobranzaComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  listaCobranzas: any = [];
  ClienteId: any;  
  filtroInsumo: string = '';
  formEditarCobranza: FormGroup;
  detalleCobranza: FormArray;
  public page: number;
  listaCliente: any = [];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private clienteApi: ClienteProvider,
    private cobranzaApi: CobranzaProvider) { }


  ngOnInit(): void {
    this.ObtenerCobranzas();
    this.ObtenerClientes();
    this.formEditarCobranza = this.formBuilder.group({
      id: [''], 
      clienteId: [''], 
      total: [''], 
      fechaLiquidacion: [''],
      detalleCobranza: this.formBuilder.array([])
    });
  
    // Inicializa el FormArray detalleCobranza
    this.detalleCobranza = this.formEditarCobranza.get('detalleCobranza') as FormArray;
  }
    

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  ObtenerCobranzas() {
    this.subscription.add(
      this.cobranzaApi.ObtenerTodas().subscribe((data) => {
        if (data.ok) {
          this.listaCobranzas = data.listaCobranzas;
        }
        else {
          Swal.fire({
            title: 'Error al ingresar las cobranzas' + (data.error),
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  EditarCobranza(id: string) {
    this.subscription.add(
      this.cobranzaApi.ObtenerPorId(id).subscribe((data) => {
        if (data) {
          const cobranza = data;
          const fechaliquidacion = new Date(cobranza.fechaLiquidacion);
          const dia = fechaliquidacion.getDate();
          const mes = fechaliquidacion.getMonth() + 1;
          const año = fechaliquidacion.getFullYear();
          const fechaFormateada = `${año}-${mes < 10 ? '0' + mes : mes}-${dia < 10 ? '0' + dia : dia}`;
          this.formEditarCobranza.patchValue({
            id: cobranza.id,
            clienteId: cobranza.cliente.id,
            total: cobranza.total,
            fechaLiquidacion: fechaFormateada
          });
          if (cobranza.detalleCobranza) {
            while (this.detalleCobranza.length !== 0) {
              this.detalleCobranza.removeAt(0);
            }
            
            cobranza.detalleCobranza.forEach(detalle => {
              const fechapago = new Date(detalle.fechaPago);
              const dia = fechapago.getDate();
              const mes = fechapago.getMonth() + 1;
              const año = fechapago.getFullYear();
              const fechaFormateada = `${año}-${mes < 10 ? '0' + mes : mes}-${dia < 10 ? '0' + dia : dia}`;
              const detalleCobranza = this.formBuilder.group({
                id: [detalle.id, Validators.required],
                cobranzaId: [detalle.cobranzaId, Validators.required],
                importeCobrado: [detalle.importeCobrado, Validators.required],
                importeRetenido: [detalle.importeRetenido, Validators.required],
                fechaPago: [fechaFormateada, Validators.required],
                tipoPagoId: [detalle.tipoPagoId, Validators.required],
                facturaId: [detalle.facturaId, Validators.required],
                importeTotal: [detalle.importeTotal]
              });

              this.detalleCobranza.push(detalleCobranza);
            });
          }
        } else {
          Swal.fire({
            title: 'Error al obtener la cobranza',
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    );
  }
  
  eliminarCobranza(Id: string) {
    Swal.fire({
      title: '¿Desea eliminar la cobranza?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.cobranzaApi.Eliminar(Id).subscribe(() => {
          if (Id) {
            Swal.fire({
              title: 'Cobranza eliminada correctamente',
              icon: 'success',
              confirmButtonText: "Ok",
            });
            this.ObtenerCobranzas();
          } else {
            Swal.fire({
              title: 'Error al emiminar la cobranza',
              icon: 'error',
              confirmButtonText: "Ok",
            });
          }
        })
      }
    })
  }

  guardar() {
    Swal.fire({
      title: '¿Desea modificar la cobranza?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formEditarCobranza.valid) {
          this.subscription.add(
            this.cobranzaApi.Editar(this.formEditarCobranza.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Cobranza modificada correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
                this.ObtenerCobranzas();
              },
              error: () => {
                Swal.fire({
                  title: 'Error al modificar la cobranza',
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


  ObtenerClientes() {
    this.clienteApi.ObtenerTodos().subscribe((data) => {
      if (data) {
        this.listaCliente = data.listaClientes;
      } else {
        alert(data);
      }
    })
  }

}
