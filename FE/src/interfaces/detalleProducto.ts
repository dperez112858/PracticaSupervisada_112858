import { Guid } from "guid-typescript";
import { Insumo } from "./insumo";
import { Producto } from "./producto";

export interface DetalleProducto{
    Id: string,
    ProductoId: Producto,
    fechaCreacion: Date,
    Insumo: Insumo,
    Cantidad: number,
    PrecioInsumo: number,
    Total: number
}