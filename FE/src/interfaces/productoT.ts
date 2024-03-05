import { Guid } from "guid-typescript";
import { DetalleProductoT } from "./detalleProductoT";

export interface ProductoT {
    Id: string,
    nombre: string,
    banio: string,
    FechaCreacion: number,
    costoProducto: number,
    utilidad: number,
    costoTotal: number,
    detalleProductoT: DetalleProductoT[]
}