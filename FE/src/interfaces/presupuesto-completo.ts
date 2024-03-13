import { Cliente } from "./cliente";

export interface PresupuestoCompleto {
    id: string;
    numero: number;
    campania: string;
    total: number;
    aceptado: boolean;
    cliente?: Cliente;
    fecha:Date;
    ok?: boolean;
    statusCode?: number;
    error?: string;
    items: items[];
}

interface items {
    producto: string;
    total: number;
    cantidad: number;
    id: string;
}