import { Router } from '@angular/router';
import { ClienteProvider } from '../../../providers/clienteProvider'
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProvinciaProvider } from 'src/providers/provinciaProvider';
import { Localidad } from 'src/interfaces/localidad';
import { Provincia } from 'src/interfaces/provincia';
import { LocalidadProvider } from 'src/providers/localidadProvider';
import { Cliente } from 'src/interfaces/cliente';
import { CondicionIva } from 'src/interfaces/condicionIva';
import { CondicionIvaProvider } from 'src/providers/condicionIvaProvider';


@Component({
  selector: 'app-lista-cliente',
  templateUrl: './lista-cliente.component.html',
  styleUrls: ['./lista-cliente.component.css']
})
export class ListaClienteComponent implements OnInit, OnDestroy {

  listaClientes: any = [];
  LocalidadId: any;
  CondicionIvaId: any;
  ClienteId: any;
  listaLocalidades: Localidad[];
  listaProvincias: Provincia[];
  listaCondicionIva: CondicionIva[];
  filtroPorRazonSocial: string = '';
  formEditarCliente: FormGroup;
  cliente = {} as Cliente;
  public page: number;
  private subscription = new Subscription();

  constructor(
    private router: Router,
    private clienteApi: ClienteProvider,
    private formBuilder: FormBuilder,
    private provinciaApi: ProvinciaProvider,
    private localidadApi: LocalidadProvider,
    private condicionIvaApi: CondicionIvaProvider
  ) {
    this.formEditarCliente = this.formBuilder.group({
      id: [''],
      razonSocial: ['', Validators.required],
      cuit: ['', Validators.required],
      calle: ['', Validators.required],
      numero: ['', Validators.required],
      codigoPostal: [''],
      telefono: [''],
      nombreContacto: [''],
      email: ['', Validators.email],
      comentario: [''],
      LocalidadId: [''],
      CondicionIvaId: ['']
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.ObtenerClientes();
    this.obtenerProvincias();
    this.ObtenerCondicionIva();
  }

  borrarFormulario() {
    this.formEditarCliente.reset();
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

  obtenerProvincias() {
    this.provinciaApi.ObtenerTodas().subscribe((data) => {
      if (data) {
        this.listaProvincias = data;
        console.log(this.listaProvincias);
      } else {
        alert(data);
      }
    })
  }

  obtenerLocalidad(id: any) {
    this.localidadApi.ObtenerPorId(id).subscribe((data) => {
      if (data.ok) {
        this.listaLocalidades = data.listaLocalidades;
      } else {
        alert(data.error);
      }
    })
  }

  ObtenerCondicionIva() {
    this.condicionIvaApi.ObtenerCondicionIva().subscribe((data) => {
      if (data) {
        this.listaCondicionIva = data;
        console.log(this.listaCondicionIva);
      } else {
        alert(data);
      }
    })
  }

  editarCliente(id: any) {
    this.clienteApi.obtenerCliente(id).subscribe((data) => {
      if (data) {
        //debugger
        (this.cliente = data)
        console.log(this.cliente);
        // this.formEditarCliente.patchValue(this.cliente as Cliente);
        this.formEditarCliente.patchValue({
          razonSocial: this.cliente.razonSocial,
          id: this.cliente.id,
          cuit: this.cliente.cuit,
          calle: this.cliente.calle,
          numero: this.cliente.numero,
          codigoPostal: this.cliente.codigoPostal,
          telefono: this.cliente.telefono,
          nombreContacto: this.cliente.nombreContacto,
          email: this.cliente.email,
          comentario: this.cliente.comentario,
          LocalidadId: this.cliente.localidad.id,
          CondicionIvaId: this.cliente.condicionIva.id
        })


      } else {
        alert("Error al editar el cliente");
      }
    })
  }

  // guardarCliente(){
  //   if(this.formEditarCliente.valid){
  //     this.clienteApi.Editar(this.formEditarCliente.value as Cliente).subscribe({
  //       next: () =>{
  //         alert("cliente modificado desde FE");
  //       },
  //       error: () =>{
  //         alert("Error al editar el cliente desde FE");
  //       }
  //     })
  //   }
  // }   

  eliminarCliente(Id: string) {
    Swal.fire({
      title: '¿Desea eliminar el cliente?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.clienteApi.Eliminar(Id).subscribe(() => {
          if (Id) {
            Swal.fire({
              title: 'Cliente eliminado correctamente',
              icon: 'success',
              confirmButtonText: "Ok",
            });
            this.ObtenerClientes();
          } else {
            Swal.fire({
              title: 'Error al eliminar el cliente',
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
      title: '¿Desea modificar el cliente?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {

        if (this.formEditarCliente.valid) {
          console.log(this.formEditarCliente.value);
          this.subscription.add(
            this.clienteApi.Editar(this.formEditarCliente.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Cliente modificado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
                this.ObtenerClientes();
              },
              error: () => {
                Swal.fire({
                  title: 'Error al modificar el cliente',
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
