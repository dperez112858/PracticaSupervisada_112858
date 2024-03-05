import { Guid } from "guid-typescript";
import { Insumo } from "./insumo";
import { Producto } from "./producto";

export interface DetalleProductoT{
    Id: string,
    Insumo: Insumo,
    NombreInsumo: string,
    FechaCreacion: number,
    Cantidad: number,
    PrecioInsumo: number,
    ProductoId: string,
    NombreProducto: string,
    NombreBanio: string,
    Total: number
}