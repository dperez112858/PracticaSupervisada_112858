import { Guid } from "guid-typescript";
import { DetalleProducto } from "./detalleProducto";

export interface Producto {
    Id: string,
    nombreProducto: string,
    banioProducto: string,
    fechaCrecion: number,
    costoProducto: number,
    utilidad: number,
    costoTotal: number,
    detalleProductos?: DetalleProducto[]
}

