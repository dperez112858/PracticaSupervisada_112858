import { DetalleProductoU } from "./detalleProductoU";

export interface ProductoU {
    Id: string,
    nombre: string,
    banio: string,
    FechaCreacion: number,
    costoProducto: number,
    utilidad: number,
    costoTotal: number,
    DetalleProducto: DetalleProductoU[]
}