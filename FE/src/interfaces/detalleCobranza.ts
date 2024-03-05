import { Cobranza } from "./cobranza";
import { Factura } from "./factura";
import { TipoPago } from "./tipoPago";

export interface DetalleCobranza {
    id: string,
    importeCobrado: number,
    importeRetenido: number,
    importeTotal: number,
    //Factura: Factura,
    facturaId: string,
    fechaPago: Date,
    //TipoPago: TipoPago,
    tipoPagoId: string,
    cobranzaId: string
}
