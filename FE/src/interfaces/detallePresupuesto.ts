import { Guid } from "guid-typescript";
import { Producto } from "./producto";

export interface DetallePresupuesto{
    Id: string,
    CantidadProductos: number,
    PrecioUnitario: number,
    Total: number,
    ProductoId: string,
    PresupuestoId: string,
    Producto: Producto
}