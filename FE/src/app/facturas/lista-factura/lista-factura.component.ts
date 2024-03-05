import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { Subscription } from 'rxjs';
import { Factura } from 'src/interfaces/factura';
import { FacturaProvider } from 'src/providers/facturaProvider';
import Swal from 'sweetalert2';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ClienteProvider } from 'src/providers/clienteProvider';

@Component({
  selector: 'app-lista-factura',
  templateUrl: './lista-factura.component.html',
  styleUrls: ['./lista-factura.component.css']
})
export class ListaFacturaComponent {
  sumaFactras: number;
  dia: any;
  diaHora: any;
  fecha: number;
  visible: boolean = true;
  listaFacturas: any = [];
  listaClientes: any = [];
  filtroPorCliente: string = '';
  factura = {} as Factura;
  public page: number;
  formEditarComprobante: FormGroup;
  private subscription = new Subscription();

  constructor(
    private router: Router,
    private facturaApi: FacturaProvider,
    public datepipe: DatePipe,
    private formBuilder: FormBuilder,
    private clienteApi: ClienteProvider) {
    this.formEditarComprobante = this.formBuilder.group({
      id: [''],
      fecha: ['', Validators.required],
      numero: ['', Validators.required],
      tipoComprobante: [, Validators.required],
      netoGravado: [, Validators.required],
      iva: [0],
      dolar: [false],
      tipoCambio: [1],
      total: [0],
      ClienteId: ['', Validators.required],
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  ngOnInit(): void {
    this.ObtenerTotalFacturas();
    this.ObtenerClientes();
    this.ObtenerTodas();
    this.ObtenerFecha();
    this.visible = false;

    console.log(this.listaFacturas);
  }



  ObtenerFecha() {
    this.fecha = Date.now();
    this.dia = this.datepipe.transform(this.fecha, 'dd/MM/yyyy');
    this.diaHora = this.datepipe.transform(this.fecha, 'yyyy-MM-dd-hhmmss');
  }

  ObtenerTodas() {
    this.subscription.add(
      this.facturaApi.ObtenerTodas().subscribe((data) => {
        if (data.ok) {
          this.listaFacturas = data.listaFacturas;
        }
        else {
          Swal.fire({
            title: 'Error al ingresar el cliente' + (data),
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  eliminarFactura(Id: string) {
    Swal.fire({
      title: '¿Desea eliminar la factura?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.facturaApi.Eliminar(Id).subscribe(() => {
          if (Id) {
            Swal.fire({
              title: 'Factura eliminada correctamente',
              icon: 'success',
              confirmButtonText: "Ok",
            });
            this.ObtenerTodas();
          } else {
            Swal.fire({
              title: 'Error al eliminar la factura',
              icon: 'error',
              confirmButtonText: "Ok",
            });
          }
        })
      }
    })
  }

  editarFactura(id: any) {
    this.facturaApi.ObtenerPorId(id).subscribe((data) => {
      if (data) {
        //debugger
        (this.factura = data)
        console.log(this.factura);

        const fechapago = new Date(this.factura.fecha);
        const dia = fechapago.getDate();
        const mes = fechapago.getMonth() + 1;
        const año = fechapago.getFullYear();
        const fechaFormateada = `${año}-${mes < 10 ? '0' + mes : mes}-${dia < 10 ? '0' + dia : dia}`;

        // this.formEditarCliente.patchValue(this.cliente as Cliente);
        this.formEditarComprobante.patchValue({
          id: this.factura.id,
          fecha: fechaFormateada,
          numero: this.factura.numero,
          tipoComprobante: this.factura.tipoComprobante,
          netoGravado: this.factura.netoGravado,
          iva: this.factura.iva,
          moneda: this.factura.moneda,
          tipoCambio: this.factura.tipoCambio,
          total: this.factura.total,
          saldo: this.factura.saldo,
          ClienteId: this.factura.cliente.id//,
          //CondicionIvaId: this.factura.condicionIva.id*/
        })


      } else {
        alert("Error al editar la factura");
      }
    })
  }

  guardar() {
    Swal.fire({
      title: '¿Desea modificar el comprobante?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.formEditarComprobante.valid) {
          this.subscription.add(
            this.facturaApi.Editar(this.formEditarComprobante.value).subscribe({
              next: (res) => {
                console.log(res)
                Swal.fire({
                  title: 'Comprobante modificado correctamente',
                  icon: 'success',
                  confirmButtonText: "Ok",
                });
                this.ObtenerTodas();
              },
              error: () => {
                Swal.fire({
                  title: 'Error al modficar el comprobante',
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

  pdf() {
    var data = document.getElementById('facturacion'); //id="facturacion"
    if (data !== null) {
      html2canvas(data).then(canvas => {
        // Few necessary setting options  
        let imgWidth = 208;
        let imgHeight = canvas.height * imgWidth / canvas.width;

        const contentDataURL = canvas.toDataURL('image/png')
        let pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF  
        let position = 0;
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
        pdf.save('ListadoFacturas_' + this.diaHora + '.pdf'); // Nombre de la salida PDF
      });
    }
  }

  borrarFormulario() {
    this.formEditarComprobante.reset();
  }

  ObtenerTotalFacturas() {
    this.subscription.add(
      this.facturaApi.ObtenerTodas().subscribe((data) => {
        if (data.ok) {
          this.listaFacturas = data.listaFacturas;
          this.sumaFactras = this.listaFacturas.reduce((acumulador: number, actual: { total: number; }) => acumulador + actual.total, 0);
          console.log("TOTAL Facturas: " + this.sumaFactras);
        }
        else {
          alert("Error al obtener el total de las facturas");
        }
      })
    )
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
            title: 'Error al obtener clientes' + (data.error), //REVISAR
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  get controlfecha(): FormControl {
    return this.formEditarComprobante.controls['fecha'] as FormControl;
  }
  get controlnumero(): FormControl {
    return this.formEditarComprobante.controls['numero'] as FormControl;
  }
  get controltipoComprobante(): FormControl {
    return this.formEditarComprobante.controls['tipoComprobante'] as FormControl;
  }
  get controlnetoGravado(): FormControl {
    return this.formEditarComprobante.controls['netoGravado'] as FormControl;
  }
  get controliva(): FormControl {
    return this.formEditarComprobante.controls['iva'] as FormControl;
  }
  get controldolar(): FormControl {
    return this.formEditarComprobante.controls['dolar'] as FormControl;
  }
  get controltipoCambio(): FormControl {
    return this.formEditarComprobante.controls['tipoCambio'] as FormControl;
  }
  get controltotal(): FormControl {
    return this.formEditarComprobante.controls['total'] as FormControl;
  }
  get controlClienteId(): FormControl {
    return this.formEditarComprobante.controls['ClienteId'] as FormControl;
  }
}
