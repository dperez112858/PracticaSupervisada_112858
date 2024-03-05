import { Guid } from "guid-typescript";
import { Cliente } from "./cliente";
import { DetallePresupuesto } from "./detallePresupuesto";
import { Producto } from "./producto";

export interface Presupuesto {
    Id: string,
    Numero: string,
    Cliente: Cliente,
    Campania: string,
    total: number,
    activo: boolean,
    aceptado: boolean,
    productos?: Producto[]
}