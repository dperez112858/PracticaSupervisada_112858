import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Localidad } from 'src/interfaces/localidad';
import { Proveedor } from 'src/interfaces/proveedor';
import { Provincia } from 'src/interfaces/provincia';
import { LocalidadProvider } from 'src/providers/localidadProvider';
import { ProveedorProvider } from 'src/providers/proveedorProvider';
import { ProvinciaProvider } from 'src/providers/provinciaProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-alta-proveedor',
  templateUrl: './alta-proveedor.component.html',
  styleUrls: ['./alta-proveedor.component.css']
})
export class AltaProveedorComponent implements OnInit, OnDestroy {

  subscription = new Subscription();

  proveedorCompleto = {} as Proveedor;
  listaLocalidades: Localidad[];
  listaProvincias: Provincia[];
  formAgregarProveedor: FormGroup;

  constructor(
    private router: Router,
    private proveedorApi: ProveedorProvider,
    private provinciaApi: ProvinciaProvider,
    private localidadApi: LocalidadProvider,
    private formBuilder: FormBuilder) {
      
      this.formAgregarProveedor = this.formBuilder.group({
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


  borrarFormulario() {
    this.formAgregarProveedor.reset();
  }

  guardar() {
    Swal.fire({
      title: '¿Desea ingresar el proveedor?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formAgregarProveedor.valid) {
          this.subscription.add(
            this.proveedorApi.Crear(this.formAgregarProveedor.value).subscribe({
              next: (res) => {

                console.log(res)
                Swal.fire({
                  title: 'Proveedor ingresado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
              },
              error: () => {
                Swal.fire({
                  title: 'Error al ingresar el proveedor',
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
    return this.formAgregarProveedor.controls['razonSocial'] as FormControl;
  }

  get controlCalle(): FormControl {
    return this.formAgregarProveedor.controls['calle'] as FormControl;
  }

  get controlNumero(): FormControl {
    return this.formAgregarProveedor.controls['numero'] as FormControl;
  }

  get controlProvincia(): FormControl {
    return this.formAgregarProveedor.controls['Provincia'] as FormControl;
  }

  get controlLocalidadId(): FormControl {
    return this.formAgregarProveedor.controls['LocalidadId'] as FormControl;
  }

  get controlCondicionIvaId(): FormControl {
    return this.formAgregarProveedor.controls['CondicionIvaId'] as FormControl;
  }

  get controlCuit(): FormControl {
    return this.formAgregarProveedor.controls['cuit'] as FormControl;
  }
}
