import { DetallePresupuestoT } from "./detallePresupuestoT";

export interface PresupuestoT {
    Id: string,
    Campania: string,
    Total: number,
    Activo: boolean,
    Aceptado: boolean,
    clienteId: string,
    listaDetallePresupuestoT: DetallePresupuestoT[]
}
