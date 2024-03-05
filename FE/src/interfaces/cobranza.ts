import { Cliente } from "./cliente";
import { DetalleCobranza } from "./detalleCobranza";

export interface Cobranza {
    id: string,
    fechaLiquidacion: Date,
    total: number,
    cliente: Cliente,
    detalleCobranza: DetalleCobranza[]
}
