import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AltaClienteComponent } from './clientes/alta-cliente/alta-cliente.component';
import { ListaClienteComponent } from './clientes//lista-cliente/lista-cliente.component';
import { AltaProveedorComponent } from './proveedores/alta-proveedor/alta-proveedor.component';
import { PrincipalComponent } from './inicio/principal.component';
import { ClienteProvider } from '../providers/clienteProvider';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ListaProveedorComponent } from './proveedores/lista-proveedor/lista-proveedor.component';
import { BajaProveedorComponent } from './proveedores/baja-proveedor/baja-proveedor.component';
import { ModificarProveedorComponent } from './proveedores/modificar-proveedor/modificar-proveedor.component';
import { ProveedorProvider } from 'src/providers/proveedorProvider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AltaPresupuestoComponent } from './presupuestos/alta-presupuesto/alta-presupuesto.component';
import { BajaPresupuestoComponent } from './presupuestos/baja-presupuesto/baja-presupuesto.component';
import { ModificacionPresupuestoComponent } from './presupuestos/modificacion-presupuesto/modificacion-presupuesto.component';
import { ListaPresupuestoComponent } from './presupuestos/lista-presupuesto/lista-presupuesto.component';
import { AltaCobranzaComponent } from './cobranzas/alta-cobranza/alta-cobranza.component';
import { AltaProductoComponent } from './productos/alta-producto/alta-producto.component';
import { AltaInsumoComponent } from './catalogo/alta-insumo/alta-insumo.component';
import { ListaInsumoComponent } from './catalogo/lista-insumo/lista-insumo.component';
import { InsumoProvider } from 'src/providers/insumoProvider';
import { ProvinciaProvider } from 'src/providers/provinciaProvider';
import { LocalidadProvider } from 'src/providers/localidadProvider';
import { ListaFacturasComponent } from './cobranzas/lista-facturas/lista-facturas.component';
import { FacturaProvider } from 'src/providers/facturaProvider';
import { HighchartsChartModule } from 'highcharts-angular';
import { DetalleProductoProvider } from 'src/providers/detalleProductoProvider';
import { ProductoProvider } from 'src/providers/productoProvider';
import { FiltroInsumoPipe } from './pipe/filtro-insumo.pipe';
import { FiltroClientePipe } from './pipe/filtro-cliente.pipe';
import { FiltroRazonSocialPipe } from './pipe/filtro-razon-social.pipe';
import { ListaFacturaComponent } from './facturas/lista-factura/lista-factura.component';
import { ListaCobranzaComponent } from './cobranzas/lista-cobranza/lista-cobranza.component';
import { FormatoFechaPipe } from './pipe/formato-fecha.pipe';
import { PresupuestoProvider } from 'src/providers/presupuestoProvider';
import { MenuProvider } from 'src/providers/menuProvider';
import { FiltroProveedorPipe } from './pipe/filtro-proveedor.pipe';
import { FiltroFechaPipe } from './pipe/filtro-fecha.pipe';
import { DatePipe } from '@angular/common';
import { CondicionIvaProvider } from 'src/providers/condicionIvaProvider';
import { NgxPaginationModule } from 'ngx-pagination';
import { AceraDeComponent } from './inicio/acera-de.component';
import { CobranzaProvider } from 'src/providers/cobranzaProvider';
import { FiltroPresupuestoPipe } from './pipe/filtro-presupuesto.pipe';
import { ReportesComponent } from './reportes/reportes.component';
import { EntregableComponent } from './presupuestos/entregable/entregable.component';
import { AltaFacturaComponent } from './facturas/alta-factura/alta-factura.component';
import { FaqComponent } from './inicio/faq.component';

@NgModule({
  declarations: [
    AppComponent,
    AltaClienteComponent,
    ListaClienteComponent,
    AltaProveedorComponent,
    PrincipalComponent,
    ListaProveedorComponent,
    BajaProveedorComponent,
    ModificarProveedorComponent,
    AltaPresupuestoComponent,
    BajaPresupuestoComponent,
    ModificacionPresupuestoComponent,
    ListaPresupuestoComponent,
    AltaCobranzaComponent,
    AltaProductoComponent,
    AltaInsumoComponent,
    ListaInsumoComponent,
    ListaFacturasComponent,
    FiltroInsumoPipe,
    FiltroClientePipe,
    FiltroRazonSocialPipe,
    ListaFacturaComponent,
    ListaCobranzaComponent,
    FormatoFechaPipe,
    FormatoFechaPipe,
    FiltroProveedorPipe,
    FiltroFechaPipe,
    AceraDeComponent,
    FiltroPresupuestoPipe,
    ReportesComponent,
    EntregableComponent,
    AltaFacturaComponent,
    FaqComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    HighchartsChartModule,
    FormsModule,
    NgxPaginationModule
  ],
  providers: [
    ClienteProvider, 
    ProveedorProvider, 
    InsumoProvider, 
    ProvinciaProvider, 
    LocalidadProvider, 
    FacturaProvider, 
    DetalleProductoProvider,
    ProductoProvider,
    PresupuestoProvider,
    MenuProvider,
    DatePipe,
    CondicionIvaProvider,
    CobranzaProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
