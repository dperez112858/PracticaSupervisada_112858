import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { FacturaProvider } from 'src/providers/facturaProvider';
import { MenuProvider } from 'src/providers/menuProvider';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import { ProductoProvider } from 'src/providers/productoProvider';
import Swal from 'sweetalert2';
import * as Highcharts from 'highcharts';
import { Grafico } from 'src/interfaces/grafico';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit, OnDestroy {

  graficoFacturacionAnualAnterior: typeof Highcharts = Highcharts;
  graficoFacturacionAnualActual: typeof Highcharts = Highcharts;
  graficoPedidosActual: typeof Highcharts = Highcharts;
  graficoPedidossAnterior: typeof Highcharts = Highcharts;

  sumaPresupuestos: number = 1;
  sumaProductos: number = 3;
  data: any;
  dataPeriodo: any[] = [];
  dataMonto: any[] = [];


  private subscripction = new Subscription();

  cotizaciones: any = [];
  listaFacturas: any = [];
  isInicio: boolean = false;
  isClientes: boolean = false;
  isPresupuestos: boolean = true;
  isReportes: boolean = false;
  isCobranzas: boolean = false;
  isFacturas: boolean = false;
  isProveedores: boolean = false;
  isCatalogo: boolean = false;
  isAcercaDe: boolean = false;
  isFaq: boolean = false;
  dia: number;
  mes: string;
  hora: number;
  min: string;
  seg: number;
  sumaFactras: number;
  dataSumaPresu: number[] = [];
  sumaPresupuestosAceptados: number;
  listaFiltrada: any;
  maxYAxisValue: number = 14;
  valoresGrafico: Grafico[] = [];




  constructor(
    private router: Router,
    private facturaApi: FacturaProvider,
    private productoApi: ProductoProvider,
    private presupuestoApi: PresupuestoProvider,
    private menuApi: MenuProvider) {

  }

  ngOnDestroy(): void {
    this.subscripction.unsubscribe();
  }
  ngOnInit(): void {
    this.ObtenerTotalFacturas();
    this.ObtenerCantidadProductos();
    this.ObtenerCantidadPresupuestos();
    this.ObtenerCantidadPresupuestosAceptados();
    this.limpiarBanderas();
    this.isInicio = true;
    this.contarFacturasPorCliente();
    this.ObtenerFacturasAnioAnterior();
    this.ObtenerFacturasAnioActual();

    this.obtenerHora();
    setInterval(() => {
      this.obtenerHora();
    }, 1000); // Actualizar cada segundo (1000 ms)

  }
  // ObtenerFacturasAnioActual() {
  //   this.subscripction.add(
  //     this.facturaApi.ObtenerTotalAnioActual().subscribe((rta) => {
  //       this.valoresGrafico = rta;
  //       console.log(this.valoresGrafico);
  //       if (this.valoresGrafico != null) {
  //         for (let i = 0; i < this.valoresGrafico.length; i++) {
  //           this.dataMonto.push(this.valoresGrafico[i].montoFacturadoMensual);
  //           this.dataPeriodo.push(this.valoresGrafico[i].mes);
  //         }
  //         console.log("dataPeriodo" + this.dataPeriodo);
  //         console.log("dataMonto" + this.dataMonto);
  //       }
  //       else {
  //         alert("Error al obtener los totales de la facturación mensual para el gráfico");
  //       }
  //     })
  //   )
  // }
  ObtenerFacturasAnioActual() {
    this.subscripction.add(
      this.facturaApi.ObtenerTotalAnioActual().subscribe((rta) => {
        if (rta.listadoFactuasUltimoAnio != null) {
          this.valoresGrafico = rta.listadoFactuasUltimoAnio;
          console.log(this.valoresGrafico);
          for (let i = 0; i < this.valoresGrafico.length; i++) {
            this.dataMonto.push(this.valoresGrafico[i].montoFacturadoMensual);
            this.dataPeriodo.push(this.valoresGrafico[i].mes);
          }
          console.log("dataPeriodo", this.dataPeriodo);
          console.log("dataMonto", this.dataMonto);
        } else {
          alert("Error al obtener los totales de la facturación mensual para el gráfico");
        }
      })
    )
  }


  chartOptionsFacturacionAnualAnterior: Highcharts.Options = {
    title: {
      text: "Facturación"
    },
    subtitle: {
      text: "Año 2023"
    },
    xAxis: {
      categories: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"]
    },
    yAxis: {
      title: {
        text: ""
      }
    },
    tooltip: {
      valueSuffix: " $"
    },
    series: [
      {
        name: 'Facturación año anterior ($)',
        type: 'line',
        color: '#C0635C',
        data: [87720.16, 184723.44, 1008849.60, 394502.35, 523131.40, 1446095.20, 917083.20, 711480, 1963781.60, 1149906.56, 0, 235774.79],
      }
    ],
  };
  chartOptionsFacturacionAnualActual: Highcharts.Options = {
    title: {
      text: "Facturación"
    },
    subtitle: {
      text: "Año 2024"
    },
    xAxis: {
      categories: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"]
    },
    yAxis: {
      title: {
        text: ""
      }
    },
    tooltip: {
      valueSuffix: " $"
    },
    series: [
      {
        name: 'Facturación año anterior ($)',
        type: 'line',
        color: '#40856E',
        data: [0, 409628.32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      }
    ],
  };


  chartOptionsPedidosActual: Highcharts.Options = {
    title: {
      text: "Pedidos"
    },
    subtitle: {
      text: "Año 2024"
    },
    xAxis: {
      categories: ['Entregados', 'Pendientes']
      
    },
    yAxis:{
      max: this.maxYAxisValue,
      title: {
        text: ""
      },
    },
    plotOptions: {
      series: {
        stacking: 'normal'
      }
    },
    series: [
      {
        type: "bar",
        name: 'Pedidos',
        data: [
          {
            name: 'Entregados',
            y: 2,
            color: '#40856E' // Color para la barra "Entregados"
          },
          {
            name: 'Pendientes',
            y: 0,
            color: '#C0635C' // Color para la barra "Pendientes"
          }
        ]
      }
    ],
  }

  chartOptionsPedidosAnterior: Highcharts.Options = {
    title: {
      text: "Pedidos"
    },
    subtitle: {
      text: "Año 2023"
    },
    xAxis: {
      categories: ['Entregados', 'Pendientes']
    },
    yAxis:{
      max: this.maxYAxisValue,
      title: {
        text: ""
      },
    },
    plotOptions: {
      series: {
        stacking: 'normal'
      }
    },
    series: [
      {
        type: "bar",
        name: 'Pedidos',
        data: [
          {
            name: 'Entregados',
            y: 12,
            color: '#40856E' // Color para la barra "Entregados"
          },
          {
            name: 'Pendientes',
            y: 3,
            color: '#C0635C' // Color para la barra "Pendientes"
          }
        ]
      }
    ],
  }
  mostrarGrafico() {
    // if (this.chartOptions.series) { // Verificar si this.chartOptions.series está definido
    //   const seriesData = this.chartOptions.series[0] as Highcharts.SeriesLineOptions; // Asegurar el tipo como SeriesLineOptions
    //   seriesData.data = this.dataMonto;
    //   Highcharts.chart('graficoFacturacion', this.chartOptions);
    // } else {
    //   console.error('La propiedad series en chartOptions no está definida.');
    // }
  }


  ObtenerFacturasAnioAnterior() {

  }

  obtenerHora() {
    const meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Deciembre"];
    const fechaActual = new Date();
    this.dia = fechaActual.getDate();
    this.mes = meses[fechaActual.getMonth()];
    this.hora = fechaActual.getHours();
    this.min = (fechaActual.getMinutes() < 10 ? '0' : '') + fechaActual.getMinutes();
  }

  limpiarBanderas() {
    this.isInicio = false;
    this.isClientes = false;
    this.isPresupuestos = false;
    this.isReportes = false;
    this.isCobranzas = false;
    this.isFacturas = false;
    this.isProveedores = false;
    this.isCatalogo = false;
    this.isAcercaDe = false;
    this.isFaq = false;
  }

  inicio() {
    this.limpiarBanderas();
    this.isInicio = true;
  }

  clientes() {
    this.limpiarBanderas();
    this.isClientes = true;
  }

  presupuestos() {
    this.limpiarBanderas();
    this.isPresupuestos = true;
  }

  reportes() {
    this.limpiarBanderas();
    this.isReportes = true;
  }

  cobranzas() {
    this.limpiarBanderas();
    this.isCobranzas = true;
  }

  facturas() {
    this.limpiarBanderas();
    this.isFacturas = true;
  }

  proveedores() {
    this.limpiarBanderas();
    this.isProveedores = true;
  }

  catalogo() {
    this.limpiarBanderas();
    this.isCatalogo = true;
  }

  acercaDe() {
    this.limpiarBanderas();
    this.isAcercaDe = true;
  }
  
  faq() {
    this.limpiarBanderas();
    this.isFaq = true;
  }
  ObtenerTotalFacturas() {
    this.subscripction.add(
      this.facturaApi.ObtenerTodas().subscribe((data) => {
        if (data.ok) {
          this.listaFacturas = data.listaFacturas;
          this.sumaFactras = this.listaFacturas.reduce((acumulador: number, actual: { total: number; }) => acumulador + actual.total, 0);
          //facturación anual
        }
        else {
          alert("Error al obtener el total de las facturas");
        }
      })
    )
  }

  contarFacturasPorCliente() {
    this.listaFiltrada = {};
    for (const item of this.listaFacturas) {
      const cliente = item.Cliente.razonSocial;
      const date = new Date(item.FechaCreacion);
      const month = date.getMonth() + 1;
      const year = date.getFullYear();
      if (!this.listaFiltrada[year][month]) {
        this.listaFiltrada[year][month] = [];
      }
      this.listaFiltrada[year][month].push(cliente);
    }
    console.log(this.listaFiltrada);
  }



  ObtenerCantidadProductos() {
    this.subscripction.add(
      this.productoApi.ObtenerCantidad().subscribe((data) => {
        if (data != null) {
          this.sumaProductos = data;
          //cantidad de productos
        }
        else {
          alert("Error al obtener el total de los productos");
        }
      })
    )
  }

  ObtenerCantidadPresupuestos() {
    this.subscripction.add(
      this.presupuestoApi.ObtenerCantidad().subscribe((data) => {
        if (data != null) {
          this.sumaPresupuestos = data;
          //cantidad de presupuestos generados
        }
        else {
          alert("Error al obtener el total de los presupuestos");
        }
      })
    )
  }

  ObtenerCantidadPresupuestosAceptados() {
    this.subscripction.add(
      this.presupuestoApi.ObtenerCantidadAceptados().subscribe((data) => {
        if (data != null) {
          this.sumaPresupuestosAceptados = data;
          //cantidad de presupuestos generados

        }
        else {
          alert("Error al obtener el total de los presupuestos");
        }
      })
    )
  }



}
