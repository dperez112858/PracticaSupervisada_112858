import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProvinciaProvider } from 'src/providers/provinciaProvider';
import { Localidad } from 'src/interfaces/localidad';
import { Provincia } from 'src/interfaces/provincia';
import { LocalidadProvider } from 'src/providers/localidadProvider';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Proveedor } from 'src/interfaces/proveedor';
import { MenuProvider } from 'src/providers/menuProvider';
import { ProveedorProvider } from 'src/providers/proveedorProvider';
import { CondicionIva } from 'src/interfaces/condicionIva';
import { CondicionIvaProvider } from 'src/providers/condicionIvaProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lista-proveedor',
  templateUrl: './lista-proveedor.component.html',
  styleUrls: ['./lista-proveedor.component.css']
})
export class ListaProveedorComponent implements OnInit, OnDestroy {

  listaProveedores: any = [];
  LocalidadId: any;
  CondicionIvaId: any;
  filtroPorRazonSocial: string = '';
  proveedor = {} as Proveedor;
  formEditarProveedor: FormGroup;
  public page: number;
  private subscripction = new Subscription();
  listaLocalidades: Localidad[];
  listaProvincias: Provincia[];
  listaCondicionIva: CondicionIva[];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private menuProvider: MenuProvider,
    private proveedorApi: ProveedorProvider,
    private provinciaApi: ProvinciaProvider,
    private localidadApi: LocalidadProvider,
    private condicionIvaApi: CondicionIvaProvider,
  ) { }

  ngOnDestroy(): void {
    this.subscripction.unsubscribe();
  }

  ngOnInit(): void {
    this.ObtenerProveedores();
    this.obtenerProvincias();
    this.ObtenerCondicionIva();
    this.formEditarProveedor = this.formBuilder.group({
      id: [''], 
      razonSocial: [''], 
      cuit: [''], 
      calle: [''], 
      numero: [''], 
      codigoPostal: [''], 
      telefono: [''], 
      nombreContacto: [''], 
      email: [''], 
      comentario: [''], 
      condicionIvaId: [''], 
      localidadId: [''],

    })
  }

  ObtenerProveedores() {
    this.subscripction.add(
      //servicio inyectado.método(parámetros en caso de ser necesario).subscribe(resultado)
      this.proveedorApi.ObtenerTodos().subscribe((data) => {
        if (data.ok) {
          this.listaProveedores = data.listaProveedores;
        }
        else {
          Swal.fire({
            title: 'Error al ingresar el proveedor' + (data.error), //REVISAR
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  EditarProveedor(id: string) {
    this.proveedorApi.ObtenerPorId(id).subscribe((data) => {
      if (data) {
        (this.proveedor = data)
        
         this.formEditarProveedor.patchValue({
          id: this.proveedor.id,
          razonSocial: this.proveedor.razonSocial,
          cuit: this.proveedor.cuit,
          calle: this.proveedor.calle,
          numero: this.proveedor.numero,
          codigoPostal: this.proveedor.codigoPostal,
          telefono: this.proveedor.telefono,
          nombreContacto: this.proveedor.nombreContacto,
          email: this.proveedor.email,
          comentario: this.proveedor.comentario,
          condicionIvaId: this.proveedor.condicionIva.id,
          localidadId: this.proveedor.localidad.id 
         })
        console.log(this.proveedor);
      } else {
        alert("Error al obtener el insumo");
      }
    })
  }


  guardar() {
    Swal.fire({
      title: '¿Desea modificar el proveedor?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formEditarProveedor.valid) {
          console.log(this.formEditarProveedor.value);
          this.subscripction.add(
            this.proveedorApi.Editar(this.formEditarProveedor.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Proveedor modificado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
                this.ObtenerProveedores();
              },
              error: () => {
                Swal.fire({
                  title: 'Error al modificar el proveedor',
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

  eliminarProveedor(Id: string) {
    Swal.fire({
      title: '¿Desea eliminar el proveedor?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.proveedorApi.Eliminar(Id).subscribe(() => {
          if (Id) {
            Swal.fire({
              title: 'Proveedor eliminado correctamente',
              icon: 'success',
              confirmButtonText: "Ok",
            });
            this.ObtenerProveedores();
          } else {
            Swal.fire({
              title: 'Error al eliminar el proveedor',
              icon: 'error',
              confirmButtonText: "Ok",
            });
          }
        })
      }
    })
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
}
