import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Insumo } from 'src/interfaces/insumo';
import { InsumoProvider } from 'src/providers/insumoProvider';
import { MenuProvider } from 'src/providers/menuProvider';
import { ProveedorProvider } from 'src/providers/proveedorProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lista-insumo',
  templateUrl: './lista-insumo.component.html',
  styleUrls: ['./lista-insumo.component.css']
})
export class ListaInsumoComponent implements OnInit, OnDestroy {

  listaInsumos: any = [];
  listaProveedores: any = [];
  filtroPorInsumo: string = '';
  insumo = {} as Insumo;
  formEditarInsumo: FormGroup;
  public page: number;
  private subscription = new Subscription();

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private insumoApi: InsumoProvider,
    private menuProvider: MenuProvider,
    private proveedorApi: ProveedorProvider
  ) {}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.ObtenerInsumos();
    this.ObtenerProveedores();
    this.formEditarInsumo = this.formBuilder.group({
      id: [''],
      descripcion: ['', Validators.required],
      precio: [, Validators.required],
      fechaCompra: [, Validators.required],
      ProveedorId: ['', Validators.required],
      detalleCobranza: this.formBuilder.array([])
    })
  }

  editarInsumo(id: string) {
    this.insumoApi.ObtenerPorId(id).subscribe((data) => {
      if (data) {
        (this.insumo = data)
        const fechaCompra = new Date(this.insumo.fechaCompra);
        const fechaFormateada = fechaCompra.toISOString().split('T')[0];

        this.formEditarInsumo.patchValue({
          id: this.insumo.id,
          descripcion: this.insumo.descripcion,
          precio: this.insumo.precio,
          fechaCompra: fechaFormateada,
          ProveedorId: this.insumo.proveedor.id
        });

        console.log(this.insumo);
      } else {
        alert("Error al obtener el insumo");
      }
    })
  }

  eliminarInsumo(Id: string) {
    Swal.fire({
      title: '¿Desea eliminar el insumo?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.insumoApi.Eliminar(Id).subscribe(() => {
          if (Id) {
            Swal.fire({
              title: 'Insumo eliminado correctamente',
              icon: 'success',
              confirmButtonText: "Ok",
            });
            this.ObtenerInsumos();
          } else {
            Swal.fire({
              title: 'Error al emiminar el insumo',
              icon: 'error',
              confirmButtonText: "Ok",
            });
          }
        })
      }
    })
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

  ObtenerProveedores() {
    this.proveedorApi.ObtenerTodos().subscribe((data) => {
      if (data.ok) {
        this.listaProveedores = data.listaProveedores;
      }
      else {
        alert(data.error);
      }
    })
  }

  guardar() {
    Swal.fire({
      title: '¿Desea modificar el insumo?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formEditarInsumo.valid) {
          this.subscription.add(
            this.insumoApi.Editar(this.formEditarInsumo.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Insumo modificado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
                this.ObtenerInsumos();
              },
              error: () => {
                Swal.fire({
                  title: 'Error al modificar el insumo',
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

  get controlDescripcion(): FormControl {
    return this.formEditarInsumo.controls['descripcion'] as FormControl;
  }

  get controlPrecio(): FormControl {
    return this.formEditarInsumo.controls['precio'] as FormControl;
  }

  get controlFechaCompra(): FormControl {
    return this.formEditarInsumo.controls['fechaCompra'] as FormControl;
  }

  get controlProveedorId(): FormControl {
    return this.formEditarInsumo.controls['ProveedorId'] as FormControl;
  }

}
