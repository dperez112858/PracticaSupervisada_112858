import { Proveedor } from "./proveedor";

export interface Insumo {
    id: string,
    descripcion: string,
    precio: number,
    fechaCompra: Date,
    proveedor: Proveedor
}
