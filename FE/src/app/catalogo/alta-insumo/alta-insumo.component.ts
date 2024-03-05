import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { InsumoProvider } from 'src/providers/insumoProvider';
import Swal from 'sweetalert2';
import { ProveedorProvider } from 'src/providers/proveedorProvider';

@Component({
  selector: 'app-alta-insumo',
  templateUrl: './alta-insumo.component.html',
  styleUrls: ['./alta-insumo.component.css']
})
export class AltaInsumoComponent implements OnInit, OnDestroy{

  subscription = new Subscription();
  formAgregarInsumo: FormGroup;
  listaProveedores: any = [];
  
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private insumoApi: InsumoProvider,
    private proveedorApi: ProveedorProvider) { }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.ObtenerProveedores();
    
    this.formAgregarInsumo = this.formBuilder.group({
      descripcion: ['', Validators.required],
      precio: ['', Validators.required],
      fechaCompra: ['', Validators.required],
      ProveedorId: ['', Validators.required]
    })
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
      title: '¿Desea ingresar el insumo?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formAgregarInsumo.valid) {
          this.subscription.add(
            this.insumoApi.Crear(this.formAgregarInsumo.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Insumo ingresado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
              },
              error: () => {
                Swal.fire({
                  title: 'Error al ingresar el insumo',
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
    return this.formAgregarInsumo.controls['descripcion'] as FormControl;
  }

  get controlPrecio(): FormControl {
    return this.formAgregarInsumo.controls['precio'] as FormControl;
  }

  get controlFechaCompra(): FormControl {
    return this.formAgregarInsumo.controls['fechaCompra'] as FormControl;
  }

  get controlProveedorId(): FormControl {
    return this.formAgregarInsumo.controls['ProveedorId'] as FormControl;
  }
}
