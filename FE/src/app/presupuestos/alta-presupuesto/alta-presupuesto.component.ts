import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Producto } from 'src/interfaces/producto';
import { ClienteProvider } from 'src/providers/clienteProvider';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-alta-presupuesto',
  templateUrl: './alta-presupuesto.component.html',
  styleUrls: ['./alta-presupuesto.component.css']
})
export class AltaPresupuestoComponent implements OnInit, OnDestroy {

  listaClientes: any = [];
  formAgregarPresupuesto: FormGroup;
  productos: Producto[]
  private subscription = new Subscription();

  constructor(
    private clienteApi: ClienteProvider,
    private formBuilder: FormBuilder,
    private presupuestoApi: PresupuestoProvider
  ) {
    this.formAgregarPresupuesto = this.formBuilder.group({
      ClienteId: ['', Validators.required],
      campania: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.ObtenerClientes();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  guardar() {
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
        if (this.formAgregarPresupuesto.valid) {
          this.subscription.add(
            this.presupuestoApi.Crear(this.formAgregarPresupuesto.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Presupuesto ingresado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
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

  EditarProducto() { }
  ObtenerProducto() { }

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



}
