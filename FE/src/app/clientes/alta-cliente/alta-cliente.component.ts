import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteProvider } from '../../../providers/clienteProvider'
import { Cliente } from 'src/interfaces/cliente';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { Localidad } from 'src/interfaces/localidad';
import { Provincia } from 'src/interfaces/provincia';
import { ProvinciaProvider } from 'src/providers/provinciaProvider';
import { LocalidadProvider } from 'src/providers/localidadProvider';
import { CondicionIvaProvider } from 'src/providers/condicionIvaProvider';
import { CondicionIva } from 'src/interfaces/condicionIva';

@Component({
  selector: 'app-alta-cliente',
  templateUrl: './alta-cliente.component.html',
  styleUrls: ['./alta-cliente.component.css']
})
export class AltaClienteComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();

  clienteCompleto = {} as Cliente;
  listaLocalidades: Localidad[];
  listaProvincias: Provincia[];
  listaCondicionIva: CondicionIva[];
  formAgregarCliente: FormGroup;


  constructor(
    private router: Router,
    private clienteApi: ClienteProvider,
    private provinciaApi: ProvinciaProvider,
    private localidadApi: LocalidadProvider,
    private condicionIvaApi: CondicionIvaProvider,
    private formBuilder: FormBuilder) {
      
      this.formAgregarCliente = this.formBuilder.group({
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
        CondicionIvaId:['']
      })
    }

  ngOnInit(): void {
    this.obtenerProvincias();
    this.ObtenerCondicionIva();
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

  ObtenerCondicionIva(){
    this.condicionIvaApi.ObtenerCondicionIva().subscribe((data) => {
      if (data) {
        this.listaCondicionIva = data;
        console.log(this.listaCondicionIva);
      } else {
        alert(data);
      }
    })
  }

  borrarFormulario() {
    this.formAgregarCliente.reset();
  }

  guardar() {
    Swal.fire({
      title: '¿Desea ingresar el cliente?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formAgregarCliente.valid) {
          this.subscription.add(
            this.clienteApi.Crear(this.formAgregarCliente.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Cliente ingresado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
              },
              error: () => {
                Swal.fire({
                  title: 'Error al ingresar el cliente',
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

  get controlRazonSocial(): FormControl {
    return this.formAgregarCliente.controls['razonSocial'] as FormControl;
  }

  get controlCalle(): FormControl {
    return this.formAgregarCliente.controls['calle'] as FormControl;
  }

  get controlNumero(): FormControl {
    return this.formAgregarCliente.controls['numero'] as FormControl;
  }

  get controlProvincia(): FormControl {
    return this.formAgregarCliente.controls['Provincia'] as FormControl;
  }

  get controlLocalidadId(): FormControl {
    return this.formAgregarCliente.controls['LocalidadId'] as FormControl;
  }

  get controlCondicionIvaId(): FormControl {
    return this.formAgregarCliente.controls['CondicionIvaId'] as FormControl;
  }

  get controlCuit(): FormControl {
    return this.formAgregarCliente.controls['cuit'] as FormControl;
  }

}
