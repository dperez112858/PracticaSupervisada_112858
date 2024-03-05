import { Cliente } from "./cliente";

export interface Factura {
    id: string,
    fecha: Date,
    numero: string,
    tipoComprobante: number,
    netoGravado: number,
    iva: number,
    moneda: number,
    tipoCambio: number,
    total: number,
    cliente: Cliente,
    saldo: number,
    cancelada?: boolean
}