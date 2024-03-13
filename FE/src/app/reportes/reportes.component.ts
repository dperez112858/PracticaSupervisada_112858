import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { Subscription } from 'rxjs';
import { Intervalo } from 'src/interfaces/intervalo';
import { IntervaloPresupuesto } from 'src/interfaces/intervaloPresupuesto';
import { CobranzaProvider } from 'src/providers/cobranzaProvider';
import { FacturaProvider } from 'src/providers/facturaProvider';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import { ClienteProvider } from 'src/providers/clienteProvider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent implements OnInit {

  sumaFactras: number;
  dia: any;
  diaHora: any;
  fecha: number;
  visible: boolean = true;
  listaFacturas: any = [];
  listadoFactMes: any = [];
  listadoCobranzasMes: any = [];
  listadoPedidosMes: any = [];
  listaClientes: any = [];
  listaPresupuestos: any = [];
  filtroPorCliente: string = '';
  public page: number;
  facturaForm: FormGroup;
  cobranzaForm: FormGroup;
  presupuestoForm: FormGroup;
  intervalo: Intervalo;
  intervaloPresupuesto: IntervaloPresupuesto;
  mFacturacion: boolean = false;
  mCobranzas: boolean = false;
  mPedidos: boolean = false;
  mCuentaCorriente: boolean = false;
  totalFacturas: number;
  totalCuentaCorriente: number = 0;


  private subscription = new Subscription();

  constructor(
    private router: Router,
    private facturaApi: FacturaProvider,
    private cobranzaApi: CobranzaProvider,
    private presupuestoApi: PresupuestoProvider,
    private clienteApi: ClienteProvider,
    public datepipe: DatePipe,
    public formBuilder: FormBuilder,
  ) {
    this.facturaForm = this.formBuilder.group({
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required],
      cancelada: [false]
    });

    this.cobranzaForm = this.formBuilder.group({
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required]
    });

    this.presupuestoForm = this.formBuilder.group({
      clienteId: [''],
      fechaInicio: [new Date(), Validators.required],
      fechaFin: [new Date(), Validators.required]
    });
    this.intervaloPresupuesto = { fechaInicio: new Date(), fechaFin: new Date(), clienteId: '' };
    this.intervalo = { fechaInicio: new Date(), fechaFin: new Date() };
  }

  ngOnInit(): void {
    this.ObtenerFecha();
    this.limpiarMenu();
    this.ObtenerClientes();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ObtenerFecha() {
    this.fecha = Date.now();
    this.dia = this.datepipe.transform(this.fecha, 'dd/MM/yyyy');
    this.diaHora = this.datepipe.transform(this.fecha, 'yyyy-MM-dd-hhmmss');
  }

  limpiarMenu() {
    this.mFacturacion = false;
    this.mCobranzas = false;
    this.mCuentaCorriente = false;
    this.mPedidos = false;
  }

  menuFacturacion() {
    this.limpiarMenu();
    this.mFacturacion = true;
  }

  menuPedidos() {
    this.limpiarMenu();
    this.mPedidos = true;
  }

  menuCobranzas() {
    this.limpiarMenu();
    this.mCobranzas = true;
  }

  menuCuentaCorriente() {
    this.limpiarMenu();
    this.mCuentaCorriente = true;
  }

  ObtenerClientes() {
    this.subscription.add(
      this.clienteApi.ObtenerTodos().subscribe((data) => {
        if (data.ok) {
          this.listaClientes = data.listaClientes;
          this.calcularTotalSaldo();
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

  calcularTotalSaldo() {
    for (let i = 0; i < this.listaClientes.length; i++) {
      this.totalCuentaCorriente += this.listaClientes[i].saldo;
      console.log(this.totalCuentaCorriente);
    }
  }

  ObtenerPedidosPorMes() {
    this.intervalo = this.facturaForm.value;
    this.subscription.add(
      this.facturaApi.ObtenerPedidosPorMes(this.intervalo).subscribe((data) => {
        if (data) {
          console.log(data);
          this.listadoFactMes = data;
        }
        else {
          Swal.fire({
            title: 'Error al obtener las facturas' + (data),
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  ObtenerCobranzasPorMes() {
    this.intervalo = this.cobranzaForm.value;
    this.subscription.add(
      this.cobranzaApi.ObtenerCobranzasPorMes(this.intervalo).subscribe((data) => {
        if (data) {
          console.log(data);
          this.listadoCobranzasMes = data;
        }
        else {
          Swal.fire({
            title: 'Error al obtener las cobranzas' + (data),
            icon: 'error',
            confirmButtonText: "Ok",
          });
        }
      })
    )
  }

  ObtenerPresupuestos() {
    this.intervaloPresupuesto = this.presupuestoForm.value;
    this.subscription.add(
      this.presupuestoApi.ObtenerPresupuestosPorMes(this.intervaloPresupuesto).subscribe((data) => {
        console.log(data);
        if (data) {
          this.listaPresupuestos = data;
        } else {
          alert(data);
        }
      }))
  }

  pdf() {
    if (this.mPedidos) {
      var data = document.getElementById('presupuestos');
    } else if (this.mCobranzas) {
      var data = document.getElementById('cobranzas');
    } else if (this.mFacturacion) {
      var data = document.getElementById('facturacion');
    } else {
      var data = document.getElementById('cuentaCorriente');
    }
    if (data !== null) {
      html2canvas(data).then(canvas => {
        // Few necessary setting options  
        let imgWidth = 208;
        let imgHeight = canvas.height * imgWidth / canvas.width;

        const contentDataURL = canvas.toDataURL('image/png')
        let pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF  
        let position = 0;
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
        if (this.mPedidos) {
          pdf.save('ListadoPresupuestos_' + this.diaHora + '.pdf'); // Nombre de la salida PDF
        }
        else if (this.mCobranzas) {
          pdf.save('ListadoCobranzas_' + this.diaHora + '.pdf'); // Nombre de la salida PDF
        }
        else if (this.mCuentaCorriente) {
          pdf.save('ListadoCuentaCorrienteClientes_' + this.diaHora + '.pdf'); // Nombre de la salida PDF
        }
        else {
          pdf.save('ListadoFacturas_' + this.diaHora + '.pdf'); // Nombre de la salida PDF
        }

      });
    }
  }

}
