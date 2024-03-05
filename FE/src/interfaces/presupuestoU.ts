
import { ProductoU } from "./productoU";

export interface PresupuestoU {
    campania: string,
    clienteId: string,
    total: number,
    activo: boolean,
    aceptado: boolean,
    productos: ProductoU[]
}
