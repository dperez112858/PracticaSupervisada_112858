import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AltaClienteComponent } from './clientes/alta-cliente/alta-cliente.component';
import { PrincipalComponent } from './inicio/principal.component';
import { AltaProveedorComponent } from './proveedores/alta-proveedor/alta-proveedor.component';
import { ListaClienteComponent } from './clientes/lista-cliente/lista-cliente.component';
import { ListaProveedorComponent } from './proveedores/lista-proveedor/lista-proveedor.component';
import { ModificarProveedorComponent } from './proveedores/modificar-proveedor/modificar-proveedor.component';
import { ModificacionPresupuestoComponent } from './presupuestos/modificacion-presupuesto/modificacion-presupuesto.component';
import { ListaPresupuestoComponent } from './presupuestos/lista-presupuesto/lista-presupuesto.component';
import { AltaPresupuestoComponent } from './presupuestos/alta-presupuesto/alta-presupuesto.component';
import { ListaInsumoComponent } from './catalogo/lista-insumo/lista-insumo.component';
import { ListaCobranzaComponent } from './cobranzas/lista-cobranza/lista-cobranza.component';
import { ListaFacturaComponent } from './facturas/lista-factura/lista-factura.component';
import { BajaPresupuestoComponent } from './presupuestos/baja-presupuesto/baja-presupuesto.component';
import { AceraDeComponent } from './inicio/acera-de.component';

const routes: Routes = [
    {path: "inicio", component: PrincipalComponent},
    {path: "clientes", component: ListaClienteComponent},
    {path: "proveedores", component: ListaProveedorComponent},
    {path: "presupuestos", component: ListaPresupuestoComponent},
    {path: "alta-cliente", component: AltaClienteComponent},
    {path: "alta-proveedor", component: AltaProveedorComponent},
    {path: "alta-presupuesto", component: AltaPresupuestoComponent},
    {path: "editar-proveedor/:id", component: ModificarProveedorComponent},
    {path: "editar-presupuesto/:id", component: ModificacionPresupuestoComponent},
    {path: "catalogo", component: ListaInsumoComponent},
    {path: "cobranza", component: ModificacionPresupuestoComponent},
    {path: "factura", component: ListaFacturaComponent},
    {path: "acercaDe", component: AceraDeComponent},
    {path: "", component: PrincipalComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
